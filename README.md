# API_Project_2_34854673


Welcome to API Development
###


## About the Project:
The NWU Tech Trends project involves developing a RESTful API to manage and analyze telemetry data from automation processes. The goal is to track the time saved by each automation, associate this with costs, and categorize it by project and client. This API will enable CRUD operations on telemetry data and offer methods to calculate cumulative time and cost savings. Hosted on Azure, it will use .NET 8 and connect to a SQL Server database. Key features include endpoints for retrieving, creating, updating, and deleting telemetry records, as well as specific methods to aggregate and filter data based on project and client metrics.

# The database has four tables namely:
1.Client (Schema: Config) - Stores client information.
2.Project (Schema: Config) - Contains project details.
3.Process (Schema: Config) - Records information about processes.
4.JobTelemetry (Schema: Telemetry) - Captures telemetry data related to jobs.
      
      

# Microsoft Azure Integration


# Microsoft Azure database
1. Created an account on Microsoft Azure.
2. Established a monthly budget to manage credits effectively..
3. Created a resource group called API_RES.
4. Created a SQL server called apiserv and database called NWUTechTrends under the resource group API_RES.

# API Creation
1. Created the Web API: Developed the web API using Visual Studio.
2. Implemented Controllers: Added controllers to support CRUD operations for the tables in the Azure-hosted database.
3. Integrated Security: Configured authentication and authorization to ensure that only authorized users can access and utilize the API functionality.

# Hosting the API
1. Published the API from Visual Studio to Azure.
2. Deployed and tested the API from the Azure portal to ensure it's accessible and functioning correctly.


## Security:
1. Configured Authentication: Implemented authentication mechanisms to restrict access to the API.
2. This ensures that only authorized users can interact with the API endpoints.
3. Secured Credentials: Ensured that sensitive information such as credentials is not stored in the GitHub repository, following best practices for security and data protection.
4. Set Up Authorization: Applied authorization rules to control user permissions and access levels, ensuring that users can only access resources and perform actions they are permitted to.
5. Utilized HTTPS: Enabled HTTPS to encrypt data transmitted between clients and the API, protecting against eavesdropping and man-in-the-middle attacks.

# API Usage Guide

Welcome to the API Usage Guide for the NWU Tech Trends API. This guide will help you interact with the API to access and manage telemetry data from the NWUTechTrends database.

#Getting Started
Register an Account

    Visit the API registration page to create an account.
    This account is necessary to access and manipulate data in the database.

# Log In

    Use the credentials from registration to log in.
    Upon successful login, you will receive an authentication token.

# Using the API
Endpoints

# Client Data

    GET /api/clients: Retrieve a list of all clients.
    GET /api/clients/{id}: Retrieve details of a specific client.
    POST /api/clients: Create a new client entry.
    PATCH /api/clients/{id}: Update client details.
    DELETE /api/clients/{id}: Delete a client.

# Project Data

    GET /api/projects: Retrieve a list of all projects.
    GET /api/projects/{id}: Retrieve details of a specific project.
    POST /api/projects: Create a new project entry.
    PATCH /api/projects/{id}: Update project details.
    DELETE /api/projects/{id}: Delete a project.

# Process Data

    GET /api/processes: Retrieve a list of all processes.
    GET /api/processes/{id}: Retrieve details of a specific process.
    POST /api/processes: Create a new process entry.
    PATCH /api/processes/{id}: Update process details.
    DELETE /api/processes/{id}: Delete a process.

# Telemetry Data

    GET /api/telemetry: Retrieve a list of all telemetry entries.
    GET /api/telemetry/{id}: Retrieve details of a specific telemetry entry.
    POST /api/telemetry: Create a new telemetry entry.
    PATCH /api/telemetry/{id}: Update an existing telemetry entry.
    DELETE /api/telemetry/{id}: Delete a telemetry entry.
    GET /api/telemetry/savings/project: Calculate cumulative time and cost savings for a specific project.
    GET /api/telemetry/savings/client: Calculate cumulative time and cost savings for a specific client.

# Authentication

Include the authentication token received upon login in the Authorization header for each request.

# Example Workflow

    Register an account.
    Log in to receive the authentication token.
    Use the token in the Authorization header for subsequent requests.
    Access client, project, process, and telemetry data using the provided endpoints.
    Make GET, POST, PATCH, or DELETE requests as needed.

# Security Notes

    The API is secured using token-based authentication.
    Only registered users with valid tokens have access to the API endpoints.
    The server hosting the database is secured, and sensitive details are not exposed.

# Resource Group

Azure Portal - API_RES Resource Group

Link to API
https://portal.azure.com/#@nwuac.onmicrosoft.com/resource/subscriptions/a3be198e-114b-4110-bfff-81282fa5a6ed/resourceGroups/API_RES/overview
