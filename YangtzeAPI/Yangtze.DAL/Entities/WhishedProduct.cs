using System;
using System.Collections.Generic;

namespace Yangtze.DAL.Entities
{
    public partial class WhishedProduct : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public User User { get; set; }
        public Product Product { get; set; }
    }
}
