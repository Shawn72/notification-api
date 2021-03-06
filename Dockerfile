#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
RUN sed -i 's/TLSv1.2/TLSv1/g' /etc/ssl/openssl.cnf
EXPOSE 8036
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NotificationApi.csproj", "."]
RUN dotnet restore "./NotificationApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "NotificationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotificationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationApi.dll",  "--environment=Production"]