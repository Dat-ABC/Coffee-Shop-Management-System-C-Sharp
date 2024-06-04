using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace QLQuanCaPhe.DAO
{
    public class clsDatabaseDAO
    {
        private static clsDatabaseDAO instance;

        SqlConnection connection;
        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable dt;

        public static clsDatabaseDAO Instance
        {
            get
            {
                if (instance == null) instance = new clsDatabaseDAO(); return instance;
            }
            private set => instance = value;
        }

        private clsDatabaseDAO() 
        {
            line = File.ReadAllLines("ConnectionSQL.txt");
            conlectionStr = line.Length == 0 ? "": line[0];
        }

        string[] line;

        string conlectionStr;

        public DataTable excuteQuery(string query, object[] parameter = null)
        {
            dt = new DataTable();

            using (connection = new SqlConnection(conlectionStr))
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                connection.Close();
            }
            return dt;
        }

        public int excuteNonQuery(string query, object[] parameter = null)
        {
            int count = 0;

            using (connection = new SqlConnection(conlectionStr))
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                count = command.ExecuteNonQuery();
                connection.Close();
            }
            return count;
        }

        public object excuteScaler(string query, object[] parameter = null)
        {
            object data = 0;

            using (connection = new SqlConnection(conlectionStr))
            {
                connection.Open();
                command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();
                connection.Close();
            }
            return data;
        }

        public int excuteNonQuery2(string query, string temp, string typeName, object[] parameter = null)
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(conlectionStr))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Tạo tham số cho bảng dữ liệu tạm thời
                    SqlParameter parameter2 = command.Parameters.AddWithValue(temp, parameter[0]);
                    parameter2.SqlDbType = SqlDbType.Structured;
                    parameter2.TypeName = typeName;

                    count = command.ExecuteNonQuery();
                }
                connection.Close();
            }
            return count;
        }
    }
}
