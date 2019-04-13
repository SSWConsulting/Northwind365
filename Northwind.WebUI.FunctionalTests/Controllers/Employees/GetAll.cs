using System.Net.Http;
using System.Threading.Tasks;
using Northwind.Application.Employees.Queries.GetAllEmployees;
using Northwind.WebUI.FunctionalTests.Common;
using Xunit;
using Shouldly;

namespace Northwind.WebUI.FunctionalTests.Controllers.Employees
{
    public class GetAll : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetAll(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Handle_ReturnsEmployeeListViewModel()
        {
            var response = await _client.GetAsync("/api/employees/getall");

            var vm = await Utilities.GetResponseContent<EmployeeListVm>(response);

            vm.Employees.Count.ShouldBe(9);
        }
    }
}
