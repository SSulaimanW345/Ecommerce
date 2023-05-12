# Ecommerce

This is a sample application built with .NET Core 5, MySQL database, and simple JavaScript, HTML, and CSS for the front-end.

## Prerequisites
* .NET Core 5 SDK
* MySQL Server (or any other MySQL-compatible database)

## Getting Started
To get a local copy of the project up and running, follow these steps:

1. Clone the repository:
```
git clone https://github.com/your-username/your-repository.git
```
2. Change to the project directory:
```
cd your-repository
```
3.Set up the database:

* Create a MySQL database.
* Update the connection string in the **appsettings.json** file with your database credentials.

4.Run the EF Core migrations to create the database schema:
```
dotnet ef migrations add InitialMigration
dotnet ef database update
```
5.Build and run the application:
```
dotnet run
```
## Features
* Role-based authentication (Admin and User):
Admin users have access to administrative features and can manage user roles and permissions.
User authentication is implemented to provide access control and protect sensitive data.
* Shopping cart implemented:

Users can add products to their shopping cart.
Users can view and manage items in their shopping cart.
Checkout functionality is available for completing purchases.


## Technologies Used
* .NET Core 5
*  MySQL
*  JavaScript
* HTML
* CSS

## Contributing
Contributions are welcome! If you find any issues or have suggestions, please create a new issue or submit a pull request.

## Contact
For any questions or inquiries, feel free to contact me at s.s.waleed.e60@gmail.com.
