# Specimens Data Feed Service

## General
Specimens data feed service provides the following functionality:
- [Specimens data feed web API](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Docs/api.md) - REST API for uploading specimens data to the portal (including input data validation).
- Specimens data indexing service - background service responsible for specimen-centric data index creation.

Specimens data feed service is written in ASP.NET (.NET 5)

## Dependencies
- [SQL](https://github.com/dkfz-unite/unite-environment/tree/main/programs/postgresql) - SQL server with domain data and user identity data.
- [Elasticsearch](https://github.com/dkfz-unite/unite-environment/tree/main/programs/elasticsearch) - Elasticsearch server with indices of domain data.

## Access
Environment|Address|Port
-----------|-------|----
Host|http://localhost:5102|5102
Docker|http://feed.specimens.unite.net|80

## Configuration
To configure the application, change environment variables in either docker or [launchSettings.json](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Unite.Specimens.Feed.Web/Properties/launchSettings.json) file (if running locally):
Variable|Description|Default(Local)|Default(Docker)
--------|-----------|--------------|---------------
ASPNETCORE_ENVIRONMENT|ASP.NET environment|Debug|Release
UNITE_SQL_HOST|SQL server host|localhost|sql.unite.net
UNITE_SQL_PORT|SQL server port|5432|5432
UNITE_SQL_USER|SQL server user||
UNITE_SQL_PASSWORD|SQL server password||
UNITE_ELASTIC_HOST|ES service host|http://localhost:9200|es.unite.net:9200
UNITE_ELASTIC_USER|ES service user||
UNITE_ELASTIC_PASSWORD|ES service password||
UNITE_INDEXING_INTERVAL|Indexing interval (seconds)|10|
UNITE_INDEXING_BUCKET_SIZE|Indexing bucket size|10|

## Installation

### Docker Compose
The easiest way to install the application is to use docker-compose:
- Environment configuration and installation scripts: https://github.com/dkfz-unite/unite-environment
- Specimens data feed service configuration and installation scripts: https://github.com/dkfz-unite/unite-environment/tree/main/applications/unite-specimens-feed

### Docker
[Dockerfile](https://github.com/dkfz-unite/unite-specimens-feed/blob/main/Dockerfile) is used to build an image of the application.
To build an image run the following command:
```
docker build -t unite.specimens.feed:latest .
```

All application components should run in the same docker network.
To create common docker network if not yet available run the following command:
```bash
docker network create unite
```

To run application in docker run the following command:
```bash
docker run \
--name unite.specimens.feed \
--restart unless-stopped \
--net unite \
--net-alias feed.specimens.unite.net \
-p 127.0.0.1:5102:80 \
-e ASPNETCORE_ENVIRONMENT=Release \
-e UNITE_ELASTIC_HOST=http://es.unite.net:9200 \
-e UNITE_ELASTIC_USER=[elasticsearch_user] \
-e UNITE_ELASTIC_PASSWORD=[elasticsearch_password] \
-e UNITE_SQL_HOST=sql.unite.net \
-e UNITE_SQL_PORT=5432 \
-e UNITE_SQL_USER=[sql_user] \
-e UNITE_SQL_PASSWORD=[sql_password] \
-e UNITE_INDEXING_INTERVAL=10 \
-e UNITE_INDEXING_BUCKET_SIZE=10 \
-d \
unite.specimens.feed:latest
```
