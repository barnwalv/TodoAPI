﻿using Microsoft.EntityFrameworkCore;
using TodoAPI.Model;

namespace TodoAPI.Data
{
    public class TodoDbContext: DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
