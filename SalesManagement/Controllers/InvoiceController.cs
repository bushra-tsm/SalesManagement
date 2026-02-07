
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.CodeAnalysis.RulesetToEditorconfig;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;
using SalesManagement.Data;
using SalesManagement.Models;
using Rotativa.AspNetCore;


namespace SalesManagement.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
          // <-- Assign it
        }
        public IActionResult Create()
        {
            var vm = new InvoiceVM
            {
                Customers = _context.Customers
                    .Select(c => new SelectListItem
                    {
                        Value = c.CustomerId.ToString(),
                        Text = c.Name
                    }).ToList(),

                Products = _context.Products
                    .Select(p => new SelectListItem
                    {
                        Value = p.ProductId.ToString(),
                        Text = p.Name
                    }).ToList(),

                ProductIds = new List<int>(),
                Quantities = new List<int>()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(InvoiceVM vm)
        {
            var invoice = new Invoice
            {
                CustomerId = vm.CustomerId,
                Items = new List<InvoiceItem>()
            };

            decimal grandTotal = 0;

            for (int i = 0; i < vm.ProductIds.Count; i++)
            {
                var product = _context.Products
                    .First(p => p.ProductId == vm.ProductIds[i]);

                var item = new InvoiceItem
                {
                    ProductId = product.ProductId,
                    Price = product.Price,
                    Quantity = vm.Quantities[i]
                };

                product.Stock -= vm.Quantities[i];
                grandTotal += item.Total;

                invoice.Items.Add(item);
            }

            invoice.GrandTotal = grandTotal;

            _context.Invoices.Add(invoice);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = invoice.InvoiceId });
        }

        public IActionResult Details(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .ThenInclude(it => it.Product)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
                return NotFound();

            return View(invoice);
        }

        // AJAX
        public JsonResult GetCustomerEmail(int id)
        {
            var email = _context.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => c.Email)
                .FirstOrDefault();
            return Json(email);
        }

        public JsonResult GetProductPrice(int id)
        {
            var price = _context.Products
                .Where(p => p.ProductId == id)
                .Select(p => p.Price)
                .FirstOrDefault();
            return Json(price);
        }







        // Make sure this is included

        public IActionResult PrintPdf(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.Customer)
                .Include(i => i.Items)
                .ThenInclude(it => it.Product)
                .FirstOrDefault(i => i.InvoiceId == id);

            if (invoice == null)
                return NotFound();

            return new ViewAsPdf("InvoiceReport", invoice)
            {
                FileName = $"Invoice_{id}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
            };
        }

        //// Helper to render Razor to string
        //private string RenderViewToString(ControllerContext context, string viewName, object model)
        //    {
        //        var viewEngine = context.HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
        //        var tempDataProvider = context.HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;
        //        var serviceProvider = context.HttpContext.RequestServices.GetService(typeof(IServiceProvider)) as IServiceProvider;

        //        var viewResult = viewEngine.GetView(null, viewName, false);

        //        if (!viewResult.Success)
        //            throw new InvalidOperationException($"View {viewName} not found");

        //        var view = viewResult.View;

        //        using var sw = new StringWriter();
        //        var viewContext = new ViewContext(context, view, new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model }, new TempDataDictionary(context.HttpContext, tempDataProvider), sw, new HtmlHelperOptions());
        //        view.RenderAsync(viewContext).Wait();

        //        return sw.ToString();
        //    }
    }

}