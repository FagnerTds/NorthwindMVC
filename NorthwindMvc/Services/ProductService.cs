using NorthwindMvc.Models.Entities;
using NorthwindMvc.Repositories;

namespace NorthwindMvc.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _repositoryProduct;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IRepository<Supplier> _repositorySupplier;

        public ProductService(
            IRepository<Product> repositoryProduct, 
            IRepository<Category> repositoryCategory, 
            IRepository<Supplier> repositorySupplier)
        {
            _repositoryProduct = repositoryProduct;
            _repositoryCategory = repositoryCategory;
            _repositorySupplier = repositorySupplier;
        }

        public async Task CreateProductAsync(Product product)
        {
            
            if (string.IsNullOrEmpty(product.ProductName))
                throw new Exception("Nome é obrigatório");

            if (product.UnitPrice <= 0)
                throw new Exception("Preço deve ser maior que zero");

            await _repositoryProduct.AddAsync(product);
        }

        public async Task<List<Category>> GetCategoriesAssync()
        {
            return await _repositoryCategory.GetAllAssync();
        }

        public async Task<List<Supplier>> GetSuppliersAssync()
        {
            return await _repositorySupplier.GetAllAssync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _repositoryProduct.GetByIdAssync(id);
          
        }

        public async Task UpdateAsync(Product product)
        {
            await _repositoryProduct.UpdateAsync(product);
        }
    }
}
