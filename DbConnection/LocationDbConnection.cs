using LegoCollection.Entities;
using MySqlConnector;

namespace LegoCollection.DbConnection
{
    public class LocationDbConnection
    {
        private IConfiguration Configuration;

        private string? _connectionString;
        public LocationDbConnection(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public string GetConnectionString()
        {
            return this.Configuration.GetConnectionString("DefaultConnection");
        }

        //Location Calls
        //GET all Locations
        public async Task<List<LocationEntity>> GetLocations()
        {
            List<LocationEntity> locationResult = new List<LocationEntity>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM LOCATION_REPORT";
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        locationResult.Add(new LocationEntity()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            LocationId = rdr.GetValue(1).ToString() ?? string.Empty,
                            Container = rdr.GetValue(2).ToString() ?? string.Empty,
                            Unit = rdr.GetValue(3).ToString() ?? string.Empty,
                            UnitRow = rdr.GetValue(4).ToString() ?? string.Empty,
                            Drawer = rdr.GetValue(5).ToString() ?? string.Empty,
                            Overloaded = rdr.GetValue(6).ToString() == "Yes" ? true : false,
                            Underfilled = rdr.GetValue(7).ToString() == "Yes" ? true : false,
                            LocationEmpty = rdr.GetValue(8).ToString() == "Yes" ? true : false
                        });
                    }
                    con.Close();
                }
            }
            return locationResult;
        }

        //GET location by id
        public async Task<LocationEntity> GetLocationById(int? id)
        {
            var locationResult = new LocationEntity();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM LOCATION_REPORT WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        locationResult.Id = Convert.ToInt32(rdr.GetValue(0));
                        locationResult.LocationId = rdr.GetValue(1).ToString() ?? string.Empty;
                        locationResult.Container = rdr.GetValue(2).ToString() ?? string.Empty;
                        locationResult.Unit = rdr.GetValue(3).ToString() ?? string.Empty;
                        locationResult.UnitRow = rdr.GetValue(4).ToString() ?? string.Empty;
                        locationResult.Drawer = rdr.GetValue(5).ToString() ?? string.Empty;
                        locationResult.Overloaded = rdr.GetValue(6).ToString() == "Yes" ? true : false;
                        locationResult.Underfilled = rdr.GetValue(7).ToString() == "Yes" ? true : false;
                        locationResult.LocationEmpty = rdr.GetValue(8).ToString() == "Yes" ? true : false;
                    }
                    con.Close();
                }

            }
            return locationResult;
        }

        //GET location by location id
        public async Task<List<LocationEntity>> GetLocationByLocationId(string? id)
        {
            List<LocationEntity> locationResult = new List<LocationEntity>();
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT * FROM LOCATION_REPORT WHERE LOCATION_ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read())
                    {
                        locationResult.Add(new LocationEntity()
                        {
                            Id = Convert.ToInt32(rdr.GetValue(0)),
                            LocationId = rdr.GetValue(1).ToString() ?? string.Empty,
                            Container = rdr.GetValue(2).ToString() ?? string.Empty,
                            Unit = rdr.GetValue(3).ToString() ?? string.Empty,
                            UnitRow = rdr.GetValue(4).ToString() ?? string.Empty,
                            Drawer = rdr.GetValue(5).ToString() ?? string.Empty,
                            Overloaded = rdr.GetValue(6).ToString() == "Yes" ? true : false,
                            Underfilled = rdr.GetValue(7).ToString() == "Yes" ? true : false,
                            LocationEmpty = rdr.GetValue(8).ToString() == "Yes" ? true : false
                        });
                    }
                    con.Close();
                }

            }
            return locationResult;
        }

        //POST new location, probably won't use
        public async Task<int> AddLocation(LocationEntity newLocation)
        {
            int newId = 0;
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    string strOverloaded = "No";
                    string strUnderfilled = "No";
                    string strEmpty = "No";

                    if (newLocation.Overloaded)
                    {
                        strOverloaded = "Yes";
                    }
                    if (newLocation.Underfilled)
                    {
                        strUnderfilled = "Yes";
                    }
                    if (newLocation.LocationEmpty)
                    {
                        strEmpty = "Yes";
                    }

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL ADD_NEW_LOCATION (@locationid, @container, @unit, @unitrow, @drawer, @overloaded, @underfilled, @locempty)";
                    cmd.Parameters.AddWithValue("@locationid", newLocation.LocationId);
                    cmd.Parameters.AddWithValue("@container", newLocation.Container);
                    cmd.Parameters.AddWithValue("@unit", newLocation.Unit);
                    cmd.Parameters.AddWithValue("@unitrow", newLocation.UnitRow);
                    cmd.Parameters.AddWithValue("@drawer", newLocation.Drawer);
                    cmd.Parameters.AddWithValue("@overloaded", strOverloaded);
                    cmd.Parameters.AddWithValue("@underfilled", strUnderfilled);
                    cmd.Parameters.AddWithValue("@locempty", strEmpty);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();

                    //Get the new Id
                    cmd.Parameters.Clear();
                    cmd.CommandText = "SELECT ID FROM LOCATION ORDER BY ID DESC LIMIT 1";
                    MySqlDataReader rdr = await cmd.ExecuteReaderAsync();
                    while (rdr.Read()) { newId = Convert.ToInt32(rdr.GetValue(0)); }

                    con.Close();
                }
            }
            return newId;
        }

        //PUT updated location, probably used a lot
        public async void UpdateLocation(LocationEntity updateLocation)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {

                    string strOverloaded = "No";
                    string strUnderfilled = "No";
                    string strEmpty = "No";

                    if (updateLocation.Overloaded)
                    {
                        strOverloaded = "Yes";
                    }
                    if (updateLocation.Underfilled)
                    {
                        strUnderfilled = "Yes";
                    }
                    if (updateLocation.LocationEmpty)
                    {
                        strEmpty = "Yes";
                    }

                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "CALL UPDATE_LOCATION (@id, @locationid, @container, @unit, @unitrow, @drawer, @overloaded, @underfilled, @locempty)";
                    cmd.Parameters.AddWithValue("@id", updateLocation.Id);
                    cmd.Parameters.AddWithValue("@locationid", updateLocation.LocationId);
                    cmd.Parameters.AddWithValue("@container", updateLocation.Container);
                    cmd.Parameters.AddWithValue("@unit", updateLocation.Unit);
                    cmd.Parameters.AddWithValue("@unitrow", updateLocation.UnitRow);
                    cmd.Parameters.AddWithValue("@drawer", updateLocation.Drawer);
                    cmd.Parameters.AddWithValue("@overloaded", strOverloaded);
                    cmd.Parameters.AddWithValue("@underfilled", strUnderfilled);
                    cmd.Parameters.AddWithValue("@locempty", strEmpty);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }

        public async void DeleteLocation(int id)
        {
            using (MySqlConnection con = new MySqlConnection(GetConnectionString()))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.Clear();

                    cmd.CommandText = "DELETE FROM LOCATION WHERE ID = @Id";
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    await cmd.ExecuteNonQueryAsync();
                    con.Close();
                }
            }
        }
    }
}
