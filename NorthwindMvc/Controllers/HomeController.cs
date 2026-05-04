using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Data;
using NorthwindMvc.Models;
using NorthwindMvc.Models.ViewModel;

namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext _context;

        public HomeController(ILogger<HomeController> logger, NorthwindContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(string? filterType, int? filterId)
        {
            var categorias = _context.Categories.ToList();
            var fornecedores = _context.Suppliers.ToList();

            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(filterType) && filterId.HasValue)
            {
                if(filterType == "category")
                {
                    query = query.Where(p => p.CategoryId == filterId.Value);
                }
                else if (filterType == "supplier")
                {
                    query = query.Where(p => p.SupplierId == filterId.Value);
                }
            }

            var produtos = query.Select(p => new ProductListItemViewModel
            {
                Nome = p.ProductName,
                Preco = p.UnitPrice,
                Categoria = p.Category.CategoryName,
                Fornecedor = p.Supplier.CompanyName
            })
            .ToList();

            var viewModel = new ProductFilterViewModel
            {
                FilterType = filterType,
                FilterId = filterId,
                Supliers = fornecedores,
                Categories = categorias,
                Products = produtos

            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
