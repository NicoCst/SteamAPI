CREATE TABLE Friends (
                         UserId INT FOREIGN KEY REFERENCES Users(Id),
                         FriendId INT FOREIGN KEY REFERENCES Users(Id),
    [Date] DATETIME,
                         Validate BIT DEFAULT 0,
                         CONSTRAINT PK_Friends PRIMARY KEY (UserId, FriendId)
);