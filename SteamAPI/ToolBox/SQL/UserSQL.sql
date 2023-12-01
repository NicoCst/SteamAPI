CREATE TABLE Users (
                       Id INT PRIMARY KEY IDENTITY ,
                       FirstName VARCHAR(50) NOT NULL,
                       LastName VARCHAR(50) NOT NULL,
                       NickName VARCHAR(50) UNIQUE NOT NULL,
                       Email VARCHAR(50) UNIQUE NOT NULL,
    [Password] VARCHAR(100) NOT NULL,
    IsDev BIT DEFAULT 0,
    IsPlaying BIT DEFAULT 0,
    Wallet MONEY DEFAULT 0
    );