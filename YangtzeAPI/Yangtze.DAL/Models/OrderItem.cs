using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Models
{
    public partial class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public short Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Description { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
