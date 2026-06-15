using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetById(Guid id);
        Task<User> Create(User user);
    }
}