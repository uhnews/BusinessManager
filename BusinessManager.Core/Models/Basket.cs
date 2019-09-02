using System.Collections.Generic;

namespace BusinessManager.Core.Models
{
    public class Basket : BaseEntity
    {
        public virtual ICollection<BasketItem> BasketItems { get; set; }
        public Basket()
        {
            this.BasketItems = new List<BasketItem>();
        }
    }
}
