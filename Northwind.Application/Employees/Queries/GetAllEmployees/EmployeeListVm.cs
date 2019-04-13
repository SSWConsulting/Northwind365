using System.Collections.Generic;

namespace Northwind.Application.Employees.Queries.GetAllEmployees
{
    public class EmployeeListVm
    { 
        public EmployeeListVm()
        {
            Employees = new List<EmployeeListItem>();
        }

        public List<EmployeeListItem> Employees { get; set; }
    }
}