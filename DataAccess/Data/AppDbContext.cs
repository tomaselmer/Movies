﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Data
{

    public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Movie> Movies { get; set; }
            public DbSet<User> Users { get; set; }
        }
 }

