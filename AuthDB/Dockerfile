# Используем официальный образ PostgreSQL
FROM postgres:latest

EXPOSE 5432

# Копируем конфигурационный файл в контейнер
#COPY postgresql.conf /etc/postgresql/postgresql.conf

# Указываем использовать наш конфигурационный файл
CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]