using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Application;
using Domain.Entity;
using Infra.Common.Enum;
using System.Net;
using System;
using System.Text.RegularExpressions;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static ServiceReferenceOrder.Bike DefyWCF = new ServiceReferenceOrder.Bike();
        private readonly static ServiceReferenceOrder.Bike EliteWCF = new ServiceReferenceOrder.Bike();
        private readonly static ServiceReferenceOrder.Bike DuraAceWCF = new ServiceReferenceOrder.Bike();
        private readonly static Bike DefyLocal = new Bike("Giant", "Defy 1", (int)NumericValues.OneThousand);
        private readonly static Bike EliteLocal = new Bike("Specialized","Venge Elite",2400);
        private readonly static Bike DuraAceLocal = new Bike("Specialized", "S-Works Venge Dura-Ace",5200);
        private readonly static string WebAPIUrl = "http://localhost/BikeDistributorWebAPI/api/";
        private static string OrderControllerUrl = WebAPIUrl + "Order/";
        private static string GenerateOrderWithReceptUrl = OrderControllerUrl + "GenerateOrderWithRecept";
        private string GetURlWithParameter(string url, string parameterName, string parameterValue)
        {
            return url + "?" + parameterName + "=" + parameterValue;
        }
        public OrderTest() 
        {
            
        }
        private void InitiateWCFValues() 
        {
            DefyWCF.Brand = "Giant";
            DefyWCF.Model = "Defy 1";
            DefyWCF.Price = (int)NumericValues.OneThousand;
            EliteWCF.Brand = "Specialized";
            EliteWCF.Model = "Venge Elite";
            EliteWCF.Price = 2400;
            DuraAceWCF.Brand = "Specialized";
            DuraAceWCF.Model = "S-Works Venge Dura-Ace";
            DuraAceWCF.Price = 5003;
        }
        private string CallWebAPIOrderWithReceipt(Order order, bool htmlReceipt)
        {
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
                //log
            }
            return result;
            
        }                                     
        
        [TestMethod]
        public void ReceiptOneDefyWCFUsage()
        {
            InitiateWCFValues();
            List<ServiceReferenceOrder.Line> lstLines = new List<ServiceReferenceOrder.Line>();
            ServiceReferenceOrder.Line line = new ServiceReferenceOrder.Line();
            line.Bike = DefyWCF;
            line.Quantity = 1;
            lstLines.Add(line);
            ServiceReferenceOrder.Order order = new ServiceReferenceOrder.Order();
            order.Company = "Anywhere Bike Shop";
            order.Line = lstLines.ToArray();
            ServiceReferenceOrder.ServiceOrderWCFClient service = new ServiceReferenceOrder.ServiceOrderWCFClient();
            string receipt = service.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
        }
        [TestMethod]
        public void ReceiptOndeDefyWebAPIUsage() 
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(new Domain.Entity.Line(DefyLocal, 1));
            string receipt = CallWebAPIOrderWithReceipt(order,false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
        }
        [TestMethod]
        public void ReceiptOneDefyDLLUsage() 
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(new Domain.Entity.Line(DefyLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
        }
       

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop 
1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOndeEliteWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(new Domain.Entity.Line(EliteLocal, 1));
            string receipt = CallWebAPIOrderWithReceipt(order, false);
            Assert.AreEqual(ResultStatementOneElite, receipt.Replace("\n\r",""));
        }
        [TestMethod]
        public void ReceiptOneEliteDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(new Domain.Entity.Line(EliteLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneElite, receipt);
        }
        [TestMethod]
        public void ReceiptOneEliteWCFUsage() 
        {
            InitiateWCFValues();
            List<ServiceReferenceOrder.Line> lstLines = new List<ServiceReferenceOrder.Line>();
            ServiceReferenceOrder.Line line = new ServiceReferenceOrder.Line();
            line.Bike = EliteWCF;
            line.Quantity = 1;
            lstLines.Add(line);
            ServiceReferenceOrder.Order order = new ServiceReferenceOrder.Order();
            order.Company = "Anywhere Bike Shop";
            order.Line = lstLines.ToArray();
            ServiceReferenceOrder.ServiceOrderWCFClient service = new ServiceReferenceOrder.ServiceOrderWCFClient();
            string receipt = service.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
           // var order = new Order("Anywhere Bike Shop");
            //order.AddLine(new Line(DuraAce, 1));
           // Assert.AreEqual(ResultStatementOneDuraAce, order.Receipt());
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void HtmlReceiptOneDefyDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(new Domain.Entity.Line(DefyLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(HtmlResultStatementOneDefy, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneDefyWCFUsage()
        {
            //  var order = new Order("Anywhere Bike Shop");
            // order.AddLine(new Line(Defy, 1));
            // Assert.AreEqual(HtmlResultStatementOneDefy, order.HtmlReceipt());
        }
        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
           // var order = new Order("Anywhere Bike Shop");
           // order.AddLine(new Line(Elite, 1));
           // Assert.AreEqual(HtmlResultStatementOneElite, order.HtmlReceipt());
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
           // var order = new Order("Anywhere Bike Shop");
           // order.AddLine(new Line(DuraAce, 1));
            //Assert.AreEqual(HtmlResultStatementOneDuraAce, order.HtmlReceipt());
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";    
    }
}


