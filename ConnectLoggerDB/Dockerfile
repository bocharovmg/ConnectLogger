# Используем официальный образ PostgreSQL
FROM postgres:latest

EXPOSE 5432

# Указываем использовать наш конфигурационный файл
CMD ["postgres", "-c", "config_file=/etc/postgresql/postgresql.conf"]