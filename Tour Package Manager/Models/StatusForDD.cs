using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class StatusForDD
    {
        public int StatusAutoId { get; set; }
        public string StatusName { get; set; }
        public string StatusType { get; set; }

    }
    public class DurationForDD
    {
        public int DurationAutoId { get; set; }
        public string DurationName { get; set; }

    }
    public class CategoryForDD
    {
        public int CategoryAutoId { get; set; }
        public string CategoryName { get; set; }

    }
    public class LocationForDD
    {
        public int LocationAutoId { get; set; }
        public string LocationName { get; set; }

    }
    public class BlogCategoryForDD
    {
        public int BlogCategoryAutoId { get; set; }
        public string CategoryName { get; set; }

    }
    public class UserForDD
    {
        public int UserAutoId { get; set; }
        public string UserName { get; set; }

    }


}