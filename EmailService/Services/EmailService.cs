using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using GaragesStructure.DATA;
using GaragesStructure.DATA.DTOs.Email;
using GaragesStructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace GaragesStructure.Services;

public interface IEmailService
{
    Task<(EmailDto? email, string? error)> RegisterEmailAsync(EmailForm emailForm);
    
    Task<(EmailDto? email, string? error)> Send();
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

    public  async Task<(EmailDto? email, string? error)> Send()
    {
        
        
        string smtpServer = "smtp.gmail.com"; 
        int smtpPort = 587;
        string smtpUser = "technoabg@gmail.com"; 
        string smtpPass = "ocvs hwqr hums qjvr"; 

        // Email details
        string fromEmail = "technoabg@gmail.com";
        // string toEmail = "haidersaadon22@gmail.com"; 
        string subject = "تم اقتتاح ساحة بغداد للتبادل التجاري 🚀🎉";
        
        var body = "<p style='font-size: 16px;'>🎉 انضم إلينا في افتتاح مشروعنا الجديد! ستكون لحظة مميزة ومليئة بالإثارة. هيا بنا نحقق النجاح معًا! انضموا إلينا في حفل الافتتاح وكونوا جزءًا من هذه الرحلة الملهمة! 🚀 #افتتاح_المشروع</p>";
        

        
        // send email to all emails in db 
        
        var emails = await _context.Emails.ToListAsync();
        
        foreach (var email in emails)
        {
            string toEmail = email.EmailToSend;
            MailMessage mailMessage = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                IsBodyHtml = true, // Set to true to indicate HTML content
                Body = "<p dir='rtl' style='font-size: 16px;'>🎉 انضم إلينا في افتتاح مشروعنا الجديد! ستكون لحظة مميزة ومليئة بالإثارة. هيا بنا نحقق النجاح معًا! انضموا إلينا في حفل الافتتاح وكونوا جزءًا من هذه الرحلة الملهمة! 🚀 #افتتاح_المشروع</p>" +
                       "<img src='cid:image1' width='100%'/>"
            };
            

            var attachmentsDir = Path.Combine(Directory.GetCurrentDirectory(),
                "wwwroot", "Logo");
            
            string imagePath = Path.Combine(attachmentsDir, "bexy.png");

            LinkedResource linkedImage = new LinkedResource(imagePath);
            linkedImage.ContentId = "image1";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailMessage.Body, null, MediaTypeNames.Text.Html);
            htmlView.LinkedResources.Add(linkedImage);

            mailMessage.AlternateViews.Add(htmlView);

            SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            smtpClient.Send(mailMessage);
        }



        
        //
        // // ocvs hwqr hums qjvr
        // MailMessage mailMessage = new MailMessage(fromEmail, toEmail, subject, body);
        //
        //
        // Attachment attachment = new Attachment(attachmentPath);
        // mailMessage.Attachments.Add(attachment);
        //
        //
        // SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort)
        // {
        //     Credentials = new NetworkCredential(smtpUser, smtpPass),
        //     EnableSsl = true
        //     
        // };
        //

   
    
        

        // smtpClient.Send(mailMessage);
        Console.WriteLine("Email sent successfully.");
        
        return (null, null);
    }
}