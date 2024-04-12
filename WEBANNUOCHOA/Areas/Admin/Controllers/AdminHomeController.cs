using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

            var productStatistics = allOrders.GroupBy(order => order.OrderDetails.Select(od => od.ProductId))
                                             .Select(group => new ProductStatisticViewModel
                                             {
                                                 ProductId = group.Key.FirstOrDefault(), // Assuming ProductId is the first element in the key
                                                 TotalQuantity = group.Sum(order => order.OrderDetails.Sum(od => od.Quantity)),
                                                 TotalPrice = group.Sum(order => order.OrderDetails.Sum(od => od.Price * od.Quantity))
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
