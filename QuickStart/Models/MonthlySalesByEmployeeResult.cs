using System;
namespace QuickStart.Models
{

    public class MonthlySalesByEmployeeResult
    {
        public int? EmployeeID { get; set; }
        public decimal? EmployeeSales { get; set; }
        public DateTime Date { get; set; }
    }
}