using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.BLL.Models
{
    public class ProductDto : ProductForUpdateDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
