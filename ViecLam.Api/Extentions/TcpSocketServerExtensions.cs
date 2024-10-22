using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using ViecLam.Application.Contracts.Persistances;

namespace ViecLam.Infrastructure.Extensions
{
    public static class TcpSocketServerExtensions
    {
        public static void UseTcpSocketServer(this IApplicationBuilder app)
        {
            // Khởi động TCP server trong background với service scope
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            _ = Task.Run(async () =>
            {
                using (var scope = scopeFactory.CreateScope())
                {
                    var messageRepository = scope.ServiceProvider.GetRequiredService<IMessageRepository>();
                    var tcpSocketServer = new TcpSocketServer(messageRepository);
                    await tcpSocketServer.StartAsync(); // Chạy server TCP
                }
            });
        }
    }
}
