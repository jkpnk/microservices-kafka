using System.Text.Json;
using Confluent.Kafka;
using Shared;

namespace OrderApi.OrderServices
{
    public class OrderService(IConsumer<Null, string> consumer) : IOrderService
    {
        private const string AddProductTopic = "add-product-topic";
        private const string DeleteProductTopic = "delete-product-topic";

        public List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }

        public async Task StartConsumingService()
        {
            await Task.Delay(10);
            consumer.Subscribe([AddProductTopic, DeleteProductTopic]);
            while (true)
            {
                var response = consumer.Consume();
                if (!string.IsNullOrEmpty(response.Message.Value))
                {
                    // check if topic == add product
                    if (response.Topic == AddProductTopic)
                    {
                        var product = JsonSerializer.Deserialize<Product>(response.Message.Value);
                        Products.Add(product!);
                    }
                    else
                    {
                        Products.Remove(Products.FirstOrDefault(p => p.Id == int.Parse(response.Message.Value))!);

                    }
                }
            }
        }

        private void ConsoleProduct()
        {
            Console.Clear();
            foreach (var item in Products)
                Console.WriteLine($"Id: {item.Id}, Name: {item.Name}, Quantity: {item.Price}");
        }


        public void AddOrder(Order order) => Orders.Add(order);

        public List<OrderSummary> GetOrdersSummary()
        {
            var orderSummary = new List<OrderSummary>();
            foreach (var order in Orders)
            {
                orderSummary.Add(new OrderSummary()
                {
                    OrderId = order.Id,
                    ProductId = order.ProductId,
                    ProductName = Products.FirstOrDefault(p => p.Id == order.ProductId)!.Name,
                    ProductPrice = Products.FirstOrDefault(p => p.Id == order.ProductId)!.Price,
                    OrderedQuantity = order.Quantity
                });
            }
            return orderSummary;
        }

        public List<Product> GetProducts() => Products;

        
    }
}