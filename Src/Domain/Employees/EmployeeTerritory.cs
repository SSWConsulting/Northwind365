﻿namespace Northwind.Domain.Employees;

public class EmployeeTerritory
{
    public int EmployeeId { get; set; }
    public string TerritoryId { get; set; }

    public Employee Employee { get; set; }
    public Territory Territory { get; set; }
}
