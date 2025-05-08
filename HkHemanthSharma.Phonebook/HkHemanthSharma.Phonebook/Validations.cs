using Spectre.Console;
using System.Net.Mail;

namespace HkHemanthSharma.Phonebook
{
    internal class Validations
    {
        internal static string ValidateEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("The email property cannot be empty!");
                }
                var validatedMail = new MailAddress(email);
                if (validatedMail.Address != email.Trim())
                {
                    throw new ArgumentException("The email is not in correct form!");
                }
                return email;
            }
            catch (Exception e)
            {
                throw new ArgumentException("The email is not in correct form!");
            }
        }

        internal static string ValidatePhoneNumber()
        {
            string PhoneNumber = Console.ReadLine();
            while (!long.TryParse(PhoneNumber, out long num) || PhoneNumber.Length != 10)
            {
                AnsiConsole.MarkupLine("Make sure you are entering a valid phoneNumber of 10 digits long without any non-digit charecters");
                PhoneNumber = Console.ReadLine();
            }
            return PhoneNumber;
        }
    }
}

