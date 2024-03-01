# Use a imagem SDK para compilar o código
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia a solução e os arquivos de projeto
COPY ["EasyFinance.RegisterSystem.sln", "./"]
COPY ["RegisterSystem.Application/RegisterSystem.Application.csproj", "RegisterSystem.Application/"]
COPY ["RegisterSystem.Contracts/RegisterSystem.Contracts.csproj", "RegisterSystem.Contracts/"]
COPY ["RegisterSystem.Domain/RegisterSystem.Domain.csproj", "RegisterSystem.Domain/"]
COPY ["RegisterSystem.Infrastructure/RegisterSystem.Infrastructure.csproj", "RegisterSystem.Infrastructure/"]
COPY ["RegisterSystemAPI/RegisterSystem.Api.csproj", "RegisterSystemAPI/"]

# Restaura as dependências e pacotes NuGet
RUN dotnet restore "RegisterSystemAPI/RegisterSystem.Api.csproj"


# Copia os arquivos do projeto e builda a solução
COPY . .
RUN dotnet publish "RegisterSystemAPI/RegisterSystem.Api.csproj" -c Release -o /app

# Gera a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "RegisterSystem.Api.dll"]
