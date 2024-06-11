using System.Net;
using System.Net.Mail;
using GaragesStructure.DATA;
using GaragesStructure.DATA.DTOs.Email;
using GaragesStructure.Entities;

namespace GaragesStructure.Services;

public interface IEmailService
{
    Task<(EmailDto? email, string? error)> RegisterEmailAsync(EmailForm emailForm);
    
    Task<(EmailDto? email, string? error)> Send(int email);
}

public class EmailService : IEmailService
{
    private readonly DataContext _context;

    public EmailService(DataContext context)
    {
        _context = context;
    }

    public async Task<(EmailDto? email, string? error)> RegisterEmailAsync(EmailForm emailForm)
    {
        var email = _context.Emails.FirstOrDefault(x => x.EmailToSend == emailForm.Email);
        if (email != null) return (null, "البريد الاكتروني مسجل مسبقاً");

        var newEmail = new Email
        {
            EmailToSend = emailForm.Email
        };

        _context.Emails.Add(newEmail);

        await _context.SaveChangesAsync();
        
        

        return (new EmailDto
        {
            Id = newEmail.Id,
            Email = newEmail.EmailToSend,
            CreationDate = newEmail.CreationDate
        }, null);
    }

    public  async Task<(EmailDto? email, string? error)> Send(int email)
    {
        
        
        string smtpServer = "smtp.gmail.com"; 
        int smtpPort = 587;
        string smtpUser = "technoabg@gmail.com"; 
        string smtpPass = "ocvs hwqr hums qjvr"; 

        // Email details
        string fromEmail = "technoabg@gmail.com";
        string toEmail = "haidersaadon22@gmail.com"; 
        string subject = "Test Email with Attachment";
        string body = "ها كلب";
        string attachmentPath = "path/to/your/attachment.txt";

        // ocvs hwqr hums qjvr
        MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body);

        
        // Attachment attachment = new Attachment(attachmentPath);
        // mailMessage.Attachments.Add(attachment);

        
        SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
        {
            Credentials = new NetworkCredential(smtpUser, smtpPass),
            EnableSsl = true
            
        };

        smtpClient.Send(mailMessage);
        Console.WriteLine("Email sent successfully.");
        
        return (null, null);
    }
}