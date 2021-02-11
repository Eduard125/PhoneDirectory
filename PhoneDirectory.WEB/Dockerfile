#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PhoneDirectory.WEB/PhoneDirectory.WEB.csproj", "PhoneDirectory.WEB/"]
COPY ["PhoneDirectory.BLL/PhoneDirectory.BLL.csproj", "PhoneDirectory.BLL/"]
COPY ["PhoneDirectory.DAL/PhoneDirectory.DAL.csproj", "PhoneDirectory.DAL/"]
RUN dotnet restore "PhoneDirectory.WEB/PhoneDirectory.WEB.csproj"
COPY . .
WORKDIR "/src/PhoneDirectory.WEB"
RUN dotnet build "PhoneDirectory.WEB.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PhoneDirectory.WEB.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PhoneDirectory.WEB.dll"]
