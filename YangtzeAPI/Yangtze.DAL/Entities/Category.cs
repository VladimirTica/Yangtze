﻿using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Entities
{
    public partial class Category: BaseEntity
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual Category Parent { get; set; }
        public virtual ICollection<Category> InverseParent { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
