using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Models
{
    public partial class ProductReview
    {
        public ProductReview()
        {
            InverseParent = new HashSet<ProductReview>();
        }

        public int ReviewId { get; set; }
        public int ProductId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public short? Rating { get; set; }
        public byte Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string Description { get; set; }

        public virtual ProductReview Parent { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductReview> InverseParent { get; set; }
    }
}
