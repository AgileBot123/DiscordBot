using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ModBot.Domain.DTO
{
   public class LoginDTO
    {
        [Required]
        [DisplayName("User Name")]
        public  string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Password")]
        public string password { get; set; }
    }
}
