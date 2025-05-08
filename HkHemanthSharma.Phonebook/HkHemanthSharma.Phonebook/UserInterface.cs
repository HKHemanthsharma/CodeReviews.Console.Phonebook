using HkHemanthSharma.Phonebook.Models;
using HkHemanthSharma.Phonebook.Services;
using Spectre.Console;
using static HkHemanthSharma.Phonebook.Enum;
using Color = Spectre.Console.Color;


namespace HkHemanthSharma.Phonebook
{
    internal class UserInterface
    {
        internal static void MainMenu()
        {
            bool isAppRunning = true;
            while (isAppRunning)
            {
                Console.Clear();
                var userChoice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("[mediumpurple2_1] Please choose an option[/]")
                    .AddChoices("ManageContacts", "EmailContacts", "ManageCategories"));
                switch (userChoice)
                {
                    case "ManageCategories":
                        ManageCategories();
                        break;
                    case "ManageContacts":
                        ManageContacts();
                        break;
                    case "EmailContacts":
                        EmailContacts();
                        break;
                    default:
                        isAppRunning = false;
                        break;
                }
            }
        }

        private static void ManageCategories()
        {
            bool isAppRunning = true;
            while (isAppRunning)
            {
                Console.Clear();
                var userChoice = AnsiConsole.Prompt(new SelectionPrompt<CategoryMenuOptions>().Title("[mediumpurple2_1] Please choose an option[/]")
                    .AddChoices(CategoryMenuOptions.AddNewCategory,
                                 CategoryMenuOptions.DeleteCategory,
                                 CategoryMenuOptions.UpdateCategory,
                                 CategoryMenuOptions.ViewAllCategory,
                                 CategoryMenuOptions.ViewSingleCategory,
                                 CategoryMenuOptions.Quit));
                switch (userChoice)
                {
                    case CategoryMenuOptions.AddNewCategory:
                        CategoryService.AddCategory();
                        break;
                    case CategoryMenuOptions.DeleteCategory:
                        CategoryService.DeleteCategory();
                        break;
                    case CategoryMenuOptions.UpdateCategory:
                        CategoryService.UpdateCategory();
                        break;
                    case CategoryMenuOptions.ViewAllCategory:
                        CategoryService.ViewAllCategory();
                        break;
                    case CategoryMenuOptions.ViewSingleCategory:
                        CategoryService.ViewSingleCategory();
                        break;
                    default:
                        isAppRunning = false;
                        break;
                }
            }
        }

        internal static void ManageContacts()
        {
            bool isAppRunning = true;
            while (isAppRunning)
            {
                Console.Clear();
                var userChoice = AnsiConsole.Prompt(new SelectionPrompt<contactMenuOptions>().Title("[mediumpurple2_1] Please choose an option[/]")
                    .AddChoices(contactMenuOptions.AddNewContact,
                                 contactMenuOptions.DeleteContact,
                                 contactMenuOptions.UpdateContact,
                                 contactMenuOptions.ViewAllContacts,
                                 contactMenuOptions.ViewSingleContact,
                                 contactMenuOptions.Quit));
                switch (userChoice)
                {
                    case contactMenuOptions.AddNewContact:
                        ContactService.AddContact();
                        break;
                    case contactMenuOptions.ViewAllContacts:
                        ContactService.ViewAllContacts();
                        break;
                    case contactMenuOptions.ViewSingleContact:
                        ContactService.ViewSingleContact();
                        break;
                    case contactMenuOptions.DeleteContact:
                        ContactService.DeleteContact();
                        break;
                    case contactMenuOptions.UpdateContact:
                        ContactService.UpdateContact();
                        break;
                    default:
                        isAppRunning = false;
                        break;
                }
            }
        }
        internal static void EmailContacts()
        {
            EmailService.SendEmail();
        }

        internal static void ShowAllContacts(List<Contact> contacts)
        {
            Table ContactsTable = new();
            ContactsTable.AddColumn("ID");
            ContactsTable.AddColumn("Name");
            ContactsTable.AddColumn("Email");
            ContactsTable.AddColumn("PhoneNumber");
            ContactsTable.AddColumn("Address");
            ContactsTable.AddColumn("Category");
            contacts.ForEach(x => ContactsTable.AddRow(Markup.Escape(x.Id.ToString()), Markup.Escape(x.ContactName.ToString()), Markup.Escape(x.PhoneNumber.ToString()), Markup.Escape(x.Email.ToString()), Markup.Escape(x.Address.ToString()), Markup.Escape(x.Category.CategoryName.ToString())));
            AnsiConsole.Write(ContactsTable);
            AnsiConsole.MarkupLine("[aqua]Press any key to go to main Menu:[/]");
            Console.ReadLine();
        }

        internal static void ShowSingleContact(Contact selectedContact)
        {
            Panel ContactPanel = new Panel($"[Yellow]The Name of the Contact:[/][darkseagreen1]{selectedContact.ContactName}[/]" +
            $"\n[Yellow]The PhoneNumber of the Contact:[/][paleturquoise1]{selectedContact.PhoneNumber}[/]" +
            $"\n[Yellow]The Email of the Contact:[/][lightsteelblue1]{selectedContact.Email}[/]" +
            $"\n[Yellow]The Address of the Contact:[/][Aqua]{selectedContact.Address}[/]" +
            $"\n[Yellow]The Category Contact belongs to:[/][maroon]{selectedContact.Category.CategoryName}[/]")

            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Color.Orange3),
                Width = 60,
                Padding = new Padding(1, 1, 1, 1),
            };
            AnsiConsole.Write(ContactPanel);
            AnsiConsole.MarkupLine("[aqua]Press any key to go to main Menu:[/]");
            Console.ReadLine();
        }

        internal static void ShowAllCategories(List<Category> categories)
        {
            Table CategoryTable = new();
            CategoryTable.AddColumn("ID");
            CategoryTable.AddColumn("Name");
            categories.ForEach(x => CategoryTable.AddRow(Markup.Escape(x.Id.ToString()), Markup.Escape(x.CategoryName.ToString())));
            AnsiConsole.Write(CategoryTable);
            AnsiConsole.MarkupLine("[aqua]Press any key to go to main Menu:[/]");
            Console.ReadLine();
        }

        internal static void ShowSigleCategory(Category singleCategory)
        {
            Panel ContactPanel = new Panel($"[Yellow]The Name of the Category:[/][darkseagreen1]{singleCategory.CategoryName}[/]")
            {
                Border = BoxBorder.Rounded,
                BorderStyle = new Style(Spectre.Console.Color.Orange3),
                Width = 60,
                Padding = new Padding(1, 1, 1, 1),
            };
            AnsiConsole.Write(ContactPanel);
            AnsiConsole.MarkupLine("[aqua]Press any key to go to main Menu:[/]");
            Console.ReadLine();
        }
    }
}
