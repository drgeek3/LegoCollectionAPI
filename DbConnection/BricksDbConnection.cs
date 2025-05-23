using LegoCollection.Entities;
using MySqlConnector;

namespace LegoCollection.DbConnection
{
    public class BricksDbConnection
    {
        private IConfiguration Configuration;

        private string? _connectionString;
        public BricksDbConnection(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public string GetConnectionString()
        {
            return this.Configuration.GetConnectionString("DefaultConnection");
        }

        //Bricks Calls
        //GET all Bricks
        public async Task<List<BricksEntity>> GetBricks()
        {
            List<BricksEntity> bricksResult = new List<BricksEntity>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM BRICKS_REPORT";
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        bricksResult.Add(new BricksEntity()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            BrickId = rdr.GetValue(1).ToString() ?? string.Empty,
                            Description = rdr.GetValue(2).ToString() ?? string.Empty,
                            Category = rdr.GetValue(3).ToString() ?? string.Empty,
                            Subcategory = rdr.GetValue(4).ToString() ?? string.Empty,
                            AltBrickId = rdr.GetValue(5).ToString() ?? string.Empty
                        });
                    }
                    con.Close();
                }
            }
            return bricksResult;
        }

        //GET bricks by id
        public async Task<BricksEntity> GetBrickById(int? id)
        {
            var brickResult = new BricksEntity();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM BRICKS_REPORT WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        brickResult.Id = Convert.ToInt32(rdr.GetValue(0));
                        brickResult.BrickId = rdr.GetValue(1).ToString() ?? string.Empty;
                        brickResult.Description = rdr.GetValue(2).ToString() ?? string.Empty;
                        brickResult.Category = rdr.GetValue(3).ToString() ?? string.Empty;
                        brickResult.Subcategory = rdr.GetValue(4).ToString() ?? string.Empty;
                        brickResult.AltBrickId = rdr.GetValue(5).ToString() ?? string.Empty;
                    }
                    con.Close();
                }

            }
            return brickResult;
        }

        //GET brick by brick id
        public async Task<List<BricksEntity>> GetBrickByBrickId(string? id)
        {
            List<BricksEntity> bricksResult = new List<BricksEntity>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM BRICKS_REPORT WHERE BRICK_ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        bricksResult.Add(new BricksEntity()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            BrickId = rdr.GetValue(1).ToString() ?? string.Empty,
                            Description = rdr.GetValue(2).ToString() ?? string.Empty,
                            Category = rdr.GetValue(3).ToString() ?? string.Empty,
                            Subcategory = rdr.GetValue(4).ToString() ?? string.Empty,
                            AltBrickId = rdr.GetValue(5).ToString() ?? string.Empty
                        });
                    }
                    con.Close();
                }

            }
            return bricksResult;
        }

        //POST new bricks, probably won't use
        public async Task<int> AddBrick(BricksEntity newBrick)
        {
            int newId = 0;
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL ADD_NEW_BRICK (@brickid, @description, @category, @subcategory, @altbrickid)";
                    cmd.Parameters.AddWithValue("@brickid", newBrick.BrickId);
                    cmd.Parameters.AddWithValue("@description", newBrick.Description);
                    cmd.Parameters.AddWithValue("@category", newBrick.Category);
                    cmd.Parameters.AddWithValue("@subcategory", newBrick.Subcategory);
                    cmd.Parameters.AddWithValue("@altbrickid", newBrick.AltBrickId);
                    con.Open();
                    await cmd.ExecuteNonQueryAsync();

                    //Get the new Id
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT ID FROM BRICKS ORDER BY ID DESC LIMIT 1";
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read()) { newId = Convert.ToInt32(rdr.GetValue(0)); }

                    con.Close();
                }
            }
            return newId;
        }

        //PUT updated bricks, probably used a lot
        public async void UpdateBrick(BricksEntity updateBrick)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL UPDATE_BRICK (@id, @brickid, @description, @category, @subcategory, @altbrickid)";
                    cmd.Parameters.AddWithValue("@id", updateBrick.Id);
                    cmd.Parameters.AddWithValue("@brickid", updateBrick.BrickId);
                    cmd.Parameters.AddWithValue("@description", updateBrick.Description);
                    cmd.Parameters.AddWithValue("@category", updateBrick.Category);
                    cmd.Parameters.AddWithValue("@subcategory", updateBrick.Subcategory);
                    cmd.Parameters.AddWithValue("@altbrickid", updateBrick.AltBrickId);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        public async void DeleteBrick(int id)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE FROM BRICKS WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        //GET the Categories Table
        public async Task<List<CategoriesEntity>> GetCategories()
        {
            List<CategoriesEntity> categoriesResult = new List<CategoriesEntity>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM CATEGORIES ORDER BY CATEGORY";
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        categoriesResult.Add(new CategoriesEntity()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            Category = rdr.GetValue(1).ToString() ?? string.Empty,
                            IsMain = rdr.GetBoolean(2),
                            Subcat = rdr.GetValue(3).ToString() ?? string.Empty
                        });
                    }
                    con.Close();
                }
            }
            return categoriesResult;
        }
    }
}
