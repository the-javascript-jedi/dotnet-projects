using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        // constructor for the class
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        //property for dbset for users -(eg name of table)
        public DbSet<AppUser> Users { get; set; }
    }
}