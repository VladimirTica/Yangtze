using System;
using System.Collections.Generic;
using System.Text;
using Yangtze.DAL.Entities;

namespace Yangtze.BLL.Models
{
    public class CategoryForUpdateDto
    {
        public int? ParentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<CategoryDto> InverseParent = null;

        public CategoryForUpdateDto()
        {
            InverseParent = new HashSet<CategoryDto>();
        }
    }
}
