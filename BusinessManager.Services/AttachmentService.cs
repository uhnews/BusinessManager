using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web;

namespace BusinessManager.Services
{
    public class AttachmentService : IAttachmentService
    {
        public object AddAttachment(IRepository<Attachment> attachmentContext, string data, HttpPostedFileBase file)
        {
            {
                var attachment = JsonConvert.DeserializeObject<Attachment>(data);
                string fileLocation = attachment.Location;
                System.IO.Directory.CreateDirectory(fileLocation);
                if (file != null && file.ContentLength > 0)
                {
                    try
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        fileName = Helpers.AppUtils.getNextFileName(fileLocation, fileName);
                        if (fileName != "")
                        {
                            file.SaveAs(Path.Combine(fileLocation, fileName));

                            attachmentContext.Insert(attachment);
                            attachmentContext.Commit();

                            // send response object
                            return new { Successful = true, Message = "Attachment added.", AttachmentId = attachment.Id };
                        }
                        else
                        {
                            // log error;
                            Console.WriteLine("Invalid file name or location.");
                            return new { Successful = false, Message = "Attachment failed to add." };
                        }
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
                Attachment attachment = attachmentContext.Find(Id);

                attachmentContext.Delete(Id);
                attachmentContext.Commit();
                File.Delete(attachment.Location + "\\" + attachment.FileName);

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
