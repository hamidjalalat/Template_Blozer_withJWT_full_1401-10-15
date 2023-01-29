using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels.Users;

namespace PaydarShop.Server.Services
{
    public class UserService : IUserService
    {
        public UserService(Microsoft.Extensions.Options.IOptions<Infrastructure.applicationsettings.Main> options)
        {
            MainSettings = options.Value;
        }

        public Infrastructure.applicationsettings.Main MainSettings { get; set; }

        private List<User> _users;

        protected List<User> Users
        {
            get
            {
                if(_users==null)
                {
                    _users = new List<User>();
                    for (int i = 0; i < 10; i++)
                    {
                        User user = new User()
                        {
                            Id= new Guid("efd807eb-ffe2-4c15-84cd-9654a6cdc47"+i.ToString()),
                            Password="123",
                            Username=$"hamid"+i,
                            FullName=$"jalalalt"+i,
                            EmailAddress="jalalathamid@yahoo.com"
                        };
                        _users.Add(user);
                    }
                }
                return _users;
            }
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetById(Guid id)
        {
            User founduser = Users.Where(C => C.Id == id).FirstOrDefault();
            return founduser;
        }

        public LoginResponseViewModel Login(LoginRequestViewModel viewModel)
        {
            if (viewModel==null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(viewModel.UserName))
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(viewModel.Password))
            {
                return null;
            }

            Models.User foundeUser = Users.Where(C => C.Username.ToLower() == viewModel.UserName.ToLower())
                .FirstOrDefault();

            if (foundeUser==null)
            {
                return null;
            }

            if (string.Compare(foundeUser.Password,viewModel.Password,ignoreCase:false) !=0)
            {
                return null;
            }

            string token = Infrastructure.JwtUtility.GenerateJwtToken(user: foundeUser, mainSettings: MainSettings);

            LoginResponseViewModel response = new LoginResponseViewModel(foundeUser,token);

            return response;

        }
    }
}
