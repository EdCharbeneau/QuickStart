using System;
namespace QuickStart.Models
{
    public class MonthlySalesByEmployeeViewModel
    {
        public int? EmployeeID { get; set; }
        public decimal? EmployeeSales { get; set; }
        public DateTime Date { get; set; }
    }
}