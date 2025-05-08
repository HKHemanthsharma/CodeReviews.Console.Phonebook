using HkHemanthSharma.Phonebook.Controllers;

namespace HkHemanthSharma.Phonebook.Services
{
    public class EmailService
    {
        internal static void SendEmail()
        {
            Console.WriteLine("Select the Contact to send the email:");
            int selectedId = UserInputs.GetSingleContact();
            EmailController.SendEmail(selectedId);
        }
    }
}
