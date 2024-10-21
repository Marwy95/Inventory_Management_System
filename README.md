
# Inventory Management System
## Overview
The Inventory Management System is a backend API developed using .NET technologies, designed to manage product inventories efficiently. It includes features such as product management, inventory transactions, reporting, and notifications for low-stock items.

## Technologies Used
ASP.NET Core Web API

Entity Framework Core
RabbitMQ
CQRS (Command Query Responsibility Segregation)
Vertical Slicing Architecture
JWT Authentication
Hangfire for Background Jobs

## Features
### Product Management
 Add, update, delete, and retrieve products.
 List all products in inventory.

### Inventory Transactions
Add, remove Transaction.
Transfer Stock between warehouses -in progress

### Reporting
Generate low stock reports.
Generate transaction history.

### Notifications via Emails
Alerts for low-stock products via RabbitMQ.
Scheduled background jobs for daily low-stock checks and transaction archiving.

### Role-Based Authorization
Different access levels for users (e.g., Admin access for certain features).

## API Endpoints
### Product Management
POST /AddproductEndPoint - Create a new product.
PUT /UpdateProductEndPoint/{id} - Update an existing product.
DELETE /DeleteProductEndPoint/{id} - Delete a product.
GET /GetProductDetailsEndPoint/{id} - Get product details.
GET /GetAllProductsEndPoint - List all products.

### Inventory Transactions
POST /AddStockTransactionEndPoint - Add stock.
POST /RemoveStockTransactionEndPoint - Remove stock.

### Reporting
GET /CreateLowStockReportEndPoint - Get low stock report.
Post /CreateTransactionHistoryReportEndPoint - Get transaction history.

### User
Post /LoginUserEndPoint - Login User.
Post /RegisterUserEndPoint - Register User.


