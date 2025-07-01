using Contracts.Common.Interfaces;
using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Common
{
    public class RepositoryBaseAsync<T, K, TContext> : IRepositoryBaseAsync<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {
        private readonly TContext _dbContext;
        private readonly IUnitOfWork<TContext> _unitOfWork;

        /// <summary>
        /// Constructor khởi tạo repository với DbContext và UnitOfWork
        /// Ví dụ: var userRepo = new RepositoryBaseAsync<User, int, AppDbContext>(dbContext, unitOfWork);
        /// </summary>
        public RepositoryBaseAsync(TContext dbContext, IUnitOfWork<TContext> unitOfWork)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        /// <summary>
        /// Bắt đầu transaction để thực hiện nhiều thao tác database trong 1 giao dịch
        /// Ví dụ: using var transaction = await _userRepo.BeginTransactionAsync();
        /// </summary>
        public Task<IDbContextTransaction> BeginTransactionAsync() =>
            _dbContext.Database.BeginTransactionAsync();

        /// <summary>
        /// Kết thúc transaction và commit tất cả thay đổi vào database
        /// Ví dụ: await _userRepo.EndTransactionAsync(); // Lưu và commit
        /// </summary>
        public async Task EndTransactionAsync()
        {
            await SaveChangesAsync();
            await _dbContext.Database.CommitTransactionAsync();
        }

        /// <summary>
        /// Hủy bỏ transaction và rollback tất cả thay đổi
        /// Ví dụ: await _userRepo.RollbackTransactionAsync(); // Hủy tất cả thay đổi
        /// </summary>
        public Task RollbackTransactionAsync() =>
            _dbContext.Database.RollbackTransactionAsync();

        /// <summary>
        /// Thêm mới 1 entity vào database và trả về ID
        /// Ví dụ: var userId = await _userRepo.CreateAsync(new User { Name = "John" });
        /// </summary>
        public async Task<K> CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity.Id;
        }

        /// <summary>
        /// Thêm mới nhiều entity cùng lúc (bulk insert) và trả về danh sách ID
        /// Ví dụ: var userIds = await _userRepo.CreateListAsync(listUsers);
        /// </summary>
        public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            return entities.Select(x => x.Id).ToList();
        }

        /// <summary>
        /// Cập nhật thông tin của 1 entity
        /// Ví dụ: await _userRepo.UpdateAsync(user); // user.Name = "Updated Name"
        /// </summary>
        public Task UpdateAsync(T entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Unchanged)
                return Task.CompletedTask;

            T? exist = _dbContext.Set<T>().Find(entity.Id);
            if (exist != null)
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Cập nhật nhiều entity cùng lúc - sửa lỗi logic từ AddRangeAsync
        /// Ví dụ: await _userRepo.UpdateListAsync(updatedUsers);
        /// </summary>
        public Task UpdateListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Xóa 1 entity khỏi database
        /// Ví dụ: await _userRepo.DeleteAsync(user);
        /// </summary>
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Xóa nhiều entity cùng lúc
        /// Ví dụ: await _userRepo.DeleteListAsync(usersToDelete);
        /// </summary>
        public Task DeleteListAsync(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Lấy tất cả records từ bảng, có thể tắt tracking để tăng performance
        /// Ví dụ: var users = await _userRepo.FindAll(false).ToListAsync(); // Không track changes
        /// </summary>
        public IQueryable<T> FindAll(bool trackChanges = false) =>
            !trackChanges
                ? _dbContext.Set<T>().AsNoTracking()
                : _dbContext.Set<T>();

        /// <summary>
        /// Lấy tất cả records và include các navigation properties (JOIN)
        /// Ví dụ: var orders = _orderRepo.FindAll(false, o => o.Customer, o => o.OrderItems).ToList();
        /// </summary>
        public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindAll(trackChanges);
            items = includeProperties.Aggregate(items,
                (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        /// <summary>
        /// Tìm kiếm records theo điều kiện WHERE
        /// Ví dụ: var activeUsers = await _userRepo.FindByCondition(u => u.IsActive).ToListAsync();
        /// </summary>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
            !trackChanges
                ? _dbContext.Set<T>().Where(expression).AsNoTracking()
                : _dbContext.Set<T>().Where(expression);

        /// <summary>
        /// Tìm kiếm theo điều kiện và include navigation properties
        /// Ví dụ: var user = await _userRepo.FindByCondition(u => u.Email == "test@test.com", false, u => u.Posts).FirstOrDefaultAsync();
        /// </summary>
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
        {
            var items = FindByCondition(expression, trackChanges);
            items = includeProperties.Aggregate(items,
                (current, includeProperty) => current.Include(includeProperty));
            return items;
        }

        /// <summary>
        /// Lấy 1 entity theo ID - fix nullable warning
        /// Ví dụ: var user = await _userRepo.GetByIdAsync(123);
        /// </summary>
        public async Task<T?> GetByIdAsync(K id) =>
            await FindByCondition(x => x.Id!.Equals(id))
                .FirstOrDefaultAsync();

        /// <summary>
        /// Lấy entity theo ID và include các navigation properties - fix nullable warning
        /// Ví dụ: var user = await _userRepo.GetByIdAsync(123, u => u.Posts, u => u.Profile);
        /// </summary>
        public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
            await FindByCondition(x => x.Id!.Equals(id), false, includeProperties)
                .FirstOrDefaultAsync();

        /// <summary>
        /// Lưu tất cả thay đổi vào database thông qua UnitOfWork
        /// Ví dụ: await _userRepo.SaveChangesAsync(); // Commit tất cả thay đổi
        /// </summary>
        public Task<int> SaveChangesAsync() =>
            _unitOfWork.CommitAsync();
    }
}