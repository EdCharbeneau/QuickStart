using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QuickStart.Models
{
    public class NorthwindDBContext : DbContext
    {
        public NorthwindDBContext() : base("NorthwindDB")
        { }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}