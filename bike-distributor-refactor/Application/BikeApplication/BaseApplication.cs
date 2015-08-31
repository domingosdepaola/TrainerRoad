using Domain.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BaseApplication
    {
        public BaseApplication() 
        {
            Factory.Instance.InstallIoC();
        }
    }
}
