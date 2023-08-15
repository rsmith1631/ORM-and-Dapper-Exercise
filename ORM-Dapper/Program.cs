using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);
            #region Department Section
            var departmentRepo = new DapperDepartmentRepository(conn);
             var departments = departmentRepo.GetAllDepartments();

            departmentRepo.InsertDepartment("Accounting");

             foreach (var department in departments)
             {
                 Console.WriteLine(department.DepartmentID);
                 Console.WriteLine(department.Name);
                 Console.WriteLine();
                 Console.WriteLine();
             }
            
            #endregion

            var productRepo = new DapperProductRepository(conn);
            var products = productRepo.GetAllProducts();

            productRepo.CreateProduct("TV", 132.00, 22);

            foreach (var product in products)
            {
                Console.WriteLine(product.ProductID);
                Console.WriteLine(product.Name);
                Console.WriteLine(product.Price);
                Console.WriteLine(product.CategoryID);
                Console.WriteLine(product.OnSale);
                Console.WriteLine(product.OnSale);
                Console.WriteLine();
                Console.WriteLine();
            }
           
        }
    }
}
