using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebAPI.Domain.Entities;
using WebAPI.Domain.Interfaces.Repositories;

namespace WebApi.ProcedureRegion.Repositories
{
    public class CountryProcedureRepository : ICountryRepository
    {
        private SqlConnection _sqlConn;        
        private List<Country> _countries;
        private bool disposed = false;

        public CountryProcedureRepository()
        {
            _sqlConn = new SqlConnection(WebApi.ProcedureRegion.Properties.Settings
                                               .Default.ConnectionStringProcedure);
            _countries = new List<Country>();
        }

        public void Delete(Guid id)
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandDelete = new SqlCommand("uspManagerNonQueryCountry", _sqlConn)
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Country> GetAll()
        {
            try
            {
                const int ACTION = 0;

                _sqlConn.Open();

                var sqlCommandGetAll = new SqlCommand("uspManagerQueryCountry", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetAll.Parameters.AddWithValue("Action", ACTION.ToString());
                var reader = sqlCommandGetAll.ExecuteReader();

                while (reader.Read())
                {
                    var _country = new Country
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Flag = reader["Flag"].ToString()                        
                    };
                    _countries.Add(_country);
                }
                _sqlConn.Close();
                return _countries;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<Country>();
        }        

        public Country GetById(Guid id)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommandGetById = new SqlCommand("uspManagerQueryCountry", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetById.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetById.Parameters.AddWithValue("Id", id.ToString());
                var reader = sqlCommandGetById.ExecuteReader();

                var _country = new Country();

                while (reader.Read())
                {
                    _country.Id = Guid.Parse(reader["Id"].ToString());
                    _country.Name = reader["Name"].ToString();
                    _country.Flag = reader["Flag"].ToString();                    
                }
                _sqlConn.Close();
                return _country;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return null;
        }

        public IEnumerable<Country> GetByName(Country country)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommandGetByName = new SqlCommand("uspManagerQueryCountry", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommandGetByName.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommandGetByName.Parameters.AddWithValue("Name", country.Name);
                var reader = sqlCommandGetByName.ExecuteReader();

                while (reader.Read())
                {
                    var _country = new Country
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        Flag = reader["Flag"].ToString(),
                        
                    };
                    _countries.Add(_country);
                }
                _sqlConn.Close();

                return _countries;
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            return new List<Country>();            
        }
        
        public void Save(Country country)
        {
            try
            {
                const int ACTION = 1;

                _sqlConn.Open();

                var sqlCommSave = new SqlCommand("uspManagerNonQueryCountry", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommSave.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommSave.Parameters.AddWithValue("Id", country.Id);
                sqlCommSave.Parameters.AddWithValue("Name", country.Name);
                sqlCommSave.Parameters.AddWithValue("Flag", country.Flag);
                
                sqlCommSave.ExecuteNonQuery();

                _sqlConn.Close();
            }
            catch (Exception ex)
            {
                var result = ex.Message;
            }
            
        }

        public void Update(Country country)
        {
            try
            {
                const int ACTION = 2;

                _sqlConn.Open();

                var sqlCommUpdate = new SqlCommand("uspManagerNonQueryCountry", _sqlConn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommUpdate.Parameters.AddWithValue("Action", ACTION.ToString());
                sqlCommUpdate.Parameters.AddWithValue("Id", country.Id);
                sqlCommUpdate.Parameters.AddWithValue("Name", country.Name);
                sqlCommUpdate.Parameters.AddWithValue("Flag", country.Flag);
                
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
        ~CountryProcedureRepository()
        {
            // Simply call Dispose(false).
            Dispose(false);
        }

    }
}
