using AutoMapper;
using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using EcommerceInfrastructure.Persistance;
using Microsoft.EntityFrameworkCore;


namespace EcommerceInfrastructure.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync(CancellationToken cancellationToken = default)
        {
               return await _dbSet.Include(p=>p.Items).ThenInclude(p=>p.Product).AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersByUserAsync(
            string userId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Order?> GetOrderWithItemsAsync(
            Guid orderId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);
        }
    }


}
