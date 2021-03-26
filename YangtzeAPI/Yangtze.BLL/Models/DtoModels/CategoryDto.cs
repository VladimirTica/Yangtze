using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.BLL.Models
{
    public class CategoryDto : CategoryForUpdateDto
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
    }
}
