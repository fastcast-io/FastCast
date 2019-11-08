docker-compose build
docker-compose up &
sleep 20
dotnet ef database update --context fastcastusercontext
dotnet ef database update --context fastcastcontext

