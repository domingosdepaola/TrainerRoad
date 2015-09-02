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
        OrderApplication orderApplication = new OrderApplication();
        [HttpPut]
        public int CreateOrder([FromBody]string orderJson)
        {
            Order order = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(orderJson);
            return orderApplication.orderService.CreateOrder(order);
        }
        [HttpPost]
        public string GenerateOrderWithRecept([FromBody]string orderJson, bool htmlRecept) 
        {
            Order order = Newtonsoft.Json.JsonConvert.DeserializeObject<Order>(orderJson);
            return orderApplication.orderService.GenerateOrderWithRecept(order, htmlRecept);
        }
    }
}