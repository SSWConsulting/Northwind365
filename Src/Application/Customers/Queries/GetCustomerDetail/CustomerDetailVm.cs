using AutoMapper;
using Northwind.Application.Common.Mappings;
using Northwind.Domain.Customers;

namespace Northwind.Application.Customers.Queries.GetCustomerDetail;

public class CustomerDetailVm : IMapFrom<Customer>
{
    public required string Id { get; init; }
    public required string Address { get; init; }
    public required string City { get; init; }
    public required string CompanyName { get; init; }
    public required string ContactName { get; init; }
    public required string ContactTitle { get; init; }
    public required string Country { get; init; }
    public required string Fax { get; init; }
    public required string Phone { get; init; }
    public required string PostalCode { get; init; }
    public required string Region { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Customer, CustomerDetailVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id.Value))
            .ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address.Line1))
            .ForMember(d => d.City, opt => opt.MapFrom(s => s.Address.City))
            .ForMember(d => d.Country, opt => opt.MapFrom(s => s.Address.Country))
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(s => s.Address.PostalCode))
            .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Address.Region))
            ;
    }

    /*
     * 
     * A good example of how AutoMapper can help.
     * 
    public static Expression<Func<Customer, CustomerDetailVm>> Projection
    {
        get
        {
            return customer => new CustomerDetailVm
            {
                Id = customer.CustomerId,
                Address = customer.Address,
                City = customer.City,
                CompanyName = customer.CompanyName,
                ContactName = customer.ContactName,
                ContactTitle = customer.ContactTitle,
                Country = customer.Country,
                Fax = customer.Fax,
                Phone = customer.Phone,
                PostalCode = customer.PostalCode,
                Region = customer.Region
            };
        }
    }

    public static CustomerDetailVm Create(Customer customer)
    {
        return Projection.Compile().Invoke(customer);
    }
    */
}