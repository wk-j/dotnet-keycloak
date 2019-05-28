## Keycloak

Start server

```bash
docker run --name keycloak -p 8080:8080 -e KEYCLOAK_USER=admin -e KEYCLOAK_PASSWORD=admin jboss/keycloak
```

Add client

![](images/Client.png)

Test

- http://localhost:5000/api/hello/hello