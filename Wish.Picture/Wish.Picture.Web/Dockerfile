FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Wish.Picture.Web/Wish.Picture.Web.csproj", "Wish.Picture.Web/"]
RUN dotnet restore "Wish.Picture.Web/Wish.Picture.Web.csproj"
COPY . .
WORKDIR "/src/Wish.Picture.Web"
RUN dotnet build "Wish.Picture.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wish.Picture.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wish.Picture.Web.dll"]