using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
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
        public DbSet<Orders> Orders { get; set; }

        public DbSet<OrderDetails> Order_Details { get; set; }
    }
}