FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS restore
ARG USER
ARG TOKEN
WORKDIR /src
RUN dotnet nuget add source https://nuget.pkg.github.com/dkfz-unite/index.json -n github -u ${USER} -p ${TOKEN} --store-password-in-clear-text
COPY ["Unite.Specimens.Indices/Unite.Specimens.Indices.csproj", "Unite.Specimens.Indices/"]
COPY ["Unite.Specimens.Feed/Unite.Specimens.Feed.csproj", "Unite.Specimens.Feed/"]
COPY ["Unite.Specimens.Feed.Web/Unite.Specimens.Feed.Web.csproj", "Unite.Specimens.Feed.Web/"]
RUN dotnet restore "Unite.Specimens.Indices/Unite.Specimens.Indices.csproj"
RUN dotnet restore "Unite.Specimens.Feed/Unite.Specimens.Feed.csproj"
RUN dotnet restore "Unite.Specimens.Feed.Web/Unite.Specimens.Feed.Web.csproj"

FROM restore as build
COPY . .
WORKDIR "/src/Unite.Specimens.Feed.Web"
RUN dotnet build --no-restore "Unite.Specimens.Feed.Web.csproj" -c Release

FROM build AS publish
RUN dotnet publish --no-build "Unite.Specimens.Feed.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unite.Specimens.Feed.Web.dll"]