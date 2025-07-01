using Contracts.Domains;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common.Interfaces
{
    public interface IRepositoryQueryBase<T, K, TContext> where T : EntityBase<K>
      where TContext : DbContext
    {
        IQueryable<T> FindAll(bool trackChanges = false);

        IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false,
            params Expression<Func<T, object>>[] includeProperties);

        Task<T?> GetByIdAsync(K id);

        Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);
    }

    public interface IRepositoryBaseAsync<T, K, TContext>
        : IRepositoryQueryBase<T, K, TContext>
        where T : EntityBase<K>
        where TContext : DbContext
    {
        /// <summary>
        /// Thêm mới một thực thể vào cơ sở dữ liệu.
        /// </summary>
        Task<K> CreateAsync(T entity);

        /// <summary>
        /// Thêm danh sách thực thể vào cơ sở dữ liệu.
        /// </summary>
        Task<IList<K>> CreateListAsync(IEnumerable<T> entities);

        /// <summary>
        /// Cập nhật một thực thể.
        /// </summary>
        Task UpdateAsync(T entity);

        /// <summary>
        /// Cập nhật danh sách thực thể.
        /// </summary>
        Task UpdateListAsync(IEnumerable<T> entities);

        /// <summary>
        /// Xóa một thực thể.
        /// </summary>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Xóa danh sách thực thể.
        /// </summary>
        Task DeleteListAsync(IEnumerable<T> entities);

        /// <summary>
        /// Lưu các thay đổi xuống cơ sở dữ liệu.
        /// </summary>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Bắt đầu một giao dịch database.
        /// </summary>
        Task<IDbContextTransaction> BeginTransactionAsync();

        /// <summary>
        /// Kết thúc và commit giao dịch hiện tại.
        /// </summary>
        Task EndTransactionAsync();

        /// <summary>
        /// Hủy bỏ giao dịch hiện tại (rollback).
        /// </summary>
        Task RollbackTransactionAsync();
    }


}
