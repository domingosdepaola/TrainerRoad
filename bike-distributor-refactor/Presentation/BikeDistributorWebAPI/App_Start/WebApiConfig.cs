using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace BikeDistributorWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.Routes.MapHttpRoute(
                 name: "ControllerOnly",
                 routeTemplate: "api/{controller}"
             );
            config.Routes.MapHttpRoute(
                           name: "ControllerAndId",
                           routeTemplate: "api/{controller}/{id}",
                           defaults: null,
                           constraints: new { id = @"^\d+$" } // Only integers 
                       );

            // Controllers with Actions
            // To handle routes like `/api/VTRouting/route`
            config.Routes.MapHttpRoute(
                name: "ControllerAndAction",
                routeTemplate: "api/{controller}/{action}"
            );
            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
        }
    }
}
