# Pr�-requisitos para execu��o do projeto
- docker
- docker-compose

# como executar?
- Criar e configurar no diret�rio [src/WebApi] o arquivo appsettings.json. Para isso, clone o arquivo "example.appsettings.json" e renomeie para "appsettings.json". 
- No diret�rio raiz da solu��o, execute o comando abaixo para criar a imagem:
```
docker-compose build
```

- No diret�rio raiz da solu��o, execute o comando abaixo para criar e executar o container:
```
docker-compose up
```