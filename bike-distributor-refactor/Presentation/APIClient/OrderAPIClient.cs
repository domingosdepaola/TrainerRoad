using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;

namespace APIClient
{
    //Created to call the API in a separated module
    public class OrderAPIClient
    {
        static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private readonly static string WebAPIUrl = "http://localhost/BikeDistributorWebAPI/api/";
        private static string OrderControllerUrl = WebAPIUrl + "Order/";
        private static string GenerateOrderWithReceptUrl = OrderControllerUrl + "GenerateOrderWithRecept";
        private static string GetURlWithParameter(string url, string parameterName, string parameterValue)
        {
            return url + "?" + parameterName + "=" + parameterValue;
        }
        public static string CallWebAPIOrderWithReceipt(Order order, bool htmlReceipt)
        {
            CleanNavigationPropertys(order);
            string orderToSend = Newtonsoft.Json.JsonConvert.SerializeObject(order);
            string parameter = "htmlRecept";
            string value = htmlReceipt.ToString();
            string FullUrl = GetURlWithParameter(GenerateOrderWithReceptUrl, parameter, value);
            string result = "";
            string jsonResult = "";
            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("Content-Type", "application/json");
                    client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    jsonResult = client.UploadString(FullUrl, "=" + orderToSend);
                }
                if (jsonResult != null && jsonResult != "")
                {
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<String>(jsonResult);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex, ex.Message);
            }
            return result;

        }

        private static void CleanNavigationPropertys(Order order)
        {
            foreach (Line line in order.Line)
            {
                line.Order = null;
                if (line.Bike != null)
                {
                    line.Bike.Line = null;
                }
            }
        }                         
    }
}
