using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Application;
using Domain.Entity;
using Infra.Common.Enum;
using System.Net;
using System;
using System.Text.RegularExpressions;
using APIClient;
using System.Threading;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static ServiceReferenceOrder.Bike DefyWCF = new ServiceReferenceOrder.Bike();
        private readonly static ServiceReferenceOrder.Bike EliteWCF = new ServiceReferenceOrder.Bike();
        private readonly static ServiceReferenceOrder.Bike DuraAceWCF = new ServiceReferenceOrder.Bike();
        private readonly static Bike DefyLocal = new Bike("Giant", "Defy 1", (int)NumericValues.OneThousand);
        private readonly static Bike EliteLocal = new Bike("Specialized", "Venge Elite", (int)NumericValues.TwoThousand);
        private readonly static Bike DuraAceLocal = new Bike("Specialized", "S-Works Venge Dura-Ace", (int)NumericValues.FiveThousand);
       private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";
        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";
        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";
        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";
        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";
        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        public OrderTest()
        {
            // For DLL usage, my current culture was pt-BR. In other usages the culture is in the web.config
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
        }
        private void InitiateWCFValues()
        {
            DefyWCF.Brand = "Giant";
            DefyWCF.Model = "Defy 1";
            DefyWCF.Price = (int)NumericValues.OneThousand;
            EliteWCF.Brand = "Specialized";
            EliteWCF.Model = "Venge Elite";
            EliteWCF.Price = (int)NumericValues.TwoThousand;
            DuraAceWCF.Brand = "Specialized";
            DuraAceWCF.Model = "S-Works Venge Dura-Ace";
            DuraAceWCF.Price = (int)NumericValues.FiveThousand;
        }
        #region DefaultReceipts
        #region Defy
        [TestMethod]
        public void ReceiptOndeDefyWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DefyLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
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
        public void ReceiptOneDefyDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DefyLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDefy, receipt);
        }
        #endregion
        #region Elite
        [TestMethod]
        public void ReceiptOneEliteWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(EliteLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, false);
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
            Assert.AreEqual(ResultStatementOneElite, receipt);
        }
        [TestMethod]
        public void ReceiptOneEliteDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(EliteLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneElite, receipt);
        }
        #endregion
        #region DuraAce
        [TestMethod]
        public void ReceiptOneDuraAceWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DuraAceLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, false);
            Assert.AreEqual(ResultStatementOneDuraAce, receipt);
        }
        [TestMethod]
        public void ReceiptOneDuraAceWCFUsage()
        {
            InitiateWCFValues();
            List<ServiceReferenceOrder.Line> lstLines = new List<ServiceReferenceOrder.Line>();
            ServiceReferenceOrder.Line line = new ServiceReferenceOrder.Line();
            line.Bike = DuraAceWCF;
            line.Quantity = 1;
            lstLines.Add(line);
            ServiceReferenceOrder.Order order = new ServiceReferenceOrder.Order();
            order.Company = "Anywhere Bike Shop";
            order.Line = lstLines.ToArray();
            ServiceReferenceOrder.ServiceOrderWCFClient service = new ServiceReferenceOrder.ServiceOrderWCFClient();
            string receipt = service.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDuraAce, receipt);
        
        }
        [TestMethod]
        public void ReceiptOneDuraAceDLLUsage() 
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DuraAceLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, false);
            Assert.AreEqual(ResultStatementOneDuraAce, receipt);
        }
        #endregion
        #endregion
        #region HTMLReceipts
        #region Defy
        [TestMethod]
        public void HtmlReceiptOneDefyWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DefyLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, true);
            Assert.AreEqual(HtmlResultStatementOneDefy, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneDefyWCFUsage()
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
            string receipt = service.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneDefy, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneDefyDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DefyLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneDefy, receipt);
        }
        #endregion
        #region Elite
        [TestMethod]
        public void HtmlReceiptOneEliteWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(EliteLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, true);
            Assert.AreEqual(HtmlResultStatementOneElite, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneEliteWCFUsage()
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
            string receipt = service.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneElite, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneEliteDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(EliteLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneElite, receipt);
        }
        #endregion
        #region DuraAce
        [TestMethod]
        public void HtmlReceiptOneDuraAceWebAPIUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DuraAceLocal, 1));
            string receipt = OrderAPIClient.CallWebAPIOrderWithReceipt(order, true);
            Assert.AreEqual(HtmlResultStatementOneDuraAce, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneDuraAceWCFUsage()
        {
            InitiateWCFValues();
            List<ServiceReferenceOrder.Line> lstLines = new List<ServiceReferenceOrder.Line>();
            ServiceReferenceOrder.Line line = new ServiceReferenceOrder.Line();
            line.Bike = DuraAceWCF;
            line.Quantity = 1;
            lstLines.Add(line);
            ServiceReferenceOrder.Order order = new ServiceReferenceOrder.Order();
            order.Company = "Anywhere Bike Shop";
            order.Line = lstLines.ToArray();
            ServiceReferenceOrder.ServiceOrderWCFClient service = new ServiceReferenceOrder.ServiceOrderWCFClient();
            string receipt = service.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneDuraAce, receipt);
        }
        [TestMethod]
        public void HtmlReceiptOneDuraAceDLLUsage()
        {
            var order = new Domain.Entity.Order("Anywhere Bike Shop");
            order.AddLine(LineInstance.NewLine(DuraAceLocal, 1));
            OrderApplication orderApplication = new OrderApplication();
            string receipt = orderApplication.orderService.GenerateOrderWithRecept(order, true);
            Assert.AreEqual(HtmlResultStatementOneDuraAce, receipt);
        }
        #endregion
        #endregion
    }

}


