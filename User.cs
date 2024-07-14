using System.Collections.Generic;

namespace Azenride
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public List<string> FavoriteLocations { get; set; }
        public List<string> Notifications { get; set; }

        public User(string username, string password, string contactNumber, string address)
        {
            Username = username;
            Password = password;
            ContactNumber = contactNumber;
            Address = address;
            FavoriteLocations = new List<string>();
            Notifications = new List<string>();
        }
    }
}