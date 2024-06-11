using GaragesStructure.DATA.DTOs.Email;
using GaragesStructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace GaragesStructure.Controllers;

public class EmailController : BaseController
{
    private readonly IEmailService _emailService;
    
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost]
    public async Task<IActionResult> RegisterEmailAsync(EmailForm emailForm) => Ok(await _emailService.RegisterEmailAsync(emailForm));
    
    [HttpPost("send")]
    public async Task<IActionResult> Send() => Ok(await _emailService.Send());

}