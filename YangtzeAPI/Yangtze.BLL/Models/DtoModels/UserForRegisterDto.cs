using System;
using System.ComponentModel.DataAnnotations;

namespace Yangtze.BLL.Models.DtoModels
{
    public class UserForRegisterDto : UserBaseDto
    {

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify password between 4 and 8 characters")]
        public string Password { get; set; }
        public string Mobile { get; set; }
    }
}
