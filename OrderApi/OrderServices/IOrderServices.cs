using System.Text.Json;
using Confluent.Kafka;
using Shared;

namespace OrderApi.OrderServices
{
    public interface IOrderService
    {
        Task StartConsumingService();
        void AddOrder(Order order);
        List<Product> GetProducts();
        List<OrderSummary> GetOrdersSummary();
    }
}