﻿using Microsoft.EntityFrameworkCore;
using MyFinances.API.Models;

namespace MyFinances.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
    }
}
