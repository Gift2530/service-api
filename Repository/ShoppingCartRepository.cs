using Dapper;
using ProductData.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ProductData.Repository
{
    public class ShoppingCartRepository
    {
        private readonly string _connectionString;

        public ShoppingCartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> UpdateAsync(ShoppingCarts carts)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(
                    "UPDATE [ProductStock] SET Qty=@Qty WHERE Pid = @Pid",
                    new
                    {
                        Qty = carts.qty,
                        Pid = carts.pid
                    });
            }
        }
        public async Task<ShoppingCartsById> GetByIdAsync(string pid)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { pid = pid };
                return await db.QueryFirstOrDefaultAsync<ShoppingCartsById>("SELECT pid,qty FROM ProductStock WHERE pid = @pid", parameters);
            }
        }
    }
}
