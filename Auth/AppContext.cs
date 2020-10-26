using Auth.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
