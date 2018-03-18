using System;
using Microsoft.AspNet.Identity;

namespace Poker.Models.User
{
    public class User : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsAdmin { get; set; }
    }
}