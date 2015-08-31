using Application;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BikeDistributorWebAPI.Controllers
{
    public class BikeController : ApiController
    {
        public List<Bike> GetBikes() 
        {
            BikeApplication bikeApplication = new BikeApplication();
            List<Bike> lst = bikeApplication.bikeRepository.ListAll();
            return lst;
        }
	}
}