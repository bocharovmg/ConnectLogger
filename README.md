# ConnectLogger

## Подготовка к демонстрационному запуску
В папке с проектом запустите консоль и выполните следующие шаги
### 1. Создать общую сеть Docker
```console
docker network create connectlogger-network
```
### 2. Запуск Kafka
```console
docker-compose -f .\Kafka\docker-compose.yml up -d
```
### 3. Запуск БД микросервиса авторизации
```console
docker-compose -f .\AuthDB\docker-compose.yml up -d
```
### 4. Запуск БД микросервиса логирования подключений
```console
docker-compose -f .\ConnectLoggerDB\docker-compose.yml up -d
```
