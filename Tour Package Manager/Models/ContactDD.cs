using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class ContactDD
    {
        public int ContactAutoId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string status { get; set; }
    }
}