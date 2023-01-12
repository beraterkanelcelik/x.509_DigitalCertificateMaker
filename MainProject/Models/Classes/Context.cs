using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MainProject.Models.Classes
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}