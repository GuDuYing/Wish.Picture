FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
RUN apt-get update
RUN apt-get install -y --no-install-recommends libgdiplus libc6-dev 
ENV LANG C.UTF-8
# ≈‰÷√◊÷ÃÂ
RUN apt-get install -y --no-install-recommends fontconfig
RUN fc-list
RUN apt-get -y install fonts-wqy-zenhei && apt-get clean &&  fc-cache -fv
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["Wish.Picture.Web.csproj", "src/"]
RUN dotnet restore "src/Wish.Picture.Web.csproj"
COPY . .
WORKDIR "/src"
RUN pwd
RUN ls
RUN dotnet build "Wish.Picture.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wish.Picture.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wish.Picture.Web.dll"]