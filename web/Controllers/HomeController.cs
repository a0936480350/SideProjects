using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Diagnostics;
using web.Data;
using web.Models;

namespace web.Controllers
{
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public static List<Customer> _customers = new List<Customer>
        {
            new Customer { Id = 1, City = "台北", Name = "Kevin", Address = "信義區" },
            new Customer { Id = 2, City = "桃園", Name = "Mike", Address = "中壢區" },
            new Customer { Id = 3, City = "台中", Name = "Stella", Address = "西屯區" }
        };

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Customer> customer = new List<Customer>
        {
            new Customer { Id = 1, City = "台北", Name = "Kevin", Address = "信義區" },
            new Customer { Id = 2, City = "桃園", Name = "Mike", Address = "中壢區" },
            new Customer { Id = 3, City = "台中", Name = "Stella", Address = "西屯區" }
        };

            ViewBag.Customers2 = customer;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Hello()
        {
            return View();
        }

        public IActionResult Customer()
        {
            return View(_customers);
        }

        public IActionResult Detail(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return Content("找不到資料");
            }

            return View(customer);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customer = _context.Customers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Customer");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);


            if (customer == null)
            {
                return Content("找不到資料");
            }
           

            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var target = _context.Customers.FirstOrDefault(x => x.Id == customer.Id);

            if (target != null)
            {
                target.City = customer.City;
                target.Name = customer.Name;
                target.Address = customer.Address;
            }
            _context.SaveChanges();

            return RedirectToAction("Customer");
        }

        public IActionResult Delete(int id)
        {
            var customer = _context.Customers.FirstOrDefault(x => x.Id == id);

            if (customer != null)
            {
                _context.Remove(customer);
                _context.SaveChanges();
            }

            return RedirectToAction("Customer");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}