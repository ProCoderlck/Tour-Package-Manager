using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class BlogDD
    { 
            public int BlogAutoId { get; set; }
            public string BlogTitle { get; set; }
            public string UserAutoId { get; set; }
            public string BlogCatagoryAutoId { get; set; }
            public string Discription { get; set; }
            public string Date { get; set; }
            public string Day { get; set; }
            public string Month { get; set; }
            public string Photo { get; set; }
            public string UserImage { get; set; }
     }
}