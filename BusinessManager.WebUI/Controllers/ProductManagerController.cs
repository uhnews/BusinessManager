using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using BusinessManager.Core.ViewModels;
using BusinessManager.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {
        readonly IRepository<Product> productContext;
        readonly IRepository<ProductCategory> productCategoryConext;
        readonly ProductRetrieveService productRetrieveService = new ProductRetrieveService();

        // dependency injection
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryConext)
        {
            this.productContext = productContext;
            this.productCategoryConext = productCategoryConext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = productContext.Collection().ToList();
            return View(products);
        }

        // GET: /ProductManager/Create Page
        public JsonResult CheckForUniqueCodes(string upc, string productCode, string exceptId = "")
        {

            bool upcFound = productRetrieveService.FindUPC(upc, exceptId);
            bool prodCodeFound = productRetrieveService.FindProductCode(productCode, exceptId);

            return Json(new { UPC = upcFound, ProductCode = prodCodeFound }, JsonRequestBehavior.AllowGet);
        }

        // GET: /ProductManager/Create Page
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategoryConext.Collection();

            return View(viewModel);
        }

        // POST: /ProductManager/Create Page
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + product.Image));
                }
                productContext.Insert(product);
                productContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /ProductManager/Edit Page
        public ActionResult Edit(string Id)
        {
            Product product = productContext.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = productCategoryConext.Collection();
                return View(viewModel);
            }
        }

        // POST: /ProductManager/Edit Page
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = productContext.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.WholesalePrice = product.WholesalePrice;
                productToEdit.SupplierPrice = product.SupplierPrice;
                productToEdit.Quantity = product.Quantity;
                productToEdit.QuantityMin = product.QuantityMin;
                productToEdit.UPC = product.UPC;
                productToEdit.ProductCode = product.ProductCode;
                productToEdit.IsService = product.IsService;
                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//" + productToEdit.Image));
                }

                productContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /ProductManager/Delete Page
        public ActionResult Delete(string Id)
        {
            Product productToDelete = productContext.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        // POST: /ProductManager/Delete Page
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = productContext.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                productContext.Delete(Id);
                productContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}