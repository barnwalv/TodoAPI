//intialize our application with default settings and configurations.
using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add DI - add services to the container.
// Register services in the container. like DBContext, Repositories, Services, etc.

// AddDbContext<TContext> method registers the context with the DI container.
builder.Services.AddDbContext<TodoDbContext>(options => options.UseInMemoryDatabase("TodoList")); //DbName: TodoList.




//builds the application based on the configuration and services registered in the container.
//So that the application can accept & handle HTTP requests.
var app = builder.Build(); //Line 1-8 Before building the App, Line8-14 After building the App.




app.MapGet("/", () => "Hello World!");

// Configure the HTTP Request Pipeline - UseMethod.
// Configure the middleware pipeline to handle HTTP requests. like AuthN, AUthZ, Logging, etc.
// Middleware is a piece of software that handles an HTTP request or response.
// Middleware is executed in the order it is added to the pipeline.
// Middleware can be added to the pipeline using the Use method.
// Use method adds a middleware to the pipeline.

app.MapGet("/todoitems", async(TodoDbContext db) =>
{
    var todoItems = await db.TodoItems.ToListAsync();
    return Results.Ok(todoItems);
});

app.MapGet("/todoitems/{id}", async(TodoDbContext db, int id) =>
{
    var todoItemById = await db.TodoItems.FindAsync(id);
    return Results.Ok(todoItemById);
});

app.MapPost("/todoitems", async(TodoDbContext db, TodoItem todoItem) =>
{
    db.TodoItems.Add(todoItem);
    await db.SaveChangesAsync();
    return Results.Created($"/todoitems/{todoItem.Id}", todoItem);
});

app.MapPut("/todoitems/{id}", async(TodoDbContext db, int id, TodoItem todoItem) =>
{
    if (id != todoItem.Id) return Results.BadRequest();

    var todo = await db.TodoItems.FindAsync(id);
    if (todo == null) return Results.NotFound();

    todo.Name = todoItem.Name;
    todo.IsComplete = todoItem.IsComplete;

    //db.Entry(todoItem).State = EntityState.Modified;
    await db.SaveChangesAsync();
    return Results.Ok("Todo item updated successfully.");
});

app.MapDelete("/todoitems/{id}", async(TodoDbContext db, int id) =>
{
    var todoItem = await db.TodoItems.FindAsync(id);
    if (todoItem == null)
    {
        return Results.NotFound();
    }

    db.TodoItems.Remove(todoItem);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.Run();