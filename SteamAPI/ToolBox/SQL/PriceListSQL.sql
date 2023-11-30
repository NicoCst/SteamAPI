CREATE TABLE PricesList(
                           Id INT PRIMARY KEY IDENTITY,
                           PriceDate DATETIME,
                           Price FLOAT,
                           GameId INT FOREIGN KEY REFERENCES Games(Id) NOT NULL
);