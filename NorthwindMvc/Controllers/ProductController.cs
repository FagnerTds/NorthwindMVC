using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NorthwindMvc.Data;
using NorthwindMvc.Models.Entities;
using NorthwindMvc.Models.ViewModel;
using NorthwindMvc.Services;

namespace NorthwindMvc.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var viewModel = new CreateProductViewModel
        {
            Categories = await _service.GetCategoriesAssync(),
            Suppliers = await _service.GetSuppliersAssync()
        };
        return View(viewModel);
    } 

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Categories = await _service.GetCategoriesAssync();
            viewModel.Suppliers = await _service.GetSuppliersAssync();

            return View(viewModel); 
        }

        var product = new Product
        {
            ProductName = viewModel.ProductName,
            UnitPrice = viewModel.UnitPrice,
            CategoryId = viewModel.CategoryId,
            SupplierId = viewModel.SupplierId
        };
        await _service.CreateProductAsync(product);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Edit (int id)
    {
        var product = await _service.GetByIdAsync(id);

        if(product == null)
            return NotFound();

        var viewModel = new CreateProductViewModel
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            UnitPrice = product.UnitPrice,
            CategoryId = product.CategoryId,
            SupplierId = product.SupplierId,

            Categories = await _service.GetCategoriesAssync(),
            Suppliers = await _service.GetSuppliersAssync()
        };

        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CreateProductViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Categories = await _service.GetCategoriesAssync();
            viewModel.Suppliers = await _service.GetSuppliersAssync();

            return View(viewModel);
        }

        var product = await _service.GetByIdAsync(viewModel.ProductId.Value);

        if (product == null)
            return NotFound();

        product.ProductName = viewModel.ProductName;
        product.UnitPrice = viewModel.UnitPrice;
        product.CategoryId = viewModel.CategoryId;
        product.SupplierId = viewModel.SupplierId;

        await _service.UpdateAsync(product);

        return RedirectToAction("Index", "Home");
    }
}
