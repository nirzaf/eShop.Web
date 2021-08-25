using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eShop.Web.Common.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public string Password { get; set; }
    }
}
