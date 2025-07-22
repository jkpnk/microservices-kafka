using System.Text.Json;
using Confluent.Kafka;
using Shared;

namespace ProductApi.ProductServices
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(int id);
    }
}