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
                Suppliers = fornecedores,
                Categories = categorias,
                Products = produtos

            };
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult GetfilterOptions(string ? filterType)
        {
            if (filterType == "category")
            {
                var categoria = _context.Categories
                    .Select(c => new { id = c.CategoryId, nome = c.CategoryName })
                    .ToList();
                return Json(categoria);
            }
            if (filterType == "supplier")
            {
                var fornecedor = _context.Suppliers
                    .Select(s => new { id = s.SupplierId, nome = s.CompanyName })
                    .ToList();
                return Json(fornecedor);
                
            }

            return Json(new List<object>());
        }

        [HttpGet]
        public IActionResult GetFilteredProducts (string? filterType, int? filterId, int page = 1)
        {
            int pageSize = 5;

            var query = _context.Products.AsQueryable();

            if(!string.IsNullOrEmpty(filterType) && filterId.HasValue)
            {
                if(filterType == "category")
                {
                    query = query.Where(p => p.CategoryId == filterId.Value);
                }
                if(filterType == "supplier")
                {
                    query = query.Where(p => p.SupplierId == filterId.Value);
                }
            }

            var totalItens = query.Count();
            var produtos = query
                .OrderBy(p => p.ProductName)
                .Skip((page -1) * pageSize)
                .Take(pageSize)
                .Select(p => new
                {
                    productId = p.ProductId,
                    nome = p.ProductName,
                    preco = p.UnitPrice,
                    categoria = p.Category.CategoryName,
                    fornecedor = p.Supplier.CompanyName
                }).ToList();

            return Json(new
            {
                data = produtos,
                total = totalItens
            });
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
