using BusinessManager.Core.Models;
using System.Web;

namespace BusinessManager.Core.Contracts
{
    public interface IAttachmentService
    {
        object AddAttachment(IRepository<Attachment> attachmentContext, string data, HttpPostedFileBase file);
        object DeleteAttachment(IRepository<Attachment> attachmentContext, string Id);
    }
}
