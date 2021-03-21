﻿using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Entities
{
    public partial class CartItem : BaseEntity 
    {
        public int ProductId { get; set; }
        public int CartId { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public short Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Description { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
    }
}
