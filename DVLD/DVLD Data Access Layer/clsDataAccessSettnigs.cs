using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DVLD_Data_Access_Layer
{
    public static class clsDataAccessSettings
    {
        private static string ConnectionString = "Server = .; Database = DVLD; User Id = sa; Password = 0000;";
        private static SqlConnection Connection = new SqlConnection(ConnectionString);
        private static SqlCommand Command = new SqlCommand();

        private static void _SetCommandParameters(SqlCommand cmd)
        {
            Command = cmd;
            Command.Connection = Connection;
        }

        /// <summary>
        /// Used For Retrieving The First Field From The Record
        /// Returns -1 If Nothing Were Found
        /// </summary>
        private static int GetFirstFieldValue(SqlCommand cmd)
        {
            _SetCommandParameters(cmd);
            try
            {
                Connection.Open();

                int FieldValue;

                object result = Command.ExecuteScalar();
                Connection.Close();

                if (result != null && int.TryParse(result.ToString(), out FieldValue))
                {
                    return FieldValue;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int Insert(SqlCommand cmd)
        {
            _SetCommandParameters(cmd);
            try
            {
                Connection.Open();

                int insertedID;

                object result = Command.ExecuteScalar();
                Connection.Close();

                if (result != null && int.TryParse(result.ToString(), out insertedID))
                {
                    return insertedID;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        static public Dictionary<string, object> Find(SqlCommand command)
        {
            Dictionary<string, object> objData = new Dictionary<string, object>();
            _SetCommandParameters(command);

            try
            {
                Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string columnName = reader.GetName(i);
                        object columnValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                        objData.Add(columnName, columnValue);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return objData;
        }

        public static bool Update(SqlCommand cmd)
        {
            int AffectedRows = 0;
            _SetCommandParameters(cmd);
            try
            {
                Connection.Open();
                AffectedRows = Command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return (AffectedRows > 0);
        }

        public static bool Delete(SqlCommand cmd)
        {
            return Update(cmd);
        }

        /// <summary>
        /// Used For Retrieving a Table or View
        /// </summary>
        public static DataTable GetAllRecords(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            _SetCommandParameters(cmd);

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                if (reader != null)
                {
                    dt.Load(reader);
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Connection.Close();
            }
            return dt;
        }

        /// <summary>
        /// Used For Retrieving The ID of any Record
        /// Returns -1 If Nothing Were Found
        /// </summary>
        public static int GetRecordID(SqlCommand cmd)
        {
            return GetFirstFieldValue(cmd);
        }

        /// <summary>
        /// Used For Searching For Existence of any Record
        /// </summary>
        public static bool IsRecordExist(SqlCommand cmd)
        {
            _SetCommandParameters(cmd);

            bool isExist = false;

            try
            {
                Connection.Open();

                SqlDataReader reader = Command.ExecuteReader();

                isExist = reader.HasRows;

                reader.Close();
            }

            catch (Exception)
            {
                throw;
            }

            finally
            {
                Connection.Close();
            }
            return isExist;
        }

        public static int GetCount(SqlCommand cmd)
        {
            int Count = GetFirstFieldValue(cmd);
            return (Count != -1) ? Count : 0;
        }

    }
}