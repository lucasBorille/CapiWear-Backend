using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapiWear_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CapiWear_API.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApiDbContext _context;
        public OrderRepository(ApiDbContext context) => _context = context;

        public async Task<Order?> GetOrderByIdAsync(int id)
            => await _context.Orders
                             .Include(o => o.OrderItems)
                             .AsNoTracking()
                             .FirstOrDefaultAsync(o => o.Id == id);

        public async Task<IReadOnlyList<Order>> GetAllOrdersAsync()
            => await _context.Orders
                             .Include(o => o.OrderItems)
                             .AsNoTracking()
                             .OrderByDescending(o => o.PlacedAt)
                             .ToListAsync();

        public async Task<Order> AddOrderAsync(Order order)
        {
            order.PlacedAt = System.DateTime.UtcNow;
            order.UpdatedAt = order.PlacedAt;
            order.Total = order.Subtotal + order.Freight;

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(Order order)
        {
            var existing = await _context.Orders.FirstOrDefaultAsync(o => o.Id == order.Id);
            if (existing is null) return null;

            existing.Subtotal = order.Subtotal;
            existing.Freight  = order.Freight;
            existing.Total    = existing.Subtotal + existing.Freight;
            existing.UpdatedAt = System.DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var existing = await _context.Orders.FindAsync(id);
            if (existing is null) return false;

            _context.Orders.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
