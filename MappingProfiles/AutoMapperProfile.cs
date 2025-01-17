using AutoMapper;
using TodoAPI.Contracts;
using TodoAPI.Models;

// We typically do not want to map the id, CreatedAt, and UpdatedAt properties when creating or updating a Todo entity,
// as these properties are managed by the database. We can use the Ignore method to exclude these properties from the mapping configuration.

namespace TodoAPI.MappingProfiles
{
    public class AutoMapperProfile : Profile //Inheriting from Profile class, and can define mapping configurations in this class
    {
        public AutoMapperProfile()
        {
            CreateMap<CreateTodoRequest, Todo>() //map from createTodoRequest to Todo
                //ForMember method configures the mapping for a specific property
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignore the id property
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore()) // Ignore the CreatedAt property
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore()); // Ignore the UpdatedAt property

            CreateMap<UpdateTodoRequest, Todo>() //map from updateTodoRequest to Todo
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());
        }
    }
}
