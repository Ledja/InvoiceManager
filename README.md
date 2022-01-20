# InvoiceManager
.NET Testing Assignment

This repository contains a basic Web API implemented in ASP.NET 5 and secured with a custom ApiKey.

To run this:
Clone the repo: git clone https://github.com/Ledja/InvoiceManager.git

This API is documented by using Swagger.
Find the app secret key within appsettings.Deveopment.json configuration file.
Type dotnet run in a terminal window to launch the Web API.
Point your browser to https://localhost:5001/swagger/index.html to get the list of all endpoints.

The "dummy" endpoint is an entry point to the implementation of the self-refreshing cache task.

Requirements:
.NET 5 installed on your machine

PS: I went with Web API solution because I have never worked on a ASP.NET Core MVC project and it would have take me a lot of time only to study a little bit about it.


