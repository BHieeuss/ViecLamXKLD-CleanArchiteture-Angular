using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Domain.Entities;

namespace ViecLam.Infrastructure.Extensions
{
    public class TcpSocketServer
    {
        private readonly IMessageRepository _messageRepository;
        private readonly TcpListener _listener;
        private ConcurrentBag<TcpClient> _connectedClients; // Lưu trữ tất cả client đang kết nối

        public TcpSocketServer(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
            _listener = new TcpListener(IPAddress.Any, 5000);
            _connectedClients = new ConcurrentBag<TcpClient>();
        }

        // Phương thức bắt đầu server TCP
        public async Task StartAsync()
        {
            _listener.Start();
            Console.WriteLine("Server started on port 5000...");

            while (true)
            {
                try
                {
                    var client = await _listener.AcceptTcpClientAsync();
                    _connectedClients.Add(client); // Thêm client vào danh sách
                    _ = Task.Run(() => HandleClientAsync(client)); // Xử lý kết nối client trong một task khác
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error accepting client: {ex.Message}");
                }
            }
        }

        // Phương thức xử lý client
        private async Task HandleClientAsync(TcpClient client)
        {
            Console.WriteLine("Client connected.");
            var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                // Lấy tất cả tin nhắn cũ và gửi cho client mới kết nối
                var allMessages = await _messageRepository.GetMessagesAsync();
                var messageBuilder = new StringBuilder();

                foreach (var msg in allMessages)
                {
                    messageBuilder.Append($"{msg.SenderName}: {msg.Content}\n");
                }

                // Gửi tất cả tin nhắn cũ cho client
                var messageBytes = Encoding.UTF8.GetBytes(messageBuilder.ToString());
                await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                Console.WriteLine("Sent all old messages to the new client.");

                // Tiếp tục xử lý tin nhắn mới từ client
                while (client.Connected)
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    if (bytesRead > 0)
                    {
                        var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {receivedMessage}");

                        // Giả định format tin nhắn từ client là "SenderName: Content"
                        var splitMessage = receivedMessage.Split(new[] { ':' }, 2);
                        if (splitMessage.Length == 2)
                        {
                            var senderName = splitMessage[0].Trim();
                            var content = splitMessage[1].Trim();

                            // Lưu tin nhắn vào cơ sở dữ liệu qua repository
                            var message = new Message
                            {
                                SenderName = senderName,
                                Content = content,
                                SentAt = DateTime.UtcNow
                            };
                            await _messageRepository.AddMessageAsync(message);
                            Console.WriteLine("Message saved to the database.");

                            // Gửi phản hồi cho client đã gửi tin nhắn
                            var responseMessage = "Message received and saved.";
                            var responseBytes = Encoding.UTF8.GetBytes(responseMessage);
                            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
                            Console.WriteLine($"Sent: {responseMessage}");

                            // Gửi tin nhắn đến tất cả client khác
                            await BroadcastMessageAsync($"{message.SenderName}: {message.Content}");
                        }
                        else
                        {
                            Console.WriteLine("Invalid message format.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error handling client: {ex.Message}");
            }
            finally
            {
                client.Close();
                Console.WriteLine("Client disconnected.");
            }
        }


        // Phương thức để thông báo cho tất cả client về tin nhắn mới
        public async Task BroadcastMessageAsync(string messageContent)
        {
            var messageBytes = Encoding.UTF8.GetBytes(messageContent);
            foreach (var client in _connectedClients)
            {
                if (client.Connected)
                {
                    try
                    {
                        var stream = client.GetStream();
                        await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error broadcasting to client: {ex.Message}");
                    }
                }
            }
        }
    }
}
