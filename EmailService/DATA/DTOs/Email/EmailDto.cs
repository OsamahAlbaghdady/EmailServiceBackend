namespace GaragesStructure.DATA.DTOs.Email;

public class EmailDto : BaseDto<Guid>
{
    public string? Email { get; set; }
}