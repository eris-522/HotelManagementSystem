using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace HotelManagementSystem
{
    // Helper class to manage MySQL connections and run helper queries in XAMPP environment.
    public static class DatabaseConnection
    {
        private static readonly string ConnectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=hotel_booking_db;" +
            "Uid=root;" +
            "Pwd=;" +
            "Convert Zero Datetime=True;" +
            "Allow Zero Datetime=True;";

        // Retrieves an open MySqlConnection object. Caller is responsible for closing/disposing it.
        public static MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConnectionString);
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Connection Error! Please make sure XAMPP (MySQL) is running.\n\nDetails: " + ex.Message,
                    "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        // Executes a SELECT query and returns the details in a DataTable.
        public static DataTable ExecuteQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                if (conn == null) return null;

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        try
                        {
                            adapter.Fill(dt);
                            return dt;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Query execution failed!\n\nDetails: " + ex.Message,
                                "SQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        }
                    }
                }
            }
        }

        // Executes an INSERT, UPDATE, or DELETE query and returns the number of rows affected.
        public static int ExecuteNonQuery(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                if (conn == null) return -1;

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        return cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Command execution failed!\n\nDetails: " + ex.Message,
                            "SQL Execute Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
        }

        // Executes a query and returns the first column of the first row.
        public static object ExecuteScalar(string query, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                if (conn == null) return null;

                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    try
                    {
                        return cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Scalar execution failed!\n\nDetails: " + ex.Message,
                            "SQL Scalar Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
        }
    }
}