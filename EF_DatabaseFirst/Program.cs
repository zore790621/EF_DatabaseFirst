using System;
using System.Linq;
using System.Data.Entity;

namespace EF_DatabaseFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            Read();
            //Create();
        }

        static void ListProducts()
        {
            var db = new NorthwindContext();

            var products = from p in db.Products
                           select p;

            Console.WriteLine("產品資訊如下:");

            foreach (var p in products)
            {
                Console.WriteLine($"{p.ProductID}, {p.ProductName}, {p.UnitPrice}, {p.UnitsInStock}");
            }

            Console.WriteLine("請按任一鍵後離開...");
            Console.ReadKey();

            db.Dispose();   //關閉EF資料庫連線


            //使用using(){...}陳述式呼叫Dispose()方法
            using (var DB = new NorthwindContext())
            {
                var Products = from p in DB.Products
                               select p;

                Console.WriteLine("產品資訊如下:");

                foreach (var p in Products)
                {
                    Console.WriteLine($"{p.ProductID}, {p.ProductName}, {p.UnitPrice}, {p.UnitsInStock}");
                }

                Console.WriteLine("請按任一鍵後離開...");
                Console.ReadKey();
            }
        }

        static void Read()
        {
            //讀取庫存為零的產品清單資訊,並顯示產品類別及供應商資訊,預備連絡補貨
            using (var db = new NorthwindContext())
            {
                //載入關聯Entities資料
                var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);

                //查詢庫存為零的產品,分類及供應商資訊
                var query = from p in products
                            where p.UnitsOnOrder > 0
                            orderby p.Supplier.CompanyName ascending, p.Category.CategoryName descending, p.ProductName
                            select new { p.Supplier.CompanyName, p.Category.CategoryName, p.ProductID, p.ProductName, p.UnitsOnOrder };

                Console.WriteLine("在途訂單之訂購資訊:");
                foreach(var q in query)
                {
                    Console.WriteLine($"供應商:{q.CompanyName},分類: {q.CategoryName}, 產品: {q.ProductID}, {q.ProductName}, 訂購數量:{q.UnitsOnOrder}");
                }

                //Console.WriteLine(soldout.ToString());
                
                Console.ReadKey();
            }
        }

        static void Create()
        {
            using (var db = new NorthwindContext())
            {
                //新增分類
                Category c = new Category { CategoryName = "Car", Description = "Automobile" };

                db.Categories.Add(c);
                db.SaveChanges();
            }
        }

        static void Update()
        {
            using (var db = new NorthwindContext())
            {

            }
        }

        static void Delete()
        {
            using (var db = new NorthwindContext())
            {

            }
        }
    }
}
