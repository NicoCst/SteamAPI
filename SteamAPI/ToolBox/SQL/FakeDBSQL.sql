-- Insertion dans la table Users
INSERT INTO Users (FirstName, LastName, NickName, Email, [Password], IsDev, Wallet)
VALUES ('Jean', 'Dupont', 'jdupont', 'jean.dupont@example.com', 'password123', 0, 100.00),
       ('Marie', 'Durand', 'mdurand', 'marie.durand@example.com', 'password123', 1, 200.00),
       ('Pierre', 'Polo', 'ppolo', 'pierre.polo@example.com', 'password123', 0, 300.00),
       ('Sophie', 'Leroy', 'sleroy', 'sophie.leroy@example.com', 'password123', 0, 400.00),
       ('Paul', 'Martin', 'pmartin', 'paul.martin@example.com', 'password123', 1, 500.00);

-- Insertion dans la table Friends
INSERT INTO Friends (UserId, FriendId, [Date], Validate)
VALUES (1, 2, GETDATE(), 1),
       (1, 3, GETDATE(), 0),
       (2, 3, GETDATE(), 1),
       (2, 4, GETDATE(), 0),
       (3, 5, GETDATE(), 1);

-- Insertion dans la table Games
INSERT INTO Games ([Name], Genre, [Version], UserDevId)
VALUES ('Game 1', 'Action', '1.0', 1),
       ('Game 2', 'Adventure', '1.0', 2),
       ('Game 3', 'RPG', '1.0', 1),
       ('Game 4', 'Strategy', '1.0', 2),
       ('Game 5', 'Simulation', '1.0', 1);

-- Insertion dans la table PricesList
INSERT INTO PricesList (PriceDate, Price, GameId)
VALUES (GETDATE(), 19.99, 1),
       (GETDATE(), 29.99, 2),
       (GETDATE(), 39.99, 3),
       (GETDATE(), 49.99, 4),
       (GETDATE(), 59.99, 5);

-- Insertion dans la table GamesList
INSERT INTO GamesList (UserId, GameId, PurchaseDate, PlayTime, GiftId, IsWhished)
VALUES (1, 1, GETDATE(), 10, NULL, 0),
       (2, 2, GETDATE(), 20, NULL, 0),
       (3, 3, GETDATE(), 30, NULL, 0),
       (4, 4, GETDATE(), 40, 4, 0),
       (5, 5, GETDATE(), 50, 5, 0);
