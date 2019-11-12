docker-compose build
docker-compose up &
sleep 10
dotnet ef database update --context fastcastusercontext
dotnet ef database update --context fastcastcontext

