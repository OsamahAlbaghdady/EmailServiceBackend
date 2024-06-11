using GaragesStructure.DATA.DTOs.roles;

namespace GaragesStructure.DATA.DTOs.User
{
    public class UserDto : BaseDto<Guid>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        
        public string Email { get; set; }
        public RoleDto? Role { get; set; }
        public string Token { get; set; }

        public Guid? GarageId { get; set; }
        public string? GarageName { get; set; }
        
        public bool? IsActive { get; set; }
    }
}