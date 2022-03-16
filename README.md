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
- Ap�s isso, o a aplica��o estar� dispon�vel atrav�s do endere�o abaixo:
```
http://localhost:15433
```
- Para verificar os recursos dispon�veis, acesse:
```
http://localhost:15433/swagger
```

H�, tamb�m, uma cole��o dispon�vel no diret�rio [clients], para ser importada no insomnia.