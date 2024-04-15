using Microsoft.AspNetCore.Mvc;
using WEBANNUOCHOA.Repositories;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc.Rendering;
using WEBANNUOCHOA.Models;

namespace WEBANNUOCHOA.Controllers
{

    public class FemaleProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public FemaleProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Index(ProductSearchModel searchModel) //Cho tìm kiếm Product
        {
            IEnumerable<Product> products;
            if (!string.IsNullOrEmpty(searchModel?.SearchTerm))
            {
                products = await _productRepository.SearchByNameAsync(searchModel.SearchTerm);
            }
            else
            {
                products = await _productRepository.GetAllAsync();
            }

            return View(products);
        }
    }
}
