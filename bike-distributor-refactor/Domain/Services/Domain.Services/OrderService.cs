using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity;
using Infra.Common.Enum;

namespace Domain.Services
{
    public class OrderService : IOrderService
    {
        IOrderRepository orderRepository;
        IRepository<Line, int> lineRepository;
        public OrderService(IOrderRepository orderRepository, IRepository<Line, int> lineRepository)
        {
            this.orderRepository = orderRepository;
            this.lineRepository = lineRepository;
        }
        private String DoReceipt(Order order)
        {
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", order.Company + " ", Environment.NewLine));
            if (order.Line != null && order.Line.Count > 0)
            {
                foreach (var line in order.Line)
                {
                    var thisAmount = 0d;
                    if (line.Bike != null && line.Bike.Price != null)
                    {
                        switch (line.Bike.PriceRange)
                        {
                            case NumericValues.OneThousand:
                                if (line.Quantity >= 20)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .9d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                            case NumericValues.TwoThousand:
                                if (line.Quantity >= 10)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .8d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                            case NumericValues.FiveThousand:
                                if (line.Quantity >= 5)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .8d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                        }
                    }
                    result.AppendLine(string.Format("{0} x {1} {2} = {3}", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * order.TaxRate.Value;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            return result.ToString();
        }
        public String GetHtmlReceipt(int idOrder)
        {
            Order order = orderRepository.Get(idOrder);
            if (order != null)
            {
                return DoHtmlReceipt(order);
            }
            else
            {
                return "ERROR: Can not find the order";
            }
        }
        public string GetReceipt(int idOrder)
        {
            Order order = orderRepository.Get(idOrder);
            if (order != null)
            {
                return DoReceipt(order);
            }
            else
            {
                return "ERROR: Can not find the order";
            }
        }

        public string DoHtmlReceipt(Order order)
        {
            var totalAmount = 0d;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", order.Company));
            if (order.Line != null && order.Line.Count > 0)
            {
                result.Append("<ul>");
                foreach (var line in order.Line)
                {
                    var thisAmount = 0d;
                    if (line.Bike != null && line.Bike.Price != null)
                    {
                        switch (line.Bike.PriceRange)
                        {
                            case NumericValues.OneThousand:
                                if (line.Quantity >= 20)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .9d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                            case NumericValues.TwoThousand:
                                if (line.Quantity >= 10)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .8d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                            case NumericValues.FiveThousand:
                                if (line.Quantity >= 5)
                                    thisAmount += line.Quantity * line.Bike.Price.Value * .8d;
                                else
                                    thisAmount += line.Quantity * line.Bike.Price.Value;
                                break;
                        }
                    }
                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Bike.Brand, line.Bike.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }
            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * order.TaxRate.Value;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            return result.ToString();
        }


        public int CreateOrder(Entity.Order order)
        {
            if (order.TaxRate == null)
            {
                order.TaxRate = order.DefaultTaxRate;
            }
            int idOrder = orderRepository.InsertWithIdReturn(order);
            return idOrder;
        }


        public string GenerateOrderWithRecept(Order order, bool htmlRecept)
        {
            CreateOrder(order);
            if (htmlRecept)
            {
                return DoHtmlReceipt(order);
            }
            else
            {
                return DoReceipt(order);
            }
        }
    }
}
