using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaydarShop.Server.Infrastructure.applicationsettings
{
    public class Main
    {
        public Main()
        {

        }
        public string SecretKey { get; set; }
        public int TokenExpireInMinutes { get; set; }
    }
}
