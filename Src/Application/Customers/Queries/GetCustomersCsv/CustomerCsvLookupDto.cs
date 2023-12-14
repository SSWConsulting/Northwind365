using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Queries.GetCustomersCsv;

public class CustomerCsvLookupDto : IMapFrom<Customer>
{
    public required string Id { get; init; }
    public required string Name { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerCsvLookupDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.Name, opt => opt.MapFrom(s => s.CompanyName));
    }
}