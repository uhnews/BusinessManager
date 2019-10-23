using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomerNoteManagerController : Controller
    {
        readonly IRepository<CustomerNote> customernoteContext;

        // dependency injection
        public CustomerNoteManagerController(IRepository<CustomerNote> customernoteContext)
        {
            this.customernoteContext = customernoteContext;
        }

        // GET: CustomerNotesManager
        public ActionResult Index()
        {
            List<CustomerNote> customernotes = customernoteContext.Collection().ToList();
            return View(customernotes);
        }

        // GET: /CustomerNoteManager/Create Page
        public ActionResult Create()
        {
            CustomerNote customernote = new CustomerNote();
            return View(customernote);
        }

        // POST: /CustomerNoteManager/Create Page
        [HttpPost]
        public ActionResult Create(CustomerNote customernote, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(customernote);
            }
            else
            {
                customernoteContext.Insert(customernote);
                customernoteContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /CustomerNoteManager/Edit Page
        public ActionResult Edit(string Id)
        {
            CustomerNote customernote = customernoteContext.Find(Id);
            if (customernote == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(customernote);
            }
        }

        // POST: /CustomerNoteManager/Edit Page
        [HttpPost]
        public ActionResult Edit(CustomerNote customernote, string Id, HttpPostedFileBase file)
        {
            CustomerNote noteToEdit = customernoteContext.Find(Id);
            if (noteToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(customernote);
                }

                noteToEdit.CustomerId = customernote.CustomerId;
                noteToEdit.UserName = customernote.UserName;
                noteToEdit.Category = customernote.Category;
                noteToEdit.NoteBody = customernote.NoteBody;
                noteToEdit.CustomerId = customernote.CustomerId;
                noteToEdit.CreatedAt = customernote.CreatedAt;

                customernoteContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /CustomerNoteManager/Delete Page
        public ActionResult Delete(string Id)
        {
            CustomerNote noteToDelete = customernoteContext.Find(Id);

            if (noteToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(noteToDelete);
            }
        }

        // POST: /CustomerNoteManager/Delete Page
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            CustomerNote noteToDelete = customernoteContext.Find(Id);

            if (noteToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                customernoteContext.Delete(Id);
                customernoteContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}