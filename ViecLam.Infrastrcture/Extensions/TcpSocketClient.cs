using System.Net.Sockets;
using System.Text;

namespace ViecLam.Infrastructure.Extensions
{
    public class TcpSocketClient
    {
        public async Task ConnectAsync()
        {
            using (var client = new TcpClient())
            {
                await client.ConnectAsync("127.0.0.1", 5000); // Kết nối đến server chạy trên localhost và cổng 5000
                var stream = client.GetStream();

                // Gửi tin nhắn đến server
                var message = Encoding.UTF8.GetBytes("Hello from client");
                await stream.WriteAsync(message, 0, message.Length);

                // Nhận phản hồi từ server
                var buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                Console.WriteLine($"Received: {Encoding.UTF8.GetString(buffer, 0, bytesRead)}");
            }
        }
    }
}
