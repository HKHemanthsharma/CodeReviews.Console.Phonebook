namespace HkHemanthSharma.Phonebook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (PhoneBookDbContext context = new())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            UserInterface.MainMenu();
        }
    }
}
