Run command for build project
dotnet build

Go to folder contain file docker-compose
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

Nếu bạn muốn chạy từng service một thì dùng lệnh sau
docker compose up -d --build <tên_service>

### Database Migrations

To use dotnet-ef for your migrations first ensure that "UseInMemoryDatabase" is disabled, as described within previous section. Then, add the following flags to your command (values assume you are executing from repository root)

- --project src/Infrastructure (optional if in this folder)
- --startup-project src/WebUI
- --output-dir Persistence/Migrations

For example, to add a new migration from the root folder:
Add-Migration InitOrderDb  -Project Ordering.Infrastructure  -StartupProject Ordering.API -OutputDir Persistence/Migrations
Update-Database -Project Ordering.Infrastructure -StartupProject Ordering.API