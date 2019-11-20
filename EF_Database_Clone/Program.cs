using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Database_Clone
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DataModelContext();
            var products = from p in db.Products
                           select p;
            Console.WriteLine("產生產品如下:");
            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductID}, {p.ProductName}, {p.UnitPrice}, { p.UnitsInStock}");
            }
            Console.WriteLine("፝請按任一鑑離開...");
            Console.ReadKey();
            db.Dispose();
        }
    }
}