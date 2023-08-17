CREATE DATABASE TaskManager
USE TaskManager

CREATE TABLE Tasks (
    Id INT identity(1,1) PRIMARY KEY,
    Name VARCHAR(250) NOT NULL,
    IsCompleted BIT NOT NULL,
	Created_at DATE,
);

SELECT * FROM Tasks