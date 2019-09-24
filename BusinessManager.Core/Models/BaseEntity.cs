using System;
using System.ComponentModel;

namespace BusinessManager.Core.Models
{
    public abstract class BaseEntity
    {
        public string Id { get; set; }

        [DisplayName("Created At")]
        public DateTimeOffset CreatedAt { get; set; }

        [DisplayName("Modified At")]
        public DateTimeOffset ModifiedAt { get; set; }

        public BaseEntity()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
            this.ModifiedAt = DateTime.Now;
        }
    }
}
