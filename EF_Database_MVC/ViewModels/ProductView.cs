using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EF_Database_MVC.ViewModels
{
    public class ProductView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Tax { get; set; }
        public int UnitsInStock { get; set; }
    }
}