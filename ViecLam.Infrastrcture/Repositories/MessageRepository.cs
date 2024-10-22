using Microsoft.EntityFrameworkCore;
using ViecLam.Application.Contracts.Persistances;
using ViecLam.Domain.Entities;
using ViecLam.Infrastructure.Context;

namespace ViecLam.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext context;
        public MessageRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddMessageAsync(Message message)
        {
            context.Set<Message>().Add(message);
            await context.SaveChangesAsync();
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await context.Set<Message>().OrderBy(m => m.SentAt).ToListAsync();
        }
    }
}
