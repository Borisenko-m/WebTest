using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebTest.Classes.DBSync.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Application> Аpplications { get; set; }

    }
}
