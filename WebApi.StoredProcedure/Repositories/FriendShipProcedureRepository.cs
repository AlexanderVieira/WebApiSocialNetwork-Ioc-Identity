using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebAPI.BLL.Entities;
using WebAPI.BLL.Interfaces.Repositories;

namespace WebApi.StoredProcedure.Repositories
{
	public class FriendShipProcedureRepository : IFriendShipRepository
    {
        private SqlConnection _sqlConn;
        private List<FriendShip> _friendShips;
        private bool disposed = false;

        public FriendShipProcedureRepository()
        {
            _sqlConn = new SqlConnection(WebApi.StoredProcedure.Properties.Settings
                                               .Default.ConnectionStringStoredProcedure);
            _friendShips = new List<FriendShip>();
        }

        public void Delete(Guid id)
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandDelete = new SqlCommand("uspManagerNonQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandDelete.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandDelete.Parameters.AddWithValue("Id", id.ToString());
                sqlCommandDelete.ExecuteNonQuery();

                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }

        }

        public void Dispose()
        {
            //throw new NotImplementedException();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<FriendShip> GetAll()
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandGetAll = new SqlCommand("uspManagerQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetAll.Parameters.AddWithValue("Action", ACTION.ToString());
                var reader = sqlCommandGetAll.ExecuteReader();

                while (reader.Read())
                {
                    var _friendShip = new FriendShip
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        RequestedById = Guid.Parse(reader["RequestedById"].ToString()),
                        RequestedToId = Guid.Parse(reader["RequestedToId"].ToString()),
                        RequestTime = DateTime.Parse(reader["RequestTime"].ToString()),
                        Status = (StatusEnum) int.Parse(reader["Status"].ToString())
						//RequestedBy = (Profile)reader["RequestedBy"],
						//RequestedTo = (Profile)reader["RequestedTo"]
				};
                    _friendShips.Add(_friendShip);
                }

				_sqlConn.Close();

				return _friendShips;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<FriendShip>();
        }

        public FriendShip GetById(Guid id)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommandGetById = new SqlCommand("uspManagerQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetById.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetById.Parameters.AddWithValue("Id", id.ToString());
                var reader = sqlCommandGetById.ExecuteReader();

                var _friendShip = new FriendShip();

                while (reader.Read())
                {                    
                    _friendShip.Id = Guid.Parse(reader["Id"].ToString());
                    _friendShip.RequestedById = Guid.Parse(reader["RequestedById"].ToString());
                    _friendShip.RequestedToId = Guid.Parse(reader["RequestedToId"].ToString());
                    _friendShip.RequestTime = DateTime.Parse(reader["RequestTime"].ToString());
					_friendShip.Status = (StatusEnum)int.Parse(reader["Status"].ToString());
					//_friendShip.RequestedBy = (Profile)reader["RequestedBy"];
					//_friendShip.RequestedTo = (Profile)reader["RequestedTo"];
				}

				_sqlConn.Close();

				return _friendShip;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return null;
        }

        public IEnumerable<FriendShip> GetFriendsOf(Guid id)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommandGetFriendsOf = new SqlCommand("uspManagerQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetFriendsOf.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetFriendsOf.Parameters.AddWithValue("Id", id.ToString());
                var reader = sqlCommandGetFriendsOf.ExecuteReader();

                while (reader.Read())
                {
                    var _friendShip = new FriendShip
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        RequestedById = Guid.Parse(reader["RequestedById"].ToString()),
                        RequestedToId = Guid.Parse(reader["RequestedToId"].ToString()),
                        RequestTime = DateTime.Parse(reader["RequestTime"].ToString()),
						Status = (StatusEnum)int.Parse(reader["Status"].ToString())
						//RequestedBy = (Profile)reader["RequestedBy"],
						//RequestedTo = (Profile)reader["RequestedTo"]

				};

					_friendShips.Add(_friendShip);
                }
                _sqlConn.Close();

                return _friendShips;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<FriendShip>();
        }

        public void Save(FriendShip friendShip)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommSave = new SqlCommand("uspManagerNonQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommSave.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommSave.Parameters.AddWithValue("Id", friendShip.Id.ToString());
                sqlCommSave.Parameters.AddWithValue("RequestedById", friendShip.RequestedById.ToString());
                sqlCommSave.Parameters.AddWithValue("RequestedToId", friendShip.RequestedToId.ToString());
                sqlCommSave.Parameters.AddWithValue("RequestTime", friendShip.RequestTime.ToString());
                sqlCommSave.Parameters.AddWithValue("Status", friendShip.Status);
				//sqlCommSave.Parameters.AddWithValue("Profile_Id", friendShip.RequestedBy);
				//sqlCommSave.Parameters.AddWithValue("Profile_Id1", friendShip.RequestedTo);


				sqlCommSave.ExecuteNonQuery();

                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }

        }

        public void Update(FriendShip friendShip)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommUpdate = new SqlCommand("uspManagerNonQueryFriendShip", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommUpdate.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommUpdate.Parameters.AddWithValue("Id", friendShip.Id);
                sqlCommUpdate.Parameters.AddWithValue("RequestedById", friendShip.RequestedById);
                sqlCommUpdate.Parameters.AddWithValue("RequestedToId", friendShip.RequestedToId);
                sqlCommUpdate.Parameters.AddWithValue("RequestTime", friendShip.RequestTime);
				sqlCommUpdate.Parameters.AddWithValue("Status", friendShip.Status);
				//sqlCommUpdate.Parameters.AddWithValue("Profile_Id", friendShip.RequestedBy);
				//sqlCommUpdate.Parameters.AddWithValue("Profile_Id1", friendShip.RequestedTo);

				sqlCommUpdate.ExecuteNonQuery();

                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                disposed = true;
            }
        }
        ~FriendShipProcedureRepository()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

    }
}
