# Shopping Cart API

This is a simple shopping cart API built with ASP.NET Core and Entity Framework Core.

## Features

- User authentication with JWT tokens
- Create a new shopping cart for a user
- Retrieve a user's shopping cart
- Add items to a shopping cart
- Remove items from a shopping cart
- Checkout (place an order)

## Prerequisites

Ensure you have the following installed:

- [.NET 8](https://dotnet.microsoft.com/)
- [SQL Server or SQLite](https://www.sqlite.org/download.html)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)

## Setup Instructions

### 1. Clone the Repository

```sh
git clone https://github.com/ZinPhyo/ShoppingCart.git
cd shopping-cart-api
```

### 2. Configure Database

By default, the application is set up with Entity Framework Core using SQLite or SQL Server.

If you already have a migration file, simply update the database:

```sh
dotnet ef database update
```

If you don't have a migration file, create and apply the migration:

```sh
dotnet ef migrations add InitialCreate
```

```sh
dotnet ef database update
```

### 3. Run the Application

```sh
dotnet run
```

The API will start at `http://localhost:5000` or `https://localhost:5001`.

## Authentication (JWT Token)

Before accessing the API, you must obtain a JWT token.

### **1. Get JWT Token**

```sh
POST http://localhost:5000/api/auth/token
```

#### **Response**

```json
{
  "token": "your-jwt-token-here"
}
```

Copy the token and use it in the `Authorization` header for subsequent API requests.

## Testing the API

You can test the API using tools like [Postman](https://www.postman.com/) or `curl`.

### 2. Get Cart for a User

```sh
GET http://localhost:5000/api/cart/{userId}
Authorization: Bearer your-jwt-token
```

Example:

```sh
curl -X GET http://localhost:5000/api/cart/1 -H "Authorization: Bearer your-jwt-token"
```

### 3. Add Item to Cart

```sh
POST http://localhost:5000/api/cart/add
Authorization: Bearer your-jwt-token
Content-Type: application/json

{
    "userId": 1,
    "productId": 2,
    "quantity": 1
}
```

Example:

```sh
curl -X POST http://localhost:5000/api/cart/add -H "Authorization: Bearer your-jwt-token" -H "Content-Type: application/json" -d '{"userId":1,"productId":2,"quantity":1}'
```

### 4. Remove Item from Cart

```sh
DELETE http://localhost:5000/api/cart/remove
Authorization: Bearer your-jwt-token
Content-Type: application/json

{
    "userId": 1,
    "productId": 2
}
```

Example:

```sh
curl -X DELETE http://localhost:5000/api/cart/remove -H "Authorization: Bearer your-jwt-token" -H "Content-Type: application/json" -d '{"userId":1,"productId":2}'
```

### 5. Checkout

```sh
POST http://localhost:5000/api/cart/checkout
Authorization: Bearer your-jwt-token
Content-Type: application/json

{
    "userId": 1
}
```

Example:

```sh
curl -X POST http://localhost:5000/api/cart/checkout -H "Authorization: Bearer your-jwt-token" -H "Content-Type: application/json" -d '{"userId":1}'
```

## Database Seeding

The application seeds initial data into the database:

- **Users**: `user1`, `user2`
- **Products**: `Laptop`, `Smartphone`, `Headphones`


