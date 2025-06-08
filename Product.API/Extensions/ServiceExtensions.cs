using Microsoft.OpenApi.Models;

namespace Product.API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Đăng ký các dịch vụ hạ tầng cơ bản cho Product API,
        /// bao gồm controllers, routing, Swagger, và các dịch vụ khác.
        /// </summary>
        /// <param name="services">IServiceCollection để thêm các dịch vụ.</param>
        /// <returns>IServiceCollection đã được cấu hình.</returns>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // 1. Controllers: xử lý HTTP request và trả về response
            // Ví dụ: Khi client gọi GET /products, controller sẽ trả về danh sách sản phẩm.
            services.AddControllers();

            // 2. Routing: chuyển URL thành lowercase để đảm bảo nhất quán
            // Ví dụ: /Product/GetById -> /product/getbyid, giúp tránh lỗi phân biệt hoa thường trên server
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
            });

            // 3. API Explorer: cung cấp metadata cho Swagger
            // Metadata ở đây là các thông tin về endpoint như đường dẫn, phương thức HTTP, mô tả, tham số,
            // giúp Swagger tự động xây dựng UI và tài liệu API.
            services.AddEndpointsApiExplorer();

            // 4. Swagger: tạo giao diện và tài liệu tự động cho API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                {
                    Title = "Product API",
                    Version = "v1",
                    Description = "API quản lý sản phẩm"
                });
                // Ví dụ thêm mô tả cho model nếu cần:
                // c.SchemaFilter<CustomSchemaFilter>();
            });

            // Ví dụ thêm các dịch vụ hạ tầng khác:
            // -- Logging: ghi log ra console hoặc file
            // services.AddLogging();

            // -- Database: đăng ký DbContext của Entity Framework Core
            // services.AddDbContext<ProductDbContext>(options =>
            //     options.UseSqlServer("YourConnectionString"));

            // -- Caching: sử dụng memory cache hoặc Redis
            // services.AddMemoryCache();

            // -- Message Queue: ví dụ RabbitMQ
            // services.AddSingleton<IMessageQueueClient, RabbitMqClient>();

            return services;
        }
    }
}
