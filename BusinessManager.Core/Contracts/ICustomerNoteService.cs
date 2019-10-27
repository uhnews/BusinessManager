using BusinessManager.Core.Models;

namespace BusinessManager.Core.Contracts
{
    public interface ICustomerNoteService
    {
        object AddCustomerNote(IRepository<CustomerNote> customernoteContext, string data);
        object DeleteCustomerNote(IRepository<CustomerNote> customernoteContext, string Id);
    }
}
