using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.BLL.Models
{
    public class CategoryForUpdateDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
