using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Shopping.Constants;
using Shopping.Models;

namespace EFDemo.Models;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
        // this empty constructor is needed because we need
        // to pass the options to the parent class
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = CommonConstants.connectionString;
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(connectionString)
                .UseLazyLoadingProxies(); // Enable lazy loading proxies
        }
    }

    public DbSet<Product> Product { get; set; }
    

}

