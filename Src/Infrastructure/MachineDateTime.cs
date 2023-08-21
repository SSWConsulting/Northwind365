using Northwind.Application.Common.Interfaces;

namespace Northwind.Infrastructure;

public class MachineDateTime : IDateTime
{
    public DateTime Now => DateTime.Now;

    public int CurrentYear => DateTime.Now.Year;
}
