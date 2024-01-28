using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.DataAccessLayer
{
    public class ProductUnitRepository
    {
        private readonly IConfiguration _config;

        public ProductUnitRepository(IConfiguration config)
        {
            _config = config;
        }

        private IDbConnection Connection => new SqlConnection(_config.GetConnectionString("DefaultConnection"));

        public async Task<IEnumerable<ProductUnit>> GetAllProductUnits()
        {
            using var connection = Connection;
            connection.Open();
            return await connection.QueryAsync<ProductUnit>("SELECT * FROM product_units");
        }

        public async Task<ProductUnit> GetProductUnitById(int id)
        {
            using var connection = Connection;
            connection.Open();
            return await connection.QueryFirstOrDefaultAsync<ProductUnit>("SELECT * FROM product_units WHERE product_unit_id = @Id", new { Id = id });
        }

        public async Task<int> AddProductUnit(ProductUnit productUnit)
        {
            using var connection = Connection;
            connection.Open();
            return await connection.ExecuteAsync("INSERT INTO product_units (product_unit_key, product_unit_name) VALUES (@ProductUnitKey, @ProductUnitName)",
                new { ProductUnitKey = productUnit.ProductUnitKey, ProductUnitName = productUnit.ProductUnitName });
        }

        public async Task<int> UpdateProductUnit(ProductUnit productUnit)
        {
            using var connection = Connection;
            connection.Open();
            return await connection.ExecuteAsync("UPDATE product_units SET product_unit_key = @ProductUnitKey, product_unit_name = @ProductUnitName WHERE product_unit_id = @ProductUnitId",
                new { ProductUnitId = productUnit.ProductUnitId, ProductUnitKey = productUnit.ProductUnitKey, ProductUnitName = productUnit.ProductUnitName });
        }

        public async Task<int> DeleteProductUnit(int id)
        {
            using var connection = Connection;
            connection.Open();
            return await connection.ExecuteAsync("DELETE FROM product_units WHERE product_unit_id = @Id", new { Id = id });
        }
    }
}
