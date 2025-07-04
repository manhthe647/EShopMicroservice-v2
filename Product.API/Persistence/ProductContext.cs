using Contracts.Domains.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

namespace Product.API.Persistence
{
    public class ProductContext:DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Entities.CardProduct> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Gọi phương thức base để đảm bảo các cấu hình mặc định được áp dụng
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CardProduct>().HasIndex(p => p.No).IsUnique(); // Đảm bảo trường No là duy nhất
        }

        // Ghi đè phương thức SaveChangesAsync để xử lý logic cập nhật thời gian tạo/sửa đổi
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            // Lọc các entity có trạng thái Modified, Added hoặc Deleted
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified
                         || e.State == EntityState.Added
                         || e.State == EntityState.Deleted);

            // Duyệt qua từng entity trong danh sách vừa lọc
            foreach (var item in modified)
            {
                switch (item.State)
                {
                    // Trường hợp entity mới được thêm vào
                    case EntityState.Added:
                        // Kiểm tra nếu entity này có implement interface IDateTracking
                        if (item.Entity is IDateTracking addedEntity)
                        {
                            // Gán ngày tạo là thời gian hiện tại (UTC)
                            addedEntity.CreatedDate = DateTime.UtcNow;

                            // Đảm bảo trạng thái vẫn là Added sau khi chỉnh sửa
                            item.State = EntityState.Added;
                        }
                        break;

                    // Trường hợp entity bị chỉnh sửa
                    case EntityState.Modified:
                        // Đánh dấu thuộc tính Id là không bị thay đổi
                        Entry(item.Entity).Property("Id").IsModified = false;

                        // Nếu entity có implement IDateTracking thì cập nhật ngày sửa đổi
                        if (item.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;

                            // Đảm bảo trạng thái vẫn là Modified sau khi chỉnh sửa
                            item.State = EntityState.Modified;
                        }
                        break;
                }
            }

            // Gọi phương thức SaveChangesAsync gốc để lưu thay đổi vào cơ sở dữ liệu
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
