using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpClientTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.Write("Enter your name: ");
            var senderName = Console.ReadLine();

            using (var client = new TcpClient())
            {
                await client.ConnectAsync("127.0.0.1", 5000);
                var stream = client.GetStream();

                Console.WriteLine("Connected to server. You can send messages or wait for updates from the server.");

                // Task lắng nghe dữ liệu từ server liên tục
                _ = Task.Run(async () =>
                {
                    var buffer = new byte[1024];
                    while (true)
                    {
                        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        if (bytesRead > 0)
                        {
                            var response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            await DisplayMessageWithEffect($"[Server] {response}");
                        }
                    }
                });

                // Đoạn này để bạn gửi tin nhắn đến server
                while (true)
                {
                    var content = Console.ReadLine();
                    if (content == "exit")
                    {
                        break;
                    }

                    var message = $"{senderName}: {content}";
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await stream.WriteAsync(messageBytes, 0, messageBytes.Length);
                    Console.WriteLine($"Sent: {message}");
                }
            }
        }

        // Hiển thị từng dòng với hiệu ứng từ từ
        static async Task DisplayMessageWithEffect(string message)
        {
            var lines = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None); // Tách message thành các dòng
            foreach (var line in lines)
            {
                Console.WriteLine(line); // Hiển thị từng dòng
                await Task.Delay(1000); // Tạo độ trễ giữa các dòng (1 giây ở đây)
            }
        }
    }
}
