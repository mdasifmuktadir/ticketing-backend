using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Eapproval.Models;
using Eapproval.Helpers.IHelpers;




namespace Eapproval.Helpers;

public enum EventType
{
    SeekSupervisorApproval,
    Rejected,
    SeekTicketingHeadApproval,
    SeekHigherAuthorityApproval,
    CloseRequest,
    CloseRequestAccept,
    CloseRequestReject,
    Ask,
    Give,
    Assign,
    Reassign,
    AssignSelf,
    SupervisorApproved,
    HigherAuthorityApproved
}

public class TicketMailer:ITicketMailer
{
   
            private IConfiguration _configuration;
            private string senderEmail;
            private string senderPassword;


            public TicketMailer(IConfiguration configuration)
            {
                _configuration = configuration;
                senderEmail = _configuration["Email:Email"];
                senderPassword = _configuration["Email:Password"];
            }
    

    public async Task SendMail(User from, User to, string department, string _event, string id, User raiser)
    {
         string body = string.Empty;
         string subject = string.Empty;
         string html = string.Empty;

        Console.WriteLine("Sending Email");

        switch (_event)
        {
          

            case "rejected":
                subject = "Your Request Has been Rejected";
                html = $@"
            <p>{from.EmpName} has rejected the ticket that you raised for the {department} department</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html;
                break;

            case "seeking approval":
                subject = "A new ticket needs your approval";
                html = $@"
            <p>A new ticket raised by {from.EmpName} for the {department} department requires your approval</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html;
                break;

         

            case "closed":
                subject = "One of your ticket has been closed";
                html = $@"
            <p>{from.EmpName} has closed the ticket that you raised for the {department} department</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html; 
                break;

            case "new ticket":
                subject = "A new ticket has been raised for your department";
                html = $@"
            <p>{from.EmpName} has raised a new ticket for your department</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html;
                break;

            case "seeking information":
                subject = "More information is required for your ticket";
                html = $@"
            <p>{from.EmpName} is requesting more information regarding your ticket.</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html; 
                break;

            case "given information":
                subject = "You have received more information regarding a ticket";
                html = $@"
            <p>{from.EmpName} has given you more information regarding their ticket for the {department} department.</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";

                body = html; 
                break;

            case "assigned":
                subject = "You have been assigned a new ticket";
                html = $@"
            <p>{from.EmpName} has assigned you a new ticket from {raiser.EmpName}</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";
                body = html;
                break;

            case "approved":
                subject = "You have received approval regarding a ticket";
                html = $@"
            <p>{from.EmpName} has approved a ticket</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";

                body = html; 
                break;


                 case "unassigned":
                subject = "A ticket has been unassigned from you";
                html = $@"
            <p>{from.EmpName} has unassigned a ticket from you.</p>
            <a href=""http://192.168.1.10:5173/ticketing/ticketDetails/{id}"" style='text-decoration: underline; color:dodgerblue'>Check</a>";

                body = html; 
                break;

            default:
                break;
        }

        string? recipientEmail = to.MailAddress;
        

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("", senderEmail));
        message.To.Add(new MailboxAddress("", recipientEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder();
        bodyBuilder.HtmlBody = body;
        message.Body = bodyBuilder.ToMessageBody();

        Console.WriteLine("This is the address from which the email is sent");
        Console.WriteLine(senderEmail);

        using (var client = new SmtpClient())
        {
            Console.WriteLine("Just ending email");
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }



  public async Task  SendPdfToUsers(string pdfFilePath, List<User> users)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Your Name", "your-email@example.com")); // Replace with your name and email
            message.Subject = "Subject of the Email";

            // Create a multipart message
            var multipart = new Multipart("mixed");

            // Add the PDF as an attachment
            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(File.OpenRead(pdfFilePath), ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = Path.GetFileName(pdfFilePath)
            };
            multipart.Add(attachment);

            // Create a text message
            var text = new TextPart("plain")
            {
                Text = "Body of the Email"
            };
            multipart.Add(text);

            // Set the multipart as the message body
            message.Body = multipart;

           

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls); // Replace with your SMTP server details
                await client.AuthenticateAsync(senderEmail, senderPassword); // Replace with your email and password
              foreach (User user in users)  
                {
                    message.To.Clear();
                    message.To.Add(new MailboxAddress("", user.MailAddress));

                    await client.SendAsync(message);
                }

                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions (e.g., logging, notifying admin, etc.)
            Console.WriteLine("Error sending email: " + ex.Message);
        }
    }



    
}
