using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class Attachment : BaseEntity
    {
        [StringLength(128)]
        [Required]
        public string CustomerId { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string FileName { get; set; }

        [StringLength(300)]
        public string Location { get; set; }

        [DisplayName("Attached By")]
        [StringLength(50)]
        [Required]
        public string AttachedBy { get; set; }
    }
}
