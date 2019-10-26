--
-- File generated with SQLiteStudio v3.1.1 on sáb. oct. 26 13:48:26 2019
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: Customer
CREATE TABLE Customer (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Name VARCHAR (25) NOT NULL, Address varchar(35));

-- Table: Driver
CREATE TABLE Driver (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Name VARCHAR (25) NOT NULL);

-- Table: Hour
CREATE TABLE Hour (Place VARCHAR (25) NOT NULL, IsCanceled BOOLEAN NOT NULL DEFAULT (0), CustomerId INTEGER REFERENCES Customer (Id) NOT NULL, EntryTime TIME NOT NULL, ExitTime TIME NOT NULL, EntryDriverId INTEGER DEFAULT NULL, ExitDriverId INTEGER DEFAULT NULL, DayOfWeek INT NOT NULL, PRIMARY KEY (CustomerId, EntryTime, ExitTime, DayOfWeek));

-- Index: IDX_HOUR
CREATE INDEX IDX_HOUR ON Hour (CustomerId, EntryTime, ExitTime, DayOfWeek);

-- Index: IDX_HOUR_DAYOFWEEK
CREATE INDEX IDX_HOUR_DAYOFWEEK ON Hour (DayOfWeek);

-- Trigger: customer_hour_delete
CREATE TRIGGER customer_hour_delete 
BEFORE DELETE ON Customer
FOR EACH ROW
BEGIN
    DELETE FROM Hour WHERE CustomerId = OLD.Id;
END;

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;