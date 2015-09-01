using BikeDistributor.Test.ServiceReferenceOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BikeDistributor.Test
{
    [TestClass]
    public class OrderTest
    {
        private readonly static Bike Defy = new Bike();
        private readonly static Bike Elite = new Bike();
        private readonly static Bike DuraAce = new Bike();
        public OrderTest() 
        {
            Defy.Brand = "Giant";
            Defy.Model = "Defy 1";
            Defy.Price = (int)Infra.Common.Enum.NumericValues.OneThousand;
            Elite.Brand = "Specialized";
            Elite.Model ="Venge Elite";
            Elite.Price = 2400;
            DuraAce.Brand = "Specialized";
            DuraAce.Model = "S-Works Venge Dura-Ace";
            DuraAce.Price = 5003;
        }
        [TestMethod]
        public void ReceiptOneDefy()
        {
            //var order = new Order("Anywhere Bike Shop");
            //order.AddLine(new Line(Defy, 1));
            //Assert.AreEqual(ResultStatementOneDefy, order.Receipt());
            List<ServiceReferenceOrder.Line> lstLines = new List<ServiceReferenceOrder.Line>();
            Line line = new Line();
            line.Bike = Defy;
            line.Quantity = 1;
            lstLines.Add(line);
            Order order = new Order();
            order.Line = lstLines.ToArray();
            ServiceOrderWCFClient service = new ServiceOrderWCFClient();
            string receipt = service.GenerateOrderWithRecept(order, false);
        }

        private const string ResultStatementOneDefy = @"Order Receipt for Anywhere Bike Shop
	1 x Giant Defy 1 = $1,000.00
Sub-Total: $1,000.00
Tax: $72.50
Total: $1,072.50";

        [TestMethod]
        public void ReceiptOneElite()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1));
            Assert.AreEqual(ResultStatementOneElite, order.Receipt());
        }

        private const string ResultStatementOneElite = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized Venge Elite = $2,000.00
Sub-Total: $2,000.00
Tax: $145.00
Total: $2,145.00";

        [TestMethod]
        public void ReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));
            Assert.AreEqual(ResultStatementOneDuraAce, order.Receipt());
        }

        private const string ResultStatementOneDuraAce = @"Order Receipt for Anywhere Bike Shop
	1 x Specialized S-Works Venge Dura-Ace = $5,000.00
Sub-Total: $5,000.00
Tax: $362.50
Total: $5,362.50";

        [TestMethod]
        public void HtmlReceiptOneDefy()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Defy, 1));
            Assert.AreEqual(HtmlResultStatementOneDefy, order.HtmlReceipt());
        }

        private const string HtmlResultStatementOneDefy = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Giant Defy 1 = $1,000.00</li></ul><h3>Sub-Total: $1,000.00</h3><h3>Tax: $72.50</h3><h2>Total: $1,072.50</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneElite()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(Elite, 1));
            Assert.AreEqual(HtmlResultStatementOneElite, order.HtmlReceipt());
        }

        private const string HtmlResultStatementOneElite = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized Venge Elite = $2,000.00</li></ul><h3>Sub-Total: $2,000.00</h3><h3>Tax: $145.00</h3><h2>Total: $2,145.00</h2></body></html>";

        [TestMethod]
        public void HtmlReceiptOneDuraAce()
        {
            var order = new Order("Anywhere Bike Shop");
            order.AddLine(new Line(DuraAce, 1));
            Assert.AreEqual(HtmlResultStatementOneDuraAce, order.HtmlReceipt());
        }

        private const string HtmlResultStatementOneDuraAce = @"<html><body><h1>Order Receipt for Anywhere Bike Shop</h1><ul><li>1 x Specialized S-Works Venge Dura-Ace = $5,000.00</li></ul><h3>Sub-Total: $5,000.00</h3><h3>Tax: $362.50</h3><h2>Total: $5,362.50</h2></body></html>";    
    }
}


