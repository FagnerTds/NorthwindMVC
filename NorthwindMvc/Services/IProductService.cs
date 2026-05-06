using NorthwindMvc.Models.Entities;

namespace NorthwindMvc.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(Product product);
    }
}
