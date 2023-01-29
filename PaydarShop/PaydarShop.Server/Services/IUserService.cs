

using System;
using System.Collections.Generic;

namespace PaydarShop.Server.Services
{
    public interface IUserService
    {
        ViewModels.Users.LoginResponseViewModel Login(ViewModels.Users.LoginRequestViewModel viewModel);
        IEnumerable<Models.User> GetAll();
        Models.User GetById(Guid id);
    }

}
