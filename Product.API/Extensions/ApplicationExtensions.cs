namespace Product.API.Extensions
{
    public static class ApplicationExtensions
    {
        public static void UseInfrastructure(this IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

            // 1. Development-only middleware
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
                    c.RoutePrefix = "swagger"; // Đặt Swagger UI tại /swagger thay vì root
                });
            }

            // 2. HTTPS Redirection (phải đặt trước UseRouting)
            app.UseHttpsRedirection();

            // 3. Routing
            app.UseRouting();

            // 4. Authorization
            app.UseAuthorization();

            // 5. Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
