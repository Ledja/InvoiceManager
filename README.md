# Requirements

Create a simple ASP.NET Core MVC application in C# to manage invoices in any database system. The application should have the following features:
* creating/editing an invoice,
* adding/removing invoice items.

Another part of the application is to create a simple API. Access to API should be restricted by a secret key which is sent as a header value in the request.

Please prepare 3 endpoints which have following functionality:
* getting collection of unpaid invoices,
* paying invoice (changing status to `paid`),
* editing invoice (PATCH request).



# InvoiceManager Implementation 

This repository contains a basic Web API implemented in ASP.NET 5 and secured with a custom ApiKey.

To run this:
Clone the repo: git clone https://github.com/Ledja/InvoiceManager.git

This API is documented by using Swagger.
Find the app secret key within appsettings.Deveopment.json configuration file.
Type dotnet run in a terminal window to launch the Web API.
Point your browser to https://localhost:5001/swagger/index.html to get the list of all endpoints.


Requirements:
.NET 5 installed on your machine


