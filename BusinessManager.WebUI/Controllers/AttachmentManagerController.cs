using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessManager.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AttachmentManagerController : Controller
    {
        readonly IRepository<Attachment> attachmentContext;

        // dependency injection
        public AttachmentManagerController(IRepository<Attachment> attachmentContext)
        {
            this.attachmentContext = attachmentContext;
        }

        // GET: AttachmentManager
        public ActionResult Index()
        {
            List<Attachment> attachments = attachmentContext.Collection().ToList();
            return View(attachments);
        }

        // GET: /AttachmentManager/Create Page
        public ActionResult Create()
        {
            Attachment attachment = new Attachment();
            return View(attachment);
        }

        // POST: /AttachmentManager/Create Page
        [HttpPost]
        public ActionResult Create(Attachment attachment, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(attachment);
            }
            else
            {
                attachmentContext.Insert(attachment);
                attachmentContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /AttachmentManager/Edit Page
        public ActionResult Edit(string Id)
        {
            Attachment attachment = attachmentContext.Find(Id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(attachment);
            }
        }

        // POST: /AttachmentManager/Edit Page
        [HttpPost]
        public ActionResult Edit(Attachment attachment, string Id, HttpPostedFileBase file)
        {
            Attachment attachmentToEdit = attachmentContext.Find(Id);
            if (attachmentToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(attachment);
                }

                attachmentToEdit.CustomerId = attachment.CustomerId;
                //attachmentToEdit.UserName = attachment.UserName;
                attachmentToEdit.Category = attachment.Category;
                //attachmentToEdit.NoteBody = attachment.NoteBody;
                attachmentToEdit.CustomerId = attachment.CustomerId;
                attachmentToEdit.CreatedAt = attachment.CreatedAt;

                attachmentContext.Commit();
                return RedirectToAction("Index");
            }
        }

        // GET: /AttachmentManager/Delete Page
        public ActionResult Delete(string Id)
        {
            Attachment attachmentToDelete = attachmentContext.Find(Id);

            if (attachmentToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(attachmentToDelete);
            }
        }

        // POST: /AttachmentManager/Delete Page
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Attachment attachmentToDelete = attachmentContext.Find(Id);

            if (attachmentToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                attachmentContext.Delete(Id);
                attachmentContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}