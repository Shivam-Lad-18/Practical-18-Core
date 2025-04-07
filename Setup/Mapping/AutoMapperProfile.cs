using AutoMapper;
using Setup.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Setup.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, StudentViewModel>().ReverseMap();
            CreateMap<StudentViewModel, Student>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
        }
    }
}
