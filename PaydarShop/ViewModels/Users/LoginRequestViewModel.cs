using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels.Users
{
  public  class LoginRequestViewModel
    {
        public LoginRequestViewModel()
        {

        }
        [Required(AllowEmptyStrings =true)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Password { get; set; }
    }
}
