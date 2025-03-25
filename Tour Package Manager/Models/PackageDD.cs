using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Tour_Package_Manager.Models
{
    public class PackageDD
    {
        public int PackageAutoId { get; set; }
        public string PackageName { get; set; }
        public string ShortDiscription { get; set; }
        public string Discription { get; set; }
        public double Price { get; set; }
        public string DurationAutoId { get; set; }
        public string CatagoryAutoId { get; set; }
        public string LocationAutoId { get; set; }
        public string PackageImage { get; set; }
        public string PackagePdf { get; set; }
        public string PackageStart { get; set; }
        public string PackageEnd { get; set; }

        public string StatusAutoId { get; set; }
        
    }
}