using System.Net.WebSockets;
using System.Text;
using ViecLam.Application.Contracts.Persistances;
using System.Net.Sockets;

namespace ViecLam.Infrastructure.Extensions
{
    public static class WebSocketExtensions
    {
        public static IApplicationBuilder UseCustomWebSocket(this IApplicationBuilder app)
        {
            app.UseWebSockets(); // Thêm middleware WebSocket

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/ws")
                {
                    if (context.WebSockets.IsWebSocketRequest)
                    {
                        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                        await HandleWebSocketAsync(webSocket);
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                    }
                }
                else
                {
                    await next();
                }
            });

            return app;
        }

        private static async Task HandleWebSocketAsync(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var tcpClient = new TcpClient();
            await tcpClient.ConnectAsync("127.0.0.1", 5000); // Kết nối đến TCP server
            var tcpStream = tcpClient.GetStream();

            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            while (!result.CloseStatus.HasValue)
            {
                // Gửi tin nhắn từ WebSocket tới TCP server
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"[WebSocket] Received: {receivedMessage}");
                byte[] tcpMessage = Encoding.UTF8.GetBytes(receivedMessage);
                await tcpStream.WriteAsync(tcpMessage, 0, tcpMessage.Length);

                // Nhận phản hồi từ TCP server
                var tcpBuffer = new byte[1024];
                int bytesRead = await tcpStream.ReadAsync(tcpBuffer, 0, tcpBuffer.Length);
                if (bytesRead > 0)
                {
                    string responseMessage = Encoding.UTF8.GetString(tcpBuffer, 0, bytesRead);
                    byte[] responseBuffer = Encoding.UTF8.GetBytes(responseMessage);

                    // Gửi phản hồi từ TCP server đến WebSocket client (Angular)
                    await webSocket.SendAsync(new ArraySegment<byte>(responseBuffer, 0, responseBuffer.Length), result.MessageType, result.EndOfMessage, CancellationToken.None);
                }

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }
    }
}
