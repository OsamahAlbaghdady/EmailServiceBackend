using System.Text.Json.Nodes;
using AutoMapper;
using GaragesStructure.DATA.DTOs.roles;
using GaragesStructure.DATA.DTOs.User;
using GaragesStructure.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OneSignalApi.Model;

// here to implement


namespace GaragesStructure.Helpers
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {

            CreateMap<AppUser, UserDto>();
            CreateMap<RegisterForm, App>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<UpdateUserForm, AppUser>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Role, RoleDto>();
            CreateMap<AppUser, AppUser>();

            CreateMap<Permission, PermissionDto>();



            // here to add

        }
    }
}