using Domain.Entity;
using Domain.Interfaces;
using Domain.Services.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class BikeApplication : BaseApplication
    {
        public IBikeRepository bikeRepository;
        public IRepository<Line, int> lineRepository;
        public IOrderService orderService;
        public BikeApplication() 
        {
            bikeRepository = Factory.Instance.Resolve<IBikeRepository>();
            lineRepository = Factory.Instance.Resolve <IRepository<Line, int>>();
            orderService = Factory.Instance.Resolve<IOrderService>();
        }
    }
}
