using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BusinessManager.Core.Models
{
    public class CustomerNote : BaseEntity
    {
        [StringLength(128)]
        [Required]
        public string CustomerId { get; set; }

        [DisplayName("User Name")]
        [StringLength(50)]
        [Required]
        public string UserName { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [DisplayName("Note")]
        public string NoteBody { get; set; }
    }
}
