using System.Collections.Generic;
using System.Threading.Tasks;
using CapiWear_API.Models;

namespace CapiWear_API.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(Order order);
        Task<bool> DeleteOrderAsync(int id);
    }
}
