using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class ProductCategory : BaseEntity
    {
        [StringLength(50)]
        public string Category { get; set; }
    }
}
