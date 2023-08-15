using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _conn;
        public DapperProductRepository(IDbConnection conn)
        {
            _conn = conn;
        }

        public void CreateProduct(string name, double price, int categoryID)
        {
            _conn.Execute("INSERT INTO products (Name, Price, CategoryID) " +
                                "VALUES (@name, @price, @categoryID);"
                , new { name = name, price = price, categoryID = categoryID });
        }


        public IEnumerable<Product> GetAllProducts()
        {
            return _conn.Query<Product>("Select * From Products;");
        }

        public void UpdateProductName(int productID, string updatedName)
        {
            _conn.Execute("UPDATE products SET Name = @updatedName WHERE ProductID = @productID;",
                new { updatedName = updatedName, productID = productID });
        }
        public void DeleteProduct(int productID)
        {
            _conn.Execute("DELETE FROM Reviews WHERE ProductID = @productID;",
                new { productID = productID });

            _conn.Execute("DELETE FROM Sales WHERE ProductID = @productID;",
                new { productID = productID });

            _conn.Execute("DELETE FROM Products WHERE ProductID = @productID;",
                new { productID = productID });
        }
    }
}
