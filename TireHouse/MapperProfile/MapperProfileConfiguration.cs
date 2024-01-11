using AutoMapper;
using TireHouse.DTO;
using TireHouse.Models;

namespace TireHouse.MapperProfile;

public class MapperProfileConfiguration : Profile
{
    public MapperProfileConfiguration() 
    {
        CreateMap<Category, CategoryModel>().ReverseMap();
        CreateMap<Customer, CustomerModel>().ReverseMap();
        CreateMap<Employee, EmployeeModel>().ReverseMap();
        CreateMap<Product, ProductModel>().ReverseMap();
    }
}
