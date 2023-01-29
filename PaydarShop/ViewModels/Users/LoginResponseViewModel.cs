using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Users
{
    public class LoginResponseViewModel
    {
        public LoginResponseViewModel(Models.User user, string token)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            Id = user.Id;
            FirstName = user.Username;
            LastName = user.FullName;
            Username = user.Username;
            Token = token;

        }
        public Guid Id { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }


    }
}
