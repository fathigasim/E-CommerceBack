
namespace EcommerceInfrastructure.Repository
{
    using AutoMapper;
    using EcommerceDomain.Interfaces;
    using EcommerceInfrastructure.Persistance;
    using EcommerceInfrastructure.Repository;
   
    //public class UnitOfWork : IUnitOfWork
    //{
    //    private readonly AppDbContext _context;

    //    // Repositories are now initialized
    //    public IRepository<Order> Orders { get; private set; }
    //    public IRepository<Product> Products { get; private set; }

    //    public UnitOfWork(AppDbContext context)
    //    {
    //        _context = context;

    //        // We pass the shared context to the repositories
    //        Orders = new Repository<Order>(_context);
    //        Products = new Repository<Product>(_context);
    //    }

    //    public async Task<int> CompleteAsync()
    //    {
    //        return await _context.SaveChangesAsync();
    //    }

    //    public void Dispose()
    //    {
    //        _context.Dispose();
    //    }
    //}


    using Microsoft.EntityFrameworkCore.Storage;


        public class UnitOfWork : IUnitOfWork //, IDisposable, IAsyncDisposable
        {
            private readonly AppDbContext _context;
            private readonly IMapper _mapper;
            private IDbContextTransaction? _transaction;
            private bool _disposed;

            // Repository backing fields
            private IProductRepository? _products;
            private ICategoryRepository? _categories;
            private IOrderRepository? _orders;
            private IPaymentRepository? _payments;
            private IBasketRepository? _baskets;

            public UnitOfWork(AppDbContext context,IMapper mapper)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            #region Repositories (Lazy Loading)

            public IProductRepository Products =>
                _products ??= new ProductRepository(_context,_mapper );

            public ICategoryRepository Categories =>
                _categories ??= new CategoryRepository(_context);

            public IOrderRepository Orders =>
                _orders ??= new OrderRepository(_context,_mapper);
            public IPaymentRepository Payments =>
              _payments ??= new PaymentRepository(_context, _mapper);

            public IBasketRepository Baskets =>
                _baskets ??= new BasketRepository(_context);

            #endregion

            #region Transaction Management

            public bool HasActiveTransaction => _transaction != null;

            public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
            {
                if (_transaction != null)
                {
                    throw new InvalidOperationException("A transaction is already in progress.");
                }

                _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            }

            public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            {
                if (_transaction == null)
                {
                    throw new InvalidOperationException("No transaction in progress.");
                }

                try
                {
                    await _context.SaveChangesAsync(cancellationToken);
                    await _transaction.CommitAsync(cancellationToken);
                }
                catch
                {
                    await RollbackTransactionAsync(cancellationToken);
                    throw;
                }
                finally
                {
                    await DisposeTransactionAsync();
                }
            }

            public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            {
                if (_transaction == null)
                {
                    return; // No transaction to rollback
                }

                try
                {
                    await _transaction.RollbackAsync(cancellationToken);
                }
                finally
                {
                    await DisposeTransactionAsync();
                }
            }

            private async Task DisposeTransactionAsync()
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }

            #endregion

            #region Save Changes

            public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }

            #endregion

            #region Dispose Pattern

            //public void Dispose()
            //{
            //    Dispose(true);
            //    GC.SuppressFinalize(this);
            //}

            //protected virtual void Dispose(bool disposing)
            //{
            //    if (!_disposed && disposing)
            //    {
            //        _transaction?.Dispose();
            //        _context.Dispose();
            //        _disposed = true;
            //    }
            //}

            //public async ValueTask DisposeAsync()
            //{
            //    if (!_disposed)
            //    {
            //        if (_transaction != null)
            //        {
            //            await _transaction.DisposeAsync();
            //        }

            //        await _context.DisposeAsync();
            //        _disposed = true;
            //    }

            //    GC.SuppressFinalize(this);
            //}

            #endregion
        }
    }

