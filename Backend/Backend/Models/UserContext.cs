﻿using Microsoft.EntityFrameworkCore;

namespace Backend.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) :base(options)
        {
           
        }
        public DbSet<User> Users { get; set; }
    }
}
