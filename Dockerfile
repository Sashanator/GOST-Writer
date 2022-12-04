FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 8080

# copy .csproj and restore as distinct layers
COPY "GOST.sln" "GOST.sln"
COPY "API/API.csproj" "API/API.csproj"
COPY "Gost.UnitTesting/API.UnitTesting.csproj" "Gost.UnitTesting/API.UnitTesting.csproj"

RUN dotnet restore "GOST.sln"

# run tests
# RUN dotnet test
# ENTRYPOINT [ "dotnet", "test" ]

# copy everything else and build
COPY . .
WORKDIR /app
RUN dotnet publish -c Release -o out

# build a runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "API.dll" ]