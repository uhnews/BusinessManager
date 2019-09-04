using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(60)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }
        //[Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }
        [DefaultValue(0)]
        [Range(0, 1000000)]
        public int Quantity { get; set; }
        [DefaultValue(false)]
        public bool IsService { get; set; }
    }
}
