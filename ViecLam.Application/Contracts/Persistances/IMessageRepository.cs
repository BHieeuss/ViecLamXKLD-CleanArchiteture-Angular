using ViecLam.Domain.Entities;

namespace ViecLam.Application.Contracts.Persistances
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync();

    }
}
