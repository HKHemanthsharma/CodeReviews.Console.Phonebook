using HkHemanthSharma.Phonebook.Models;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace HkHemanthSharma.Phonebook.Controllers
{
    internal class ContactController
    {

        internal static void AddNewContact(Contact newContact)
        {
            try
            {
                using (PhoneBookDbContext context = new())
                {
                    context.Contacts.Add(newContact);
                    context.SaveChanges();
                    AnsiConsole.MarkupLine("[green]Contact Added Succesfully!![/]\n press any Key to go back to MainMenu");
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Erorr Occured while Adding the contact to the Database:" + e.Message);
                Console.ReadLine();
            }
        }

        internal static void DeleteContact(int selectedId)
        {
            using (PhoneBookDbContext context = new())
            {
                Contact DeletedContact = context.Contacts.SingleOrDefault(x => x.Id == selectedId);
                context.Contacts.Remove(DeletedContact);
                context.SaveChanges();
                AnsiConsole.MarkupLine("[red]Contact Deleted Succesfully!![/]\n[yellow] press any Key to go back to MainMenu[/]");
                Console.ReadLine();
            }
        }

        internal static List<Contact> GetAllContacts()
        {
            try
            {
                using (PhoneBookDbContext context = new())
                {
                    List<Contact> Contacts = context.Contacts.Include(x => x.Category).ToList();
                    return Contacts;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Erorr Occured while getting the contacts from the Database:" + e.Message);
            }
            return null;
        }

        internal static Contact GetContactById(int selectedId)
        {
            try
            {
                using (PhoneBookDbContext context = new())
                {
                    return context.Contacts.Include(x=>x.Category).FirstOrDefault(x => x.Id == selectedId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Erorr Occured while getting the contacts from the Database:" + e.Message);
            }
            return null;
        }

        internal static void UpdateContact(Contact updatedContact)
        {
            try
            {
                using (PhoneBookDbContext context = new())
                {
                    context.Contacts.Update(updatedContact);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An Erorr Occured while getting the contacts from the Database:" + e.Message);
            }
        }
    }
}

