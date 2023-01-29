using PaydarShop.Server.Services;
using System.Threading.Tasks;
using ViewModels.Users;

namespace Dtx.Security.Server.Controllers
{
    public class UsersController : Infrastructure.BaseApiControllerWithDatabase
    {
        public UsersController(Data.IUnitOfWork unitOfWork, IUserService userService) : base(unitOfWork)
        {
            UserService = userService;
        }
        protected IUserService UserService { get; }

        [Microsoft.AspNetCore.Mvc.HttpPost("Login")]
        public
        async
         System.Threading.Tasks.Task
        <Microsoft.AspNetCore.Mvc.ActionResult<LoginResponseViewModel>>
        Login(LoginRequestViewModel viewModel)

        {
            LoginResponseViewModel response = null;
            await Task.Run(() =>
            { 
                response = UserService.Login(viewModel); 
            });

            if (response==null)
            {
                string errorMessage = "Username and/or password is not correct!";
            }

            return Ok(response);
        }

    }
}
