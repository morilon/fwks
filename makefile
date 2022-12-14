ApiProject=src/App.Api/App.Api.csproj
DataProject=src/Infra/Infra.csproj
DockerImage=example-service

run-api:
	dotnet restore
	dotnet build
	dotnet run --project ${ApiProject} --no-build

docker:
	docker compose -f ./sandbox/docker-compose.yml up -d --build

create-dropkick:
	dotnet ef --startup-project ${DataProject} migrations add DropkickDb -o Postgres/Migrations/History
drop-database:
	dotnet ef --startup-project ${DataProject} database drop
update-database:
	dotnet ef --startup-project ${DataProject} database update -- --environment Development
script-next:
	dotnet ef --startup-project ${DataProject} migrations script -- --environment Development

install-ef:
	dotnet tool install --global dotnet-ef
update-ef:
	dotnet tool update --global dotnet-ef