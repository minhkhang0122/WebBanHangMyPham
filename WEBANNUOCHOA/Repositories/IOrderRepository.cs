using WEBANNUOCHOA.Models;

namespace WEBANNUOCHOA.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetAllAsync();
        Task AddAsync(Order order);
        void Update(Order order);
        Task DeleteAsync(int id);
    }
}
// New