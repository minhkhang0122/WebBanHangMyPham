using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEBANNUOCHOA.Models;
using WEBANNUOCHOA.Repositories;

namespace WEBANNUOCHOA.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class AdminHomeController : Controller
    {
        private readonly ILogger<AdminHomeController> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminHomeController(ILogger<AdminHomeController> logger, IOrderRepository orderRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allOrders = await _orderRepository.GetAllAsync();

            var queryableOrders = allOrders.AsQueryable();


            queryableOrders = queryableOrders.Include(order => order.OrderDetails.Select(od => od.Product))
                                     .Include(order => order.OrderDate);

            var productStatistics = allOrders.GroupBy(order => order.OrderDetails.Select(od => od.ProductId))
                                             .Select(group => new ProductStatisticViewModel
                                             {
                                                 ProductId = group.Key.FirstOrDefault(),
                                                 TotalQuantity = group.Sum(order => order.OrderDetails.Sum(od => od.Quantity)),
                                                 TotalPrice = group.Sum(order => order.OrderDetails.Sum(od => od.Price * od.Quantity)),
                                                 ProductName = group.FirstOrDefault()?.OrderDetails.FirstOrDefault()?.Product?.Name, // Access product name using eager loading
                                                 PurchaseDate = group.FirstOrDefault()?.OrderDate

                                             })
                                             .ToList();

            return View(productStatistics);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
