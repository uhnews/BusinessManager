using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManager.Core.Models
{
    public class Sequence : BaseEntity
    {
        [StringLength(50)]
        [Required]
        public string SequenceName { get; set; }
        public int SequenceNumber { get; set; }
    }
}