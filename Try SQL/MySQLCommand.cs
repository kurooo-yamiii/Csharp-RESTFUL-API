using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Try_SQL
{
    internal class MySQLCommand
    {
        private MySqlConnection mycon;

        public MySQLCommand(string connectionString)
        {
            mycon = new MySqlConnection(connectionString);
        }

        public void NonQuery(string query)
        {
            MySqlCommand sqlCommand = new MySqlCommand(query, mycon);
            try
            {
                mycon.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mycon.Close();
            }
        }

        public DataTable MyPopulate(string query)
        {
            DataTable dataTable = new DataTable();
            MySqlCommand sqlCommand = new MySqlCommand(query, mycon);
            try
            {
                mycon.Open();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dataTable);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            return dataTable;
        }

        public string SelectSpecific(string query)
        {
            string result = "";
            MySqlCommand sqlCommand = new MySqlCommand(query, mycon);
            try
            {
                mycon.Open();
                object queryResult = sqlCommand.ExecuteScalar();
                if (queryResult != null)
                {
                    result = queryResult.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                mycon.Close();
            }
            return result;
        }
    }
}
