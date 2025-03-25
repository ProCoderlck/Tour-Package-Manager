using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    
    public class UserDD
    {
        public int UserAutoId { get; set; }
        public string UserName { get; set; }
        public int MobileNo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StatusAutoId { get; set; }
        public string UserImage { get; set; }

    }
}