using System;

namespace Poker.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel(string userName, DateTime dateOfBirth)
        {
            UserName = userName;
            DateOfBirth = dateOfBirth;
        }

        public string UserName { get; }
        public DateTime DateOfBirth { get;}
    }
}
