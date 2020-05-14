## Command

```bash
COMPOSE_DOCKER_CLI_BUILD=1 DOCKER_BUILDKIT=1 docker-compose up --build

docker-compose stop app
COMPOSE_DOCKER_CLI_BUILD=1 DOCKER_BUILDKIT=1 docker-compose up --build -d

docker-compose exec app sh
```

## Check

```bash
curl -s http://localhost/weatherForecast | jq
curl -s http://localhost/hello | jq
curl -s http://wks-imac.local/hello
curl http://localhost:8080

open https://localhost:5001/hello
```