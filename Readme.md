# BagAPI

## Project Description
BagAPI is a robust and user-friendly RESTful API designed to efficiently manage and organize bags and their contents. The API provides comprehensive functionality to create, retrieve, update, and delete bags and the items within them.

---

## Installation
To install and run BagAPI locally, follow these steps:

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/yourusername/BagAPI.git
    ```
2. **Navigate to the Project Directory**:
    ```bash
    cd BagAPI
    ```

---

## Configuration
### Database Configuration
Update the `appsettings.json` file with your database connection details:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=your_server;Database=BagDB;User Id=your_user;Password=your_password;"
  },
  "JwtSettings": {
    "SecretKey": "your_secret_key",
    "Issuer": "your_issuer",
    "Audience": "your_audience"
  }
}
```

### Applying Migrations
1. Generate the database schema:
    ```bash
    dotnet ef migrations add InitialCreate
    ```
2. Apply the migrations to your database:
    ```bash
    dotnet ef database update
    ```

---

## Running the Application
Start the application using the following command:
```bash
dotnet run
```
Once started, the API will be available at:
- `https://localhost:5001`
- `http://localhost:5000`

---

## API Endpoints
### User Management
- **`GET /api/users`** - Retrieve all users
- **`GET /api/users/{id}`** - Retrieve a user by ID
- **`POST /api/users`** - Create a new user
- **`PUT /api/users/{id}`** - Update an existing user
- **`DELETE /api/users/{id}`** - Delete a user
- **`POST /api/users/login`** - Authenticate a user and obtain a JWT token

### Role Management
- **`GET /api/roles`** - Retrieve all roles
- **`GET /api/roles/{id}`** - Retrieve a role by ID
- **`POST /api/roles`** - Create a new role
- **`PUT /api/roles/{id}`** - Update an existing role
- **`DELETE /api/roles/{id}`** - Delete a role

### Bag and Content Management
#### Products
- **`GET /api/products`** - Retrieve all products
- **`GET /api/products/{id}`** - Retrieve a product by ID
- **`POST /api/products`** - Create a new product
- **`PUT /api/products/{id}`** - Update an existing product
- **`DELETE /api/products/{id}`** - Delete a product

#### Categories
- **`GET /api/categories`** - Retrieve all categories
- **`GET /api/categories/{id}`** - Retrieve a category by ID
- **`POST /api/categories`** - Create a new category
- **`PUT /api/categories/{id}`** - Update an existing category
- **`DELETE /api/categories/{id}`** - Delete a category

#### Stock
- **`GET /api/stocks`** - Retrieve all stocks
- **`GET /api/stocks/{id}`** - Retrieve stock by ID
- **`POST /api/stocks`** - Add stock information
- **`PUT /api/stocks/{id}`** - Update stock information
- **`DELETE /api/stocks/{id}`** - Delete stock information

### Cart Management
- **`GET /api/carts`** - Retrieve all carts
- **`GET /api/carts/{id}`** - Retrieve a cart by ID
- **`POST /api/carts`** - Create a new cart
- **`PUT /api/carts/{id}`** - Update an existing cart
- **`DELETE /api/carts/{id}`** - Delete a cart

#### Cart Items
- **`GET /api/cartitems`** - Retrieve all cart items
- **`GET /api/cartitems/{id}`** - Retrieve a cart item by ID
- **`POST /api/cartitems`** - Add an item to a cart
- **`PUT /api/cartitems/{id}`** - Update a cart item
- **`DELETE /api/cartitems/{id}`** - Remove a cart item

### Reviews
- **`GET /api/reviews`** - Retrieve all reviews
- **`GET /api/reviews/{id}`** - Retrieve a review by ID
- **`POST /api/reviews`** - Add a review
- **`PUT /api/reviews/{id}`** - Update a review
- **`DELETE /api/reviews/{id}`** - Delete a review

### Orders
- **`GET /api/orders`** - Retrieve all orders
- **`GET /api/orders/{id}`** - Retrieve an order by ID
- **`POST /api/orders`** - Place a new order
- **`PUT /api/orders/{id}`** - Update an order
- **`DELETE /api/orders/{id}`** - Cancel an order

### Payments
- **`GET /api/payments`** - Retrieve all payments
- **`GET /api/payments/{id}`** - Retrieve payment details by ID
- **`POST /api/payments`** - Process a payment
- **`PUT /api/payments/{id}`** - Update payment details
- **`DELETE /api/payments/{id}`** - Delete payment details

### Shipping
- **`GET /api/shippingdetails`** - Retrieve all shipping details
- **`GET /api/shippingdetails/{id}`** - Retrieve shipping details by ID
- **`POST /api/shippingdetails`** - Add new shipping details
- **`PUT /api/shippingdetails/{id}`** - Update shipping details
- **`DELETE /api/shippingdetails/{id}`** - Delete shipping details

---

## Authentication
The API uses JWT tokens for authentication. To access protected endpoints, include the token in the Authorization header:
```http
Authorization: Bearer <your_token>
```

---

## Migration Commands
- **Create a migration file**: `dotnet ef migrations add <MigrationName>`
- **Apply a migration**: `dotnet ef database update`
- **Remove the last migration**: `dotnet ef migrations remove`
- **List all migrations**: `dotnet ef migrations list`
- **Add a new table**: `dotnet ef migrations add <TableName>`

---

## Postman Documentation
For detailed API documentation and testing, use the Postman collection available [here](https://documenter.getpostman.com/view/40139824/2sAYJ1mhbX).

---

### Enjoy seamless bag management with BagAPI!

