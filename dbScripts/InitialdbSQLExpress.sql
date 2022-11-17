--Create Database

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'db_redarbor')
BEGIN
CREATE DATABASE [db_redarbor]
END
GO
USE [db_redarbor]
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='company' and xtype='U')
BEGIN
    CREATE TABLE [company] (
		company_id INT IDENTITY PRIMARY KEY,
		name NVARCHAR(70),
		status_id SMALLINT NOT NULL,
		created_on DATETIME2(6) NOT NULL,
		updated_on DATETIME2(6),
		deleted_on DATETIME2(6)  
    )
END 

--Create Tables
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='portal' and xtype='U')
BEGIN
    CREATE TABLE [portal] (
		portal_id INT IDENTITY PRIMARY KEY,
		name NVARCHAR(70),
		status_id SMALLINT NOT NULL,
		created_on DATETIME2(6) NOT NULL,
		updated_on DATETIME2(6),
		deleted_on DATETIME2(6)  
    )
END 

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='role' and xtype='U')
BEGIN
    CREATE TABLE [role] (
		role_id INT IDENTITY PRIMARY KEY,
		name NVARCHAR(70),
		status_id SMALLINT NOT NULL,
		created_on DATETIME2(6) NOT NULL,
		updated_on DATETIME2(6),
		deleted_on DATETIME2(6)  
    )
END

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='employee' and xtype='U')
BEGIN
    CREATE TABLE employee (
		employee_id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT NEWID(),
		name NVARCHAR(255),
		company_id INT NOT NULL,
		portal_id INT NOT NULL,
		role_id INT NOT NULL,
		status_id SMALLINT NOT NULL,
		email NVARCHAR(255) NOT NULL,
		fax NVARCHAR(60),
 		telephone NVARCHAR(12),
		username NVARCHAR(50) NOT NULL,
		password NVARCHAR(MAX) NOT NULL,
		created_on DATETIME2(6) NOT NULL,
		updated_on DATETIME2(6),
		deleted_on DATETIME2(6),
		last_login DATETIME2(6)    
    )
END 

--Create relationships

IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.object_id = object_id(N'[dbo].[fk_employee_company]') AND OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [employee] WITH CHECK ADD CONSTRAINT fk_employee_company FOREIGN KEY (company_id) REFERENCES [company](company_id) ON UPDATE CASCADE ON DELETE NO ACTION;
END
IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.object_id = object_id(N'[dbo].[fk_employee_portal]') AND OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [employee] WITH CHECK ADD CONSTRAINT fk_employee_portal FOREIGN KEY (portal_id) REFERENCES [portal](portal_id) ON UPDATE NO ACTION ON DELETE NO ACTION;
END
IF NOT EXISTS (SELECT * FROM sys.objects o WHERE o.object_id = object_id(N'[dbo].[fk_employee_role]') AND OBJECTPROPERTY(o.object_id, N'IsForeignKey') = 1)
BEGIN
	ALTER TABLE [employee] WITH CHECK ADD CONSTRAINT fk_employee_role FOREIGN KEY (role_id) REFERENCES [role](role_id) ON UPDATE NO ACTION ON DELETE NO ACTION;
END


--Create indexs
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('dbo.[employee]') AND NAME ='ix_employee_email')
BEGIN
	CREATE INDEX ix_employee_email ON [employee] (email ASC);
END
IF NOT EXISTS(SELECT * FROM sys.indexes WHERE object_id = object_id('dbo.[employee]') AND NAME ='ix_employee_username')
BEGIN
	CREATE INDEX ix_employee_username ON [employee] (username ASC);
END

--Create basic dictionary data
IF NOT EXISTS (SELECT * FROM company WHERE [name]='DGNET LTD')
BEGIN
 INSERT INTO [company] ([name],status_id,created_on) VALUES ('DGNET LTD',1, GETDATE());
END
IF NOT EXISTS (SELECT * FROM portal WHERE [name]='Computrabajo')
BEGIN
 INSERT INTO [portal] ([name],status_id,created_on) VALUES ('Computrabajo',1, GETDATE());
END
IF NOT EXISTS (SELECT * FROM [role] WHERE [name]='Developer')
BEGIN
 INSERT INTO [role] ([name],status_id,created_on) VALUES ('Developer',1, GETDATE());
END



