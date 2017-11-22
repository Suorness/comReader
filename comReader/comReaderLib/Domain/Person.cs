using System;

namespace comReaderLib.Domain
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set;}
        public string Address { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }
        public string PathToImage { get; set; }
        public string CardNumber { get; set; }
    }
}
