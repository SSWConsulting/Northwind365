using Northwind.Application.Common.Interfaces;

namespace Northwind.Infrastructure.Services;

public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;

    public int CurrentYear => DateTime.Now.Year;
}
