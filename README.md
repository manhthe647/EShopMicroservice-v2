Run command for build project
dotnet build

Go to folder contain file docker-compose
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d --remove-orphans --build

