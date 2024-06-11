using FluentValidation;

namespace GaragesStructure.DATA.DTOs.Email;

public class EmailForm
{
    public string? Email { get; set; }
}



public  class EmailFormValidator : AbstractValidator<EmailForm>
{
    public EmailFormValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}