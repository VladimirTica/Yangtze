﻿using System;
using System.Collections.Generic;

namespace YangtzeAPI.Entities
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
