CREATE DATABASE db_redarbor;

CREATE ROLE app_redarbor_api LOGIN CREATEDB PASSWORD 'masterkey';

GRANT SELECT, INSERT, UPDATE, DELETE 
ON ALL TABLES
IN SCHEMA "public"
TO app_redarbor_api;

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS public.employee (
	employee_id UUID PRIMARY KEY DEFAULT uuid_generate_v4 (),
	name character varying COLLATE pg_catalog."default",
	company_id integer NOT NULL,
    portal_id integer NOT NULL,
	role_id smallint NOT NULL,
	status_id smallint NOT NULL,
    email character varying NOT NULL,
	fax character varying COLLATE pg_catalog."default",
 	telephone character varying COLLATE pg_catalog."default",
    username character varying NOT NULL,
	password character varying NOT NULL,
	created_on timestamp without time zone,
	updated_on timestamp without time zone,
	deleted_on timestamp without time zone,
    last_login timestamp without time zone,
    CONSTRAINT fk_employee_company FOREIGN KEY (company_id) REFERENCES public.company (company_id) MATCH SIMPLE ON UPDATE CASCADE ON DELETE NO ACTION,
    CONSTRAINT fk_employee_portal FOREIGN KEY (portal_id) REFERENCES public.portal (portal_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION,
	CONSTRAINT fk_employee_role FOREIGN KEY (role_id) REFERENCES public.portal (role_id) MATCH SIMPLE ON UPDATE NO ACTION ON DELETE NO ACTION
);
ALTER TABLE IF EXISTS public.employee OWNER to postgres;
COMMENT ON TABLE public.employee IS 'Holds data for employees';
COMMENT ON COLUMN public.employee.employee_id IS 'Unique value that identifies the employee locally on the table';
COMMENT ON COLUMN public.employee.name IS 'Name of the product';
COMMENT ON COLUMN public.employee.company_id IS 'Current portal for a employee.';
COMMENT ON COLUMN public.employee.portal_id IS 'Current portal for a employee.';
COMMENT ON COLUMN public.employee.role_id IS 'Current role for a employee.';
COMMENT ON COLUMN public.employee.status_id IS 'Current status for a employee.';
COMMENT ON COLUMN public.employee.email IS 'Employee´s email address.';
COMMENT ON COLUMN public.employee.fax IS 'Employee fax number.';
COMMENT ON COLUMN public.employee.telephone IS 'Employee´s telephone number.';
COMMENT ON COLUMN public.employee.username IS 'Employee´s username.';
COMMENT ON COLUMN public.employee.password IS 'Employee´s password.';
COMMENT ON COLUMN public.employee.created_on IS 'Record to keep track of the creation';
COMMENT ON COLUMN public.employee.updated_on IS 'Record to keep track of the last edit';
COMMENT ON COLUMN public.employee.deleted_on IS 'Record to keep track of the delete';
COMMENT ON COLUMN public.employee.last_login IS 'Record to keep track of the last login';
CREATE INDEX IF NOT EXISTS ix_employee_email ON public.employee USING btree (email COLLATE pg_catalog."default" ASC NULLS LAST) TABLESPACE pg_default;
CREATE INDEX IF NOT EXISTS ix_employee_username ON public.employee USING btree (username ASC NULLS LAST) TABLESPACE pg_default;