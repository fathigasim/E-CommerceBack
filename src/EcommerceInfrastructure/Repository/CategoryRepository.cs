using AutoMapper;
using EcommerceDomain.Entities;
using EcommerceDomain.Interfaces;
using EcommerceInfrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfrastructure.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

        public async Task<Category?> GetCategoryWithProductsAsync(
            Guid categoryId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == categoryId, cancellationToken);
        }
    }

}
