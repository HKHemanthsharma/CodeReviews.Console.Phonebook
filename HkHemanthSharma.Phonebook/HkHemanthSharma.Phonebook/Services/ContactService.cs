using HkHemanthSharma.Phonebook.Controllers;
using HkHemanthSharma.Phonebook.Models;
using Spectre.Console;

namespace HkHemanthSharma.Phonebook.Services
{
    internal class ContactService
    {
        internal static void AddContact()
        {
            Contact newContact = UserInputs.GetNewContact();
            int Id = UserInputs.GetSingleCategoryId();
            newContact.CategoryId = Id;
            ContactController.AddNewContact(newContact);

        }

        internal static void DeleteContact()
        {
            int selectedId = UserInputs.GetSingleContact();
            if (selectedId == -1)
            {
                AnsiConsole.MarkupLine("[aqua]No Contacts To Display At The Moment\n Press any Key to continue[/]");
                Console.ReadLine();
            }
            else
            {
                ContactController.DeleteContact(selectedId);
            }
        }

        internal static void UpdateContact()
        {
            int updatedId = UserInputs.GetSingleContact();
            if (updatedId == -1)
            {
                AnsiConsole.MarkupLine("[aqua]No Contacts To Display At The Moment\n Press any Key to continue[/]");
                Console.ReadLine();
            }
            else
            {
                Contact UpdateContact = ContactController.GetContactById(updatedId);
                UpdateContact = UserInputs.UpdateContactMenu(UpdateContact);
                ContactController.UpdateContact(UpdateContact);
            }
        }

        internal static void ViewAllContacts()
        {
            List<Contact> contacts = ContactController.GetAllContacts();
            if (contacts != null)
            {
                UserInterface.ShowAllContacts(contacts);
            }
            else
            {
                AnsiConsole.MarkupLine("[aqua]No Contacts To Display At The Moment\n Press any Key to continue[/]");
                Console.ReadLine();
            }

        }

        internal static void ViewSingleContact()
        {
            int selectedId = UserInputs.GetSingleContact();
            if (selectedId == -1)
            {
                AnsiConsole.MarkupLine("[aqua]No Contacts To Display At The Moment\n Press any Key to continue[/]");
                Console.ReadLine();
            }
            else
            {
                Contact selectedContact = ContactController.GetContactById(selectedId);
                UserInterface.ShowSingleContact(selectedContact);
            }
        }
    }
}

