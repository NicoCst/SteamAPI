CREATE TABLE GamesList (
                           Id INT PRIMARY KEY IDENTITY,
                           UserId INT FOREIGN KEY REFERENCES Users(Id),
                           GameId INT FOREIGN KEY REFERENCES Games(Id),
                           PurchaseDate DATE,
                           PlayTime INT DEFAULT 0,
                           GiftId INT FOREIGN KEY REFERENCES Users(Id),
                           IsWhished BIT DEFAULT 0
);