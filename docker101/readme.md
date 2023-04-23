### simple dot net core web api in docker (linux container)

#### highlights

- [Dot Net 6 installation](https://dotnet.microsoft.com/en-us/download)
- [dotnet new webapi](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-new)
- [Docker Windows installation](https://docs.docker.com/desktop/install/windows-install/)
- [Dockerfile & .dockerignroe file(s) example](https://dotnet.microsoft.com/en-us/learn/aspnet/microservice-tutorial/docker-file)
- Simple Docker Compose(docker-compose.yml)
- Note the port forwarding in yml: - "3000:80"

```yml
version: "1"
services:
  api:
    build: .
    ports:
      - "3000:80"
```
