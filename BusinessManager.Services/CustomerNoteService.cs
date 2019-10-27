using BusinessManager.Core.Contracts;
using BusinessManager.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Services
{
    public class CustomerNoteService : ICustomerNoteService
    {
        public object AddCustomerNote(IRepository<CustomerNote> customernoteContext, string data)
        {
            {
                var customernote = JsonConvert.DeserializeObject<CustomerNote>(data);
                customernote.Id = Guid.NewGuid().ToString();

                try
                {
                    customernoteContext.Insert(customernote);
                    customernoteContext.Commit();

                    // send response object
                    return new { Successful = true, Message = "Note added.", CustomerNoteId = customernote.Id };
                }
                catch (Exception ex)
                {
                    // log error;
                    Console.WriteLine(ex);

                    // send response object error
                    return new { Successful = false, Message = "Note failed to add." };
                }
            }
        }

        public object DeleteCustomerNote(IRepository<CustomerNote> customernoteContext, string Id)
        {
            try
            {
                customernoteContext.Delete(Id);
                customernoteContext.Commit();

                // send response object
                return new { Successful = true, Message = "Note deleted." };
            }
            catch (Exception ex)
            {
                // log error;
                Console.WriteLine(ex);

                // send response object error
                return new { Successful = false, Message = "Note failed to delete." };
            }
        }
    }
}
