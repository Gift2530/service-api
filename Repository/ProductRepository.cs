using Dapper;
using ProductData.Models;
using System.Data;
using System.Data.SqlClient;

namespace employee.Repository
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Products> GetAll()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Products>("SELECT p.pid, p.Name, p.Price,s.qty FROM Products p INNER JOIN ProductStock s ON p.pid = s.pid where status = 'Active'");
            }
        }

        public async Task<Products> GetByIdAsync(string pid)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var parameters = new { pid = pid };
                return await db.QueryFirstOrDefaultAsync<Products>("SELECT * FROM Products WHERE pid = @pid", parameters);
            }
        }

        public async Task<int> UpdateAsync(string pid)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(
                    "UPDATE Products SET Status=@Status WHERE pid = @Pid",
                    new
                    {
                        Pid = pid,
                        Status = "InActive",
                    });
            }
        }

        public async Task<int> UpdateStatusAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.ExecuteAsync(
                    "UPDATE Products SET Status=@Status",
                    new
                    {
                        Status = "InActive",
                    });
            }
        }
    }
}
