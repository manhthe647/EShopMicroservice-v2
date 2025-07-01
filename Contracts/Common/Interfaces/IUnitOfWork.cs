using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Common.Interfaces
{
    /// <summary>
    /// Định nghĩa giao diện cho mẫu thiết kế Unit of Work,
    /// dùng để quản lý giao dịch cơ sở dữ liệu với một DbContext cụ thể.
    /// </summary>
    /// <typeparam name="TContext">Kiểu của DbContext được sử dụng.</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : class
    {
        /// <summary>
        /// Thực hiện commit (lưu) tất cả các thay đổi trong context xuống cơ sở dữ liệu một cách bất đồng bộ.
        /// </summary>
        /// <returns>Số lượng bản ghi bị ảnh hưởng khi lưu vào cơ sở dữ liệu.</returns>
        Task<int> CommitAsync();
    }
}
