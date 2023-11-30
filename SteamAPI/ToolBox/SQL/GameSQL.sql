CREATE TABLE Games (
                       Id INT PRIMARY KEY IDENTITY,
    [Name] VARCHAR(50),
    Genre VARCHAR(50),
    [Version] VARCHAR(80),
    UserDevId INT FOREIGN KEY REFERENCES Users(Id) NOT NULL
    );