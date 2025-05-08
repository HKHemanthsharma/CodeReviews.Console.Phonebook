using HkHemanthSharma.Phonebook.Models;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Spectre.Console;
using System.Configuration;

namespace HkHemanthSharma.Phonebook.Controllers
{
    public class EmailController
    {

        public static void SendEmail(int selectedId)
        {
            try
            {
                using (PhoneBookDbContext context = new())
                {
                    Contact contact = context.Contacts.SingleOrDefault(x => x.Id == selectedId);
                    AnsiConsole.MarkupLine($"The email of the contact is: [magenta1] {contact.Email}[/]");
                    bool confirm = AnsiConsole.Confirm("Do you want to edit the Email Address before sending mail?");
                    if (confirm)
                    {
                        string email = Validations.ValidateEmail(Console.ReadLine());
                        contact.Email = email;
                        ContactController.UpdateContact(contact);
                    }
                    SendMailHelper(contact);
                }
            }
            catch (Exception e)
            {
                if (e.Message.Contains("email"))
                {
                    AnsiConsole.MarkupLine("[red] The Email is invalid one[/] [aqua]you'll be returned to main Menu upon pressing any Key and try again![/]");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
        }
        private static void SendMailHelper(Contact contact)
        {
            try
            {
                var fromAddress = ConfigurationManager.AppSettings["FromAddress"];
                var fromName = ConfigurationManager.AppSettings["FromName"];
                var appPassword = ConfigurationManager.AppSettings["AppPassword"];
                var smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                var smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                SendEmailDetail SendEmail = UserInputs.EmailMenu();               // Get email details from user              
                                                                                  // Get sender's Gmail credentials
                var senderEmail = fromAddress;
                var senderPassword = appPassword;
                bool isHtml = false;

                // Create the email message
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("You", senderEmail));
                email.To.Add(new MailboxAddress(contact.ContactName, contact.Email));
                email.Subject = SendEmail.Subject;

                email.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
                {
                    Text = EmailConstructor(SendEmail.MessageBody, contact.ContactName, fromName)
                };

                // Configure and send the email
                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                // Gmail SMTP settings
                smtp.Connect(smtpServer, smtpPort, SecureSocketOptions.StartTls);

                // Note: If you have 2FA enabled, you need to use an App Password
                smtp.Authenticate(senderEmail, senderPassword);

                smtp.Send(email);
                smtp.Disconnect(true);

                AnsiConsole.MarkupLine("[green]Email sent successfully![/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error sending email: {ex.Message}[/]");
                if (ex.InnerException != null)
                {
                    AnsiConsole.MarkupLine($"[red]Details: {ex.InnerException.Message}[/]");
                }
            }
            finally
            {
                AnsiConsole.MarkupLine("[aqua]Press any key to continue...[/]");
                Console.ReadKey();
            }
        }

        private static string EmailConstructor(string messageBody, string toName, string fromName)
        {
            string Body = @$"Hi Dear {toName},
This is {fromName} from the PhoneBook App!
{messageBody}
regards,
{fromName}
";
            return Body;
        }
    }
}

