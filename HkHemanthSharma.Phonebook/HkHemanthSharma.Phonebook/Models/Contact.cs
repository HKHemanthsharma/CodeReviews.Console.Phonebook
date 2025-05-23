﻿namespace HkHemanthSharma.Phonebook.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Address { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
