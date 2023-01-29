using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace PaydarShop.Server.Controllers
{
    public class SamplePaydarController : BaseApiControllerWithDatabase
    {
        //in yek test
        //in az github
        public SamplePaydarController(Data.IUnitOfWork unitOfWork ) : base(unitOfWork)
        {
            
        }
        //[Infrastructure.Attribute.Authorize(Role ="admin")]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public
         async
         System.Threading.Tasks.Task
         <Microsoft.AspNetCore.Mvc.ActionResult
             <System.Collections.Generic.IEnumerable<Models.SamplePaydar>>>
         GetAsync()

        {
            var result =
                await UnitOfWork.SamplePaydarRepository.GetAllAsync();

            var fluentResult = new FluentResults.Result<IList<Models.SamplePaydar>>();

            fluentResult.WithValue(result);
            fluentResult.WithSuccess("ok");

            return Ok(value: fluentResult);
        }
    }
}
