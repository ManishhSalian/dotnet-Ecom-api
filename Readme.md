# BagAPI

## Project Description
BagAPI is a RESTful API designed to manage and organize bags and their contents. It allows users to create, read, update, and delete bags and items within those bags.

## Installation
To install and run the BagAPI locally, follow these steps:

1. Clone the repository:
    ```bash
    git clone https://github.com/yourusername/BagAPI.git
    ```
2. Navigate to the project directory:
    ```bash
    cd BagAPI
    ```

## Configuration
### Configure the Database
Update the `appsettings.json` file with your database connection string:
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

### Apply Migrations
Run the following commands to apply the database migrations:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Running the Application
Run the application using the following command:
```bash
dotnet run
```
The API will be available at `https://localhost:5001` or `http://localhost:5000`.

## API Endpoints
### User Endpoints
- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `POST /api/users` - Create a new user
- `PUT /api/users/{id}` - Update an existing user
- `DELETE /api/users/{id}` - Delete a user
- `POST /api/users/login` - Authenticate a user and get a JWT token

### Role Endpoints
- `GET /api/roles` - Get all roles
- `GET /api/roles/{id}` - Get role by ID
- `POST /api/roles` - Create a new role
- `PUT /api/roles/{id}` - Update an existing role
- `DELETE /api/roles/{id}` - Delete a role

### Product Endpoints
- `GET /api/products` - Get all products
- `GET /api/products/{id}` - Get product by ID
- `POST /api/products` - Create a new product
- `PUT /api/products/{id}` - Update an existing product
- `DELETE /api/products/{id}` - Delete a product

### Category Endpoints
- `GET /api/categories` - Get all categories
- `GET /api/categories/{id}` - Get category by ID
- `POST /api/categories` - Create a new category
- `PUT /api/categories/{id}` - Update an existing category
- `DELETE /api/categories/{id}` - Delete a category

### Stock Endpoints
- `GET /api/stocks` - Get all stocks
- `GET /api/stocks/{id}` - Get stock by ID
- `POST /api/stocks` - Create a new stock
- `PUT /api/stocks/{id}` - Update an existing stock
- `DELETE /api/stocks/{id}` - Delete a stock

### Cart Endpoints
- `GET /api/carts` - Get all carts
- `GET /api/carts/{id}` - Get cart by ID
- `POST /api/carts` - Create a new cart
- `PUT /api/carts/{id}` - Update an existing cart
- `DELETE /api/carts/{id}` - Delete a cart

### CartItem Endpoints
- `GET /api/cartitems` - Get all cart items
- `GET /api/cartitems/{id}` - Get cart item by ID
- `POST /api/cartitems` - Create a new cart item
- `PUT /api/cartitems/{id}` - Update an existing cart item
- `DELETE /api/cartitems/{id}` - Delete a cart item

### Review Endpoints
- `GET /api/reviews` - Get all reviews
- `GET /api/reviews/{id}` - Get review by ID
- `POST /api/reviews` - Create a new review
- `PUT /api/reviews/{id}` - Update an existing review
- `DELETE /api/reviews/{id}` - Delete a review

### Order Endpoints
- `GET /api/orders` - Get all orders
- `GET /api/orders/{id}` - Get order by ID
- `POST /api/orders` - Create a new order
- `PUT /api/orders/{id}` - Update an existing order
- `DELETE /api/orders/{id}` - Delete an order

### Payment Endpoints
- `GET /api/payments` - Get all payments
- `GET /api/payments/{id}` - Get payment by ID
- `POST /api/payments` - Create a new payment
- `PUT /api/payments/{id}` - Update an existing payment
- `DELETE /api/payments/{id}` - Delete a payment

### Shipping Endpoints
- `GET /api/shippingdetails` - Get all shippings
- `GET /api/shippingdetails/{id}` - Get shipping by ID
- `POST /api/shippingdetails` - Create a new shipping
- `PUT /api/shippingdetails/{id}` - Update an existing shipping
- `DELETE /api/shippingdetails/{id}` - Delete a shipping

## Authentication
The API uses JWT tokens for authentication. To access protected endpoints, include the token in the Authorization header as follows:
```http
Authorization: Bearer <your_token>
```

## Migration Commands
- Creating a migration file: `dotnet ef migrations add InitialCreate`
- Attributing the migration to the database: `dotnet ef add AddAttributesToModels`
- Updating the database: `dotnet ef database update`
- Removing the last migration: `dotnet ef migrations remove`
- Update to a specific migration: `dotnet ef database update <Id of migration>`
- List of migrations: `dotnet ef migrations list`
- Add table to database: `dotnet ef migrations add AddProductTable`

## Postman Documentation
For detailed API documentation and testing, you can use the Postman collection available [here](https://documenter.getpostman.com/view/40139824/2sAYJ1mhbX).



#   d o t n e t - E c o m - a p i  
 