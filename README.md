# ASteamAPI - Steam-Like Backend API

ASteamAPI is a backend API built with ASP.NET, providing Steam-like functionalities for user management, game-related operations, and authentication. This README provides an overview of the project structure, controllers, and key functionalities.

## Controllers

### UserController

#### `GET: api/User/GetAllFriends/{id}`

Retrieve a list of friends for a user with the specified ID.

#### `GET: api/User/GetFriendsRequest/{id}`

Retrieve a list of friend requests for a user with the specified ID.

#### `POST: api/User/Register`

Create a new user with the provided registration information.

#### `POST: api/User/AddFriend`

Send a friend request to another user.

#### `PUT: api/User/{id}`

Update user information for the specified user ID.

#### `PUT: api/User/AddMoney`

Add money to the user's account.

#### `PATCH: api/User/AcceptFriend`

Accept a friend request from another user.

#### `DELETE: api/User/DeleteFriend`

Delete a friend request.

### GameController

#### `POST: api/Game/CreateGame`

Create a new game with the provided information.

#### `PUT: api/Game/{id}`

Update game information for the specified game ID.

#### `GET: api/Game/GetAllMyGames/{id}`

Retrieve a list of games owned by a user with the specified ID.

#### `POST: api/Game/BuyGame`

Purchase a game.

#### `DELETE: api/Game/RefundGame`

Request a refund for a purchased game.

#### `PATCH: api/Game/SetToWishlist`

Add a game to the user's wishlist.

#### `POST: api/Game/SetNewPrice`

Set a new price for a game.

### AuthController

#### `POST: api/Auth`

Authenticate a user and generate a JWT token.

## Project Structure

- **BLL (Business Logic Layer):**
  - Interfaces for services.
  - Models for forms and DTOs.

- **DAL (Data Access Layer):**
  - Entities representing database objects.

- **Controllers:**
  - UserController: Manages user-related operations.
  - GameController: Manages game-related operations.
  - AuthController: Handles user authentication.

## Usage

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/ASteamAPI.git
   cd ASteamAPI

2. Set up your environment variables and configure the database.

### Run the application

```bash
dotnet run
```

Access the API endpoints using the specified routes.

### Dependencies

* ASP.NET Core
* Entity Framework Core
* Microsoft.AspNetCore.Mvc
* Microsoft.AspNetCore.Authorization

### Authentication

Authentication is handled through the AuthController. Users can obtain a JWT token by providing valid login credentials. Developers (IsDev == 1) receive a special token.

### Contributing

Feel free to contribute to the project by submitting pull requests or reporting issues.

### License

This project is licensed under the MIT License.
