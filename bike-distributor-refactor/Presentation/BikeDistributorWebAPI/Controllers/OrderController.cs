using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Application;
using Domain.Entity;

namespace BikeDistributorWebAPI.Controllers
{
    public class OrderController : ApiController
    {
        NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        OrderApplication orderApplication = new OrderApplication();
        [HttpPut]
        public int CreateOrder([FromBody]string orderJson)
        {
            try
            {
                Order order = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(orderJson);
                return orderApplication.orderService.CreateOrder(order);
            }
            catch (Exception ex)
            {
                log.Error(ex, ex.Message);
                throw new Exception("ERROR : " + ex.Message + " see server log for details");
            }
        }
        [HttpPost]
        public string GenerateOrderWithRecept([FromBody]string orderJson, bool htmlRecept) 
        {
            try { 
            Order order = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(orderJson);
            return orderApplication.orderService.GenerateOrderWithRecept(order, htmlRecept);
            }
            catch (Exception ex) 
            {
                log.Error(ex.Message, ex);
                return "ERROR: " + ex.Message + " see log for details";
            }
        }
    }
}