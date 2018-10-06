using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;

namespace WebApi.ProcedureRegion.Repositories
{
    public class StateProcedureRepository : IStateRepository
    {
        private SqlConnection _sqlConn;
        private List<State> _states;
        private bool disposed = false;

        public StateProcedureRepository()
        {
            _sqlConn = new SqlConnection(WebApi.ProcedureRegion.Properties.Settings
                                               .Default.ConnectionStringProcedure);
            _states = new List<State>();
        }

        public void Delete(Guid id)
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandDelete = new SqlCommand("uspManagerNonQueryState", _sqlConn)
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

        public IEnumerable<State> GetAll()
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandGetAll = new SqlCommand("uspManagerQueryState", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetAll.Parameters.AddWithValue("Action", ACTION.ToString());
                var reader = sqlCommandGetAll.ExecuteReader();

                while (reader.Read())
                {
                    var _state = new State
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Flag = reader["Flag"].ToString()
                    };
                    _states.Add(_state);
                }
                _sqlConn.Close();
                return _states;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<State>();
        }

        public State GetById(Guid id)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommandGetById = new SqlCommand("uspManagerQueryState", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetById.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetById.Parameters.AddWithValue("Id", id.ToString());
                var reader = sqlCommandGetById.ExecuteReader();

                var _state = new State();

                while (reader.Read())
                {
                    _state.Id = Guid.Parse(reader["Id"].ToString());
                    _state.Name = reader["Name"].ToString();
                    _state.Flag = reader["Flag"].ToString();
                }
                _sqlConn.Close();
                return _state;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return null;
        }

        public IEnumerable<State> GetByName(State state)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommandGetByName = new SqlCommand("uspManagerQueryState", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetByName.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetByName.Parameters.AddWithValue("Name", state.Name);
                var reader = sqlCommandGetByName.ExecuteReader();

                while (reader.Read())
                {
                    var _state = new State
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Flag = reader["Flag"].ToString()

                    };
                    _states.Add(_state);
                }
                _sqlConn.Close();

                return _states;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<State>();
        }

        public void Save(State state)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommSave = new SqlCommand("uspManagerNonQueryState", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommSave.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommSave.Parameters.AddWithValue("Id", state.Id);
                sqlCommSave.Parameters.AddWithValue("Name", state.Name);
                sqlCommSave.Parameters.AddWithValue("Flag", state.Flag);

                sqlCommSave.ExecuteNonQuery();

                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }

        }

        public void Update(State state)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommUpdate = new SqlCommand("uspManagerNonQueryState", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommUpdate.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommUpdate.Parameters.AddWithValue("Id", state.Id);
                sqlCommUpdate.Parameters.AddWithValue("Name", state.Name);
                sqlCommUpdate.Parameters.AddWithValue("Flag", state.Flag);

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
        ~StateProcedureRepository()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

    }
}