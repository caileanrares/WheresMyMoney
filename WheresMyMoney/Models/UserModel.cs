using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WheresMyMoney.Models
{
    public class UserModel
    {
        public Guid UserId { get; set; }
        [Required(ErrorMessage ="Mandatory Field")]
        [StringLength(50,ErrorMessage ="String too long (max. 50 chars")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Mandatory Field")]
        [StringLength(50, ErrorMessage = "String too long (max. 50 chars")]
        public string LastName { get; set; }
        public  string  Email { get; set; }

    }
}