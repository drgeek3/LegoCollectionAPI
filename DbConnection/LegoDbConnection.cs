using LegoCollection.Entities;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using LegoCollection.Dtos;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;

namespace LegoCollection.DbConnection
{
    public class LegoDbConnection
    {
        private IConfiguration Configuration;

        private string? _connectionString;
        public LegoDbConnection(IConfiguration _configuration)
        {
            Configuration = _configuration;   
        }

        public string GetConnectionString()
        {
            return this.Configuration.GetConnectionString("DefaultConnection");
        }

        //This is a bunch of similar methods under different names with minimal changes. 
        //Once all is done, probably be a good idea to review these to see if they can be 
        //reduced in some way.
        //They origially were all under dofferent record calls, but I've managed to unify all
        //the Owned ones into the same Entity, which makes sense. That's a good start.

        //Full Brick Calls
        public List<BrickLocation> GetFullBrickReport()
        {
            List<BrickLocation> fullBrickReport = new List<BrickLocation>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    //Explanation: The view that this uses returns too much information under load to return.
                    //In addition, the intent of this page is to search for the brick you need, not to view a report of everything.
                    //Hence, this simply returns something on the main page so it displays some,thing, and then you can search.
                    cmd.CommandText = "SELECT * FROM BRICK_LOCATION_REPORT WHERE BRICK_ID='3001'";                    
                    
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        fullBrickReport.Add(new BrickLocation()
                        {
                            BrickId = rdr.GetValue(0).ToString() ?? string.Empty,
                            Description = rdr.GetValue(1).ToString() ?? string.Empty,
                            Category = rdr.GetValue(2).ToString() ?? string.Empty,
                            Subcategory = rdr.GetValue(3).ToString() ?? string.Empty,
                            Container = rdr.GetValue(4).ToString() ?? string.Empty,
                            Unit = rdr.GetValue(5).ToString() ?? string.Empty,
                            UnitRow = rdr.GetValue(6).ToString() ?? string.Empty,
                            Drawer = rdr.GetValue(7).ToString() ?? string.Empty,
                            Color = rdr.GetValue(8).ToString() ?? string.Empty,
                            NumAvailable = Convert.ToInt32(rdr.GetValue(9)),
                            NumInUse = Convert.ToInt32(rdr.GetValue(10)),
                            AltBrickId = rdr.GetValue(11).ToString() ?? string.Empty,
                            Overloaded = rdr.GetValue(12).ToString() == "Yes" ? true : false,
                            Underfilled = rdr.GetValue(13).ToString() == "Yes" ? true : false,
                            LocEmpty = rdr.GetValue(14).ToString() == "Yes" ? true : false
                        });
                    }
                    con.Close();
                }

            }
            return fullBrickReport;
        }

        public async Task<List<BrickLocation>> GetSingleBrickReport(string brickid)
        {
            List<BrickLocation> singleBrickReport = new List<BrickLocation>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM BRICK_LOCATION_REPORT WHERE BRICK_ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", brickid);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        singleBrickReport.Add(new BrickLocation()
                        {
                            BrickId = rdr.GetValue(0).ToString() ?? string.Empty,
                            Description = rdr.GetValue(1).ToString() ?? string.Empty,
                            Category = rdr.GetValue(2).ToString() ?? string.Empty,
                            Subcategory = rdr.GetValue(3).ToString() ?? string.Empty,
                            Container = rdr.GetValue(4).ToString() ?? string.Empty,
                            Unit = rdr.GetValue(5).ToString() ?? string.Empty,
                            UnitRow = rdr.GetValue(6).ToString() ?? string.Empty,
                            Drawer = rdr.GetValue(7).ToString() ?? string.Empty,
                            Color = rdr.GetValue(8).ToString() ?? string.Empty,
                            NumAvailable = Convert.ToInt32(rdr.GetValue(9)),
                            NumInUse = Convert.ToInt32(rdr.GetValue(10)),
                            AltBrickId = rdr.GetValue(11).ToString() ?? string.Empty,
                            Overloaded = rdr.GetValue(12).ToString() == "Yes" ? true : false,
                            Underfilled = rdr.GetValue(13).ToString() == "Yes" ? true : false,
                            LocEmpty = rdr.GetValue(14).ToString() == "Yes" ? true : false
                        });
                    }
                    con.Close();
                }

            }
            return singleBrickReport;
        }

        public async Task AddCompleteRecordAsync(FullRecordAdd newRecord)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    string strOverloaded = "No";
                    string strUnderfilled = "No";
                    string strEmpty = "No";

                    if (newRecord.Overloaded)
                    {
                        strOverloaded = "Yes";
                    }
                    if (newRecord.Underfilled)
                    {
                        strUnderfilled = "Yes";
                    }
                    if (newRecord.LocEmpty)
                    {
                        strEmpty = "Yes";
                    }

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL ADD_FULL_RECORD(@brickId, @description, @category, @subcategory, @container, @unit, @unitrow, " +
                        "@drawer, @color, @numavailable, @numinuse, @altbrickid, @overloaded, @underfilled, @locempty)";
                    cmd.Parameters.AddWithValue("@brickId", newRecord.BrickId);                    
                    cmd.Parameters.AddWithValue("@description", newRecord.Description);
                    cmd.Parameters.AddWithValue("@category", newRecord.Category);
                    cmd.Parameters.AddWithValue("@subcategory", newRecord.Subcategory);                                        
                    cmd.Parameters.AddWithValue("@container", newRecord.Container);
                    cmd.Parameters.AddWithValue("@unit", newRecord.Unit);
                    cmd.Parameters.AddWithValue("@unitrow", newRecord.UnitRow);
                    cmd.Parameters.AddWithValue("@drawer", newRecord.Drawer);
                    cmd.Parameters.AddWithValue("@color", newRecord.Color);
                    cmd.Parameters.AddWithValue("@numavailable", newRecord.NumAvailable);
                    cmd.Parameters.AddWithValue("@numinuse", newRecord.NumInUse);
                    cmd.Parameters.AddWithValue("@altbrickid", newRecord.AltBrickId);
                    cmd.Parameters.AddWithValue("@overloaded", strOverloaded);
                    cmd.Parameters.AddWithValue("@underfilled", strUnderfilled);
                    cmd.Parameters.AddWithValue("@locempty", strEmpty);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        //Owned Brick Calls
        public async Task<List<Owned>> GetAllOwned()
        {
            List<Owned> ownedResult = new List<Owned>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "SELECT * FROM OWNED_BRICK_REPORT";                    

                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        ownedResult.Add(new Owned() {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            BrickId = rdr.GetValue(1).ToString() ?? string.Empty,
                            ColorId = Convert.ToInt32(rdr.GetValue(2)),
                            Color = rdr.GetValue(3).ToString() ?? string.Empty,
                            NumAvailable = Convert.ToInt32(rdr.GetValue(4)),
                            NumInUse = Convert.ToInt32(rdr.GetValue(5)),
                            LocationId = rdr.GetValue(6).ToString() ?? string.Empty
                        });
                    }
                    con.Close();
                }

            }
            return ownedResult;
        }
        public async Task<Owned> GetOwned(int? id)
        {
            var ownedResult = new Owned();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM OWNED_BRICK_REPORT WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();                    
                    while (rdr.Read())
                    {
                        ownedResult.Id = Convert.ToInt32(rdr.GetValue(0));
                        ownedResult.BrickId = rdr.GetValue(1).ToString() ?? string.Empty;
                        ownedResult.ColorId = Convert.ToInt32(rdr.GetValue(2));
                        ownedResult.Color = rdr.GetValue(3).ToString() ?? string.Empty;
                        ownedResult.NumAvailable = Convert.ToInt32(rdr.GetValue(4));
                        ownedResult.NumInUse = Convert.ToInt32(rdr.GetValue(5));
                        ownedResult.LocationId = rdr.GetValue(6).ToString() ?? string.Empty;                       
                    }
                    con.Close();
                }
               
            }             
            return ownedResult;            
        }

        public async Task<List<Owned>> GetOwnedBrickId(string? brickid)
        {
            var ownedResult = new List<Owned>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM OWNED_BRICK_REPORT WHERE BRICK_ID = @BrickId";
                    cmd.Parameters.AddWithValue("@BrickId", brickid);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        ownedResult.Add(new Owned()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            BrickId = rdr.GetValue(1).ToString() ?? string.Empty,
                            ColorId = Convert.ToInt32(rdr.GetValue(2)),
                            Color = rdr.GetValue(3).ToString() ?? string.Empty,
                            NumAvailable = Convert.ToInt32(rdr.GetValue(4)),
                            NumInUse = Convert.ToInt32(rdr.GetValue(5)),
                            LocationId = rdr.GetValue(6).ToString() ?? string.Empty,
                        });
                    }
                    con.Close();
                }

            }
            return ownedResult;
        }

        public async Task<int> AddOwnedBrick(Owned newOwned)
        {
            int newId = 0;
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "CALL ADD_OWNED_BRICK(@brickId, @colorId, @numavail, @numinuse, @locationId)";
                    cmd.Parameters.AddWithValue("@brickId", newOwned.BrickId);
                    cmd.Parameters.AddWithValue("@colorId", newOwned.ColorId);
                    cmd.Parameters.AddWithValue("@numavail", newOwned.NumAvailable);
                    cmd.Parameters.AddWithValue("@numinuse", newOwned.NumInUse);
                    cmd.Parameters.AddWithValue("@locationId", newOwned.LocationId);
                    
                    con.Open();                    
                    //Add new brick
                    await cmd.ExecuteNonQueryAsync();

                    //Get the new Id
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT ID FROM OWNED ORDER BY OWNED.ID DESC LIMIT 1";
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read()) { newId = Convert.ToInt32(rdr.GetValue(0)); }

                    con.Close();
                }
            }
            return newId;
        }

        public async void UpdateOwnedBrick(Owned updateOwned)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL UPDATE_OWNED_BRICK (@id, @brickId, @colorId, @numavail, @numinuse, @locationId)";
                    cmd.Parameters.AddWithValue("@id", updateOwned.Id);
                    cmd.Parameters.AddWithValue("@brickId", updateOwned.BrickId);
                    cmd.Parameters.AddWithValue("@colorId", updateOwned.ColorId);
                    cmd.Parameters.AddWithValue("@numavail", updateOwned.NumAvailable);
                    cmd.Parameters.AddWithValue("@numinuse", updateOwned.NumInUse);
                    cmd.Parameters.AddWithValue("@locationId", updateOwned.LocationId);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        public async void DeleteOwnedBrick(int id)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE FROM OWNED WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        //Color Calls
        public async Task<List<ColorList>> GetColors()
        {
            List<ColorList> colorResult = new List<ColorList>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM COLORS ORDER BY COLOR";
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        colorResult.Add(new ColorList()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            Name = rdr.GetValue(1).ToString() ?? string.Empty,
                        });
                    }
                    con.Close();
                }
            }
            return colorResult;
        }

        public string GetColor(int id)
        {
            string colorResult = string.Empty;
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT COLOR FROM COLORS WHERE ID=@Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read()) { colorResult = rdr.GetValue(0).ToString() ?? string.Empty; }
                    con.Close();
                }

            }
            return colorResult;
        }

        //This method may not be needed after adding its output to AddOwnedBrick
        //Originally, AddOwnedBrick only added the new entry and I needed a second one to return to the new Id
        //Then I just added the Id search to AddOwnedBrick making this one superfluous. 
        //Going to leave it here until the project is done just in case I need it for something else.
        //public Owned GetLastAddedBrick()
        //{
        //    var lastBrickAdded = new Owned();
        //    using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand())
        //        {
        //            cmd.Connection = con;
        //            cmd.CommandType = System.Data.CommandType.Text;
        //            cmd.Parameters.Clear();
        //            cmd.CommandText = "SELECT OWNED.ID, BRICK_ID, COLOR_ID, COLORS.COLOR, NUM_AVILABLE, NUM_IN_USE, LOCATION_ID FROM OWNED JOIN COLORS ON COLOR_ID = COLORS.ID " +
        //                "ORDER BY OWNED.ID DESC LIMIT 1";
        //            con.Open();
        //            MySqlDataReader rdr = cmd.ExecuteReader();
        //            while (rdr.Read())
        //            {
        //                lastBrickAdded.Id = Convert.ToInt32(rdr.GetValue(0));
        //                lastBrickAdded.BrickId = rdr.GetValue(1).ToString() ?? string.Empty;
        //                lastBrickAdded.ColorId = Convert.ToInt32(rdr.GetValue(2));
        //                lastBrickAdded.Color = rdr.GetValue(3).ToString() ?? string.Empty;
        //                lastBrickAdded.NumAvailable = Convert.ToInt32(rdr.GetValue(4));
        //                lastBrickAdded.NumInUse = Convert.ToInt32(rdr.GetValue(5));
        //                lastBrickAdded.LocationId = rdr.GetValue(6).ToString() ?? string.Empty;
        //            }
        //            con.Close();
        //        }
        //    }
        //    return lastBrickAdded;
        //}

    }
}
