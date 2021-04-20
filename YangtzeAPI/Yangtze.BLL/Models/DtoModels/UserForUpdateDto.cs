using System;
using System.Collections.Generic;
using System.Text;

namespace Yangtze.BLL.Models.DtoModels
{
    public class UserForUpdateDto : UserForRegisterDto
    {
        public string OldPassword { get; set; }
        public bool IsActive { get; set; }
    }
}
