using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickStart.Models
{
    public class Employee
    {

        public int EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Title { get; set; }
        public string HomePhone { get; set; }
        public string Notes { get; set; }

        public string FullName
        {
            get
            {
                return String.Format("{0} {1}", FirstName, LastName);
            }
        }
        //public virtual ICollection<Order> Orders { get; set; }
    }
}