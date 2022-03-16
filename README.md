# Pré-requisitos para execução do projeto
- docker
- docker-compose

# como executar?
- Criar e configurar no diretório [src/WebApi] o arquivo appsettings.json. Para isso, clone o arquivo "example.appsettings.json" e renomeie para "appsettings.json". 
- No diretório raiz da solução, execute o comando abaixo para criar a imagem:
```
docker-compose build
```
- No diretório raiz da solução, execute o comando abaixo para criar e executar o container:
```
docker-compose up
```
- Após isso, o a aplicação estará disponível através do endereço abaixo:
```
http://localhost:15433
```
- Para verificar os recursos disponíveis, acesse:
```
http://localhost:15433/swagger
```

Há, também, uma coleção disponível no diretório [clients], para ser importada no insomnia.