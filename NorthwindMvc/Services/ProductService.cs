using NorthwindMvc.Models.Entities;
using NorthwindMvc.Repositories;

namespace NorthwindMvc.Services
{
    public class ProductService : IProductService
    {
        private readonly Repository<Product> _repository;

        public ProductService(Repository<Product> repository)
        {
            _repository = repository;
        }

        public async Task CreateProductAsync(Product product)
        {
            
            if (string.IsNullOrEmpty(product.ProductName))
                throw new Exception("Nome é obrigatório");

            if (product.UnitPrice <= 0)
                throw new Exception("Preço deve ser maior que zero");

            await _repository.AddAsync(product);
        }
    }
}
