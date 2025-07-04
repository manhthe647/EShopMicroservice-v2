using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    // Lớp mở rộng cho AutoMapper
    public static class AutoMapperExtension
    {
        /// <summary>
        /// Hàm mở rộng để tự động bỏ qua các property trong destination không tồn tại trong source.
        /// Dùng trong cấu hình AutoMapper để tránh lỗi map khi thiếu property.
        /// </summary>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            // Cờ để lấy các property public và instance
            var flags = BindingFlags.Public | BindingFlags.Instance;

            // Lấy kiểu dữ liệu của source
            var sourceType = typeof(TSource);

            // Lấy tất cả property của destination với cờ đã định nghĩa
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            // Duyệt qua từng property trong destination
            foreach (var property in destinationProperties)
            {
                // Nếu property không tồn tại trong source thì bỏ qua (Ignore)
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            // Trả về cấu hình mapping đã được sửa đổi
            return expression;
        }
    }

}
