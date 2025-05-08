namespace HkHemanthSharma.Phonebook.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Contact>? Contacts { get; set; }

    }
}
