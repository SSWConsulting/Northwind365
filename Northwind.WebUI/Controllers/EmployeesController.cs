using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Northwind.Application.Employees.Queries.GetAllEmployees;

namespace Northwind.WebUI.Controllers
{
    public class EmployeesController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<EmployeeListVm>> GetAll()
        {
            return await Mediator.Send(new GetAllEmployeesQuery());
        }
    }
}
