# Cash Flow Management System

## Overview

The Cash Flow Management System is designed to help traders manage their daily cash flow with entries (debits and credits) and provides a report that summarizes the consolidated daily balance. This project includes a Posting Control Service and a web interface for users to interact with the system.

## Features

- **User Authentication**: Secure login and logout functionality.
- **Transaction Management**: Users can create, view, and manage their transactions.
- **Daily Reports**: Generate daily reports that show total credits, debits, and balance.
- **Error Handling**: Robust error handling for a smooth user experience.
- **API Services**: RESTful APIs for managing transactions and generating reports.

## Requirements

- .NET 5.0 SDK
- SQL Server or SQL Server Express
- Visual Studio 2019 or later (optional, for development)

## Setup Instructions

### Database Setup

1. **Create Database**:
   - Create a new database in SQL Server named `CashFlowManagementDB`.

2. **Apply Migrations**:
   - Open the solution in Visual Studio.
   - Open the `Package Manager Console` (Tools > NuGet Package Manager > Package Manager Console).
   - Run the following commands:
     ```sh
     Update-Database
     ```

### Configuration

1. **Update `appsettings.json`**:
   - Navigate to `CashFlowManagement.Web` and `PostingControlService.Api` projects.
   - Update the `appsettings.json` file with your SQL Server connection string and JWT settings:
     ```json
     {
       "ConnectionStrings": {
         "DefaultConnection": "Server=your_server_name;Database=CashFlowManagementDB;Trusted_Connection=True;"
       },
       "Jwt": {
         "Key": "your_jwt_secret_key",
         "Issuer": "your_jwt_issuer",
         "Audience": "your_jwt_audience"
       }
     }
     ```

### Running the Application

1. **Start the API Service**:
   - Set `PostingControlService.Api` as the startup project.
   - Run the project to start the API service.

2. **Start the Web Application**:
   - Set `CashFlowManagement.Web` as the startup project.
   - Run the project to start the web application.

### Testing

1. **API Testing**:
   - The API can be tested using Swagger UI available at `https://localhost:9001/swagger/index.html`.

2. **Web Interface**:
   - Access the web application at `https://localhost:5001`.
   - Login using the credentials provided in the database seeding script or register a new user.
   - Create, view, and manage transactions.
   - Generate daily reports.

### MSTest

- MSTest projects are included for both the API and the web application.
- To run tests:
  1. Open the Test Explorer in Visual Studio.
  2. Run all tests to ensure the system is working correctly.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request with your changes.

## License

This project is licensed under the MIT License. See the LICENSE file for more information.

## Contact

For questions or support, please open an issue in the GitHub repository.

