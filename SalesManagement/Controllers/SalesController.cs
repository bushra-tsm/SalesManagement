using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;

namespace SalesManagement.Controllers
{
    [Authorize(Roles = ("SalesRep"))]
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SalesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Load all sales, including customer and sale details with products
            var sales = _context.Sales
                .Include(s => s.Customer)               // Load customer info
                .Include(s => s.SaleDetails)            // Load sale details
                    .ThenInclude(sd => sd.Product)      // Load product info for each detail
                .OrderByDescending(s => s.SaleDate)    // Optional: latest sales first
                .ToList();

            //// Optionally, map to a view model if needed
            //var saleViewModels = sales.Select(s => new SaleListViewModel
            //{
            //    SaleId = s.SaleId,
            //    CustomerName = s.Customer.Name,
            //    SaleDate = s.SaleDate,
            //    TotalAmount = s.TotalAmount,
            //    Details = s.SaleDetails.Select(d => new SaleDetailViewModel
            //    {
            //        ProductName = d.Product.Name,
            //        Quantity = d.Quantity,
            //        UnitPrice = d.UnitPrice
            //    }).ToList()
            //}).ToList();

            return View(sales);
        }

        // GET: Sales/Create
        public IActionResult Create()
        {
            var vm = new SaleViewModel
            {
                Customers = _context.Customers.ToList(),
                Products = _context.Products.ToList()
            };
            return View(vm);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaleViewModel vm)
        {
            try
            {
                var product = _context.Products.Find(vm.ProductId);
                var customer = _context.Customers.Find(vm.CustomerId);

                if (product == null || customer == null)
                    return BadRequest();

                if (product.Stock < vm.Quantity)
                {
                    ModelState.AddModelError("", "Not enough stock available.");
                    vm.Customers = _context.Customers.ToList();
                    vm.Products = _context.Products.ToList();
                    return View(vm);
                }

                var sale = new Sale
                {
                    CustomerId = vm.CustomerId,
                    SaleDate = DateTime.Now,
                    TotalAmount = product.Price * vm.Quantity,
                    SaleDetails = new List<SaleDetail>
            {
                new SaleDetail
                {
                    ProductId = product.ProductId,
                    Quantity = vm.Quantity,
                    UnitPrice = product.Price
                }
            }
                };

                // Deduct stock
                product.Stock -= vm.Quantity;
                _context.Products.Update(product);

                _context.Sales.Add(sale);

                // Save everything
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Show the exact error
                return Content("Error saving: " + ex.Message + "\n" + ex.InnerException?.Message);
            }
        }

     

     
            // GET: Sales/Details/5
            public IActionResult Details(int id)
            {
                if (id == 0)
                    return BadRequest();

                var sale = _context.Sales
                    .Include(s => s.Customer)
                    .Include(s => s.SaleDetails)
                        .ThenInclude(sd => sd.Product)
                    .FirstOrDefault(s => s.SaleId == id);

                if (sale == null)
                    return NotFound();

                return View(sale);
            }




        // GET: Sales/Delete/5
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var sale = _context.Sales
                .Include(s => s.Customer)
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .FirstOrDefault(s => s.SaleId == id);

            if (sale == null)
                return NotFound();

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var sale = _context.Sales
                .Include(s => s.SaleDetails)
                    .ThenInclude(sd => sd.Product)
                .FirstOrDefault(s => s.SaleId == id);

            if (sale != null)
            {
                // Restore stock
                foreach (var detail in sale.SaleDetails)
                {
                    var product = _context.Products.Find(detail.ProductId);
                    if (product != null)
                        product.Stock += detail.Quantity;
                }

                _context.Sales.Remove(sale);
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }





        

    }
}





