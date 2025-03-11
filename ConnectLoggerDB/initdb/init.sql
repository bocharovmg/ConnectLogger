CREATE DATABASE ConnectLoggerDB;

CREATE USER additional_user WITH PASSWORD 'additional_user';

GRANT CONNECT ON DATABASE ConnectLoggerDB TO additional_user;

GRANT USAGE ON SCHEMA public TO additional_user;

GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO additional_user;

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO additional_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO additional_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT USAGE, SELECT ON SEQUENCES TO additional_user;

CREATE TABLE public."UserConnectionLog"
(
	"UserConnectionLogId" bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
	"LogDateTime" timestamp without time zone NOT NULL DEFAULT (now() at time zone 'utc'),
	"ConnectedUserId" bigint NOT NULL,
	"UserIp4Address" character varying(15) COLLATE pg_catalog."default" NOT NULL,
	"UserIp6Address" character varying(39) COLLATE pg_catalog."default" NOT NULL,
	PRIMARY KEY ("UserConnectionLogId")
)

TABLESPACE pg_default;

ALTER TABLE public."UserConnectionLog"
    OWNER to postgres;