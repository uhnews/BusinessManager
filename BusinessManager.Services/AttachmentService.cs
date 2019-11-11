using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using Newtonsoft.Json;
using System;
using System.Web;

namespace BusinessManager.Services
{
    public class AttachmentService : IAttachmentService
    {
        public object AddAttachment(IRepository<Attachment> attachmentContext, string data, HttpPostedFileBase file)
        {
            {
                var attachment = JsonConvert.DeserializeObject<Attachment>(data);
                attachment.Id = Guid.NewGuid().ToString();
                if (file != null)
                {
                    try
                    {
                        attachmentContext.Insert(attachment);
                        attachmentContext.Commit();

                        // send response object
                        return new { Successful = true, Message = "Attachment added.", AttachmentId = attachment.Id };
                    }
                    catch (Exception ex)
                    {
                        // log error;
                        Console.WriteLine(ex);

                        // send response object error
                        return new { Successful = false, Message = "Attachment failed to add." };
                    }
                }
                else
                {
                    return new { Successful = false, Message = "Attachment was not received by server." };
                }
            }
        }

        public object DeleteAttachment(IRepository<Attachment> attachmentContext, string Id)
        {
            try
            {
                attachmentContext.Delete(Id);
                attachmentContext.Commit();

                // send response object
                return new { Successful = true, Message = "Attachment deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Attachment failed to delete." };
            }
        }
    }
}
