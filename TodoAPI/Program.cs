//intialize our application with default settings and configurations.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builds the application based on the configuration and services registered in the container.
//So that the application can accept & handle HTTP requests.
var app = builder.Build(); //Line 1-8 Before building the App, Line8-14 After building the App.

app.MapGet("/", () => "Hello World!");

// Configure the HTTP request pipeline.
//Configure the middleware pipeline to handle HTTP requests. like AuthN, AUthZ, Logging, etc.


app.Run();