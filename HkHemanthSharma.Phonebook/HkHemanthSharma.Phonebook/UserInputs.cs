using HkHemanthSharma.Phonebook.Controllers;
using HkHemanthSharma.Phonebook.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HkHemanthSharma.Phonebook
{
    internal class UserInputs
    {
        internal static int GetSingleContact()
        {
            List<Contact> contacts = ContactController.GetAllContacts();
            if (contacts.Count == 0)
            {
                return -1;
            }
            List<string> ContactsNames = contacts.Select(x => $"Id:{x.Id},Name:{x.ContactName}").ToList();
            string SelectedContact = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("select a Contact")
                .AddChoices(ContactsNames));
            int SelectedId = int.Parse(SelectedContact.Split(",")[0].Split(":")[1].Trim());
            return SelectedId;
        }
        internal static Contact GetNewContact()
        {
            Contact newContact = new Contact();
            try
            {
                AnsiConsole.MarkupLine("[yellow] Please enter the Name of the Contact[/]");
                string name = Console.ReadLine();
                AnsiConsole.MarkupLine("[darkkhaki] Please enter the Email of the Contact[/]\n[lightskyblue1]Make Sure the Email is in a valid form[/]"
                    + "\n eg: [greenyellow]mailaddress@host.name[/]");
                string email = Console.ReadLine();
                email = Validations.ValidateEmail(email);
                AnsiConsole.MarkupLine("[yellow] Please enter the PhoneNumber of the Contact[/]\n [greenyellow]Maku sure the number consists of 10 digits[/]");
                string phoneNumber = Validations.ValidatePhoneNumber();
                AnsiConsole.MarkupLine("[yellow] Please enter the Address of the Contact[/]");
                string Address = Console.ReadLine();
                if (string.IsNullOrEmpty(Address.Trim()))
                {
                    Address = "NA";
                }
                newContact.ContactName = name;
                newContact.Email = email;
                newContact.PhoneNumber = phoneNumber;
                newContact.Address = Address;
                return newContact;
            }
            catch (ArgumentException e)
            {
                if (e.Message.Contains("email"))
                {
                    AnsiConsole.MarkupLine("[orange3]The Entered email is not of correct form![/]\n here are the additional details:");
                    AnsiConsole.MarkupLine("[aqua]Press any key to go to main menu and try again to enter the valid contact again![/]");
                    Console.ReadLine();
                }
            }
            return newContact;
        }

        internal static Contact UpdateContactMenu(Contact updateContact)
        {
            UserInterface.ShowSingleContact(updateContact);
            bool confirm = AnsiConsole.Confirm("Do you want to Update the Name?");
            updateContact.ContactName = confirm ? Console.ReadLine() : updateContact.ContactName;
            confirm = AnsiConsole.Confirm("Do you want to Update the PhoneNumber?");
            if (confirm)
            {
                string PhoneNumber = Validations.ValidatePhoneNumber();
                updateContact.PhoneNumber = PhoneNumber;
            }
            confirm = AnsiConsole.Confirm("Do you want to Update the Email?");
            updateContact.Email = confirm ? Validations.ValidateEmail(Console.ReadLine()) : updateContact.Email;
            confirm = AnsiConsole.Confirm("Do you want to Update the Address?");
            updateContact.Address = confirm ? Console.ReadLine() : updateContact.Address;
            confirm = AnsiConsole.Confirm("Do you want to Update the Category?");
            int CategoryId = UserInputs.GetSingleCategoryId();
            updateContact.CategoryId = confirm ? CategoryId : updateContact.CategoryId;
            return updateContact;
        }

        internal static SendEmailDetail EmailMenu()
        {
            try
            {

                AnsiConsole.MarkupLine("[yellow]Enter the Mail Subject:[/]");
                string subject = Console.ReadLine();

                AnsiConsole.MarkupLine("[yellow]Enter the Mail Message body:[/]");
                string message = Console.ReadLine();
                SendEmailDetail FromEmail = new();
                FromEmail.MessageBody = message;
                FromEmail.Subject = subject;
                return FromEmail;
            }
            catch (Exception e)
            {
                if (e.Message.Contains("email"))
                {
                    AnsiConsole.MarkupLine("[orange3]The Entered email is not of correct form![/]\n here are the additional details:");
                    AnsiConsole.MarkupLine("[aqua]Press any key to go to main menu and try again to enter the valid contact again![/]");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine(e.Message);
                }
            }
            return null;
        }

        internal static Category GetNewCategory()
        {
            AnsiConsole.MarkupLine("[yellow] Please enter the Name of the Category[/]");
            string CategoryName = Console.ReadLine();
            return new Category { CategoryName = CategoryName };
        }

        internal static int GetSingleCategoryId()
        {
            List<Category> Categories = CategoryController.GetAllCategory();
            if (Categories.Count == 0)
            {
                return -1;
            }
            List<string> CategoryNames = Categories.Select(x => $"Id:{x.Id},Name:{x.CategoryName}").ToList();
            string SelectedContact = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .Title("select a Contact")
                .AddChoices(CategoryNames));
            int SelectedId = int.Parse(SelectedContact.Split(",")[0].Split(":")[1].Trim());
            return SelectedId;
        }
    }
}

