CREATE DATABASE UserDB;

CREATE USER additional_user WITH PASSWORD 'additional_user';

GRANT CONNECT ON DATABASE UserDB TO additional_user;

GRANT USAGE ON SCHEMA public TO additional_user;

GRANT SELECT, INSERT, UPDATE, DELETE ON ALL TABLES IN SCHEMA public TO additional_user;

GRANT USAGE, SELECT ON ALL SEQUENCES IN SCHEMA public TO additional_user;

ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT SELECT, INSERT, UPDATE, DELETE ON TABLES TO additional_user;
ALTER DEFAULT PRIVILEGES IN SCHEMA public GRANT USAGE, SELECT ON SEQUENCES TO additional_user;

CREATE TABLE public."User"
(
	"UserId" bigint NOT NULL NOT NULL GENERATED ALWAYS AS IDENTITY,
	"UserName" character varying(20) COLLATE pg_catalog."default" NOT NULL,
	PRIMARY KEY ("UserId"),
    CONSTRAINT "U_User_UserName" UNIQUE ("UserName")
)

TABLESPACE pg_default;

ALTER TABLE public."User"
    OWNER to postgres;