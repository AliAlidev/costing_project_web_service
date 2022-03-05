using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace oti_cost
{

    public class response {
        public bool success { set; get; }
        public string code { set; get; }
        public string data { set; get; }
    }

    class DBVariables
    {
        static string connectionstring = "server=127.0.0.1;uid=user1;pwd=123qweASD;database=oti_cost";
        static MySqlConnection conn = new MySqlConnection(connectionstring);
        static MySqlCommand comm = new MySqlCommand();
        MySqlDataReader myReader;
        static public MySqlDataAdapter da;



        static void openConn()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }

        static void closeConn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
                conn.Close();
        }

        public static response executenq(string query)
        {
            try
            {
                comm.Connection = conn;
                comm.CommandText = query;
                openConn();
                comm.ExecuteNonQuery();
                closeConn();
                response respo = new response { success = true, code = null, data = null };
                return respo;

            }
            catch (System.Exception ex)
            {
                string code = errorFunc(ex.Message, ex.Source);
                response respo = new response { success = true, code = code, data = ex.Message };
                return respo;
            }
        }


        public static string executescaler(string query)
        {
            try
            {
                comm.Connection = conn;
                comm.CommandText = query;
                openConn();
                string res = comm.ExecuteScalar().ToString();
                closeConn();
                return res;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }


        public static MySqlDataReader ExecuteReader(string query)
        {
            try
            {
                comm.Connection = conn;
                comm.CommandText = query;
                openConn();

                MySqlDataReader res = comm.ExecuteReader();
                //closeConn();
                return res;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }


        public static bool isFound(string value, string column, string table)
        {
            try
            {
                string query = "select count(*) from " + table + " where " + column + " = '" + value + "'";
                comm.Connection = conn;
                comm.CommandText = query;
                openConn();
                var resutl = comm.ExecuteScalar();
                closeConn();
                if (int.Parse(resutl.ToString()) > 0)
                    return true;
                else
                    return false;
            }
            catch (System.Exception ex)
            {
                return false;
            }
        }


        public static DataTable showactivecenter()
        {
            string query = "select id , active_center_name , team_name from active_center";
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            da.Fill(dt);

            // id column
            DataTable datatable0 = new DataTable();
            DataRow myDataRow;
            DataColumn dtColumn;

            //dtColumn = new DataColumn();
            //dtColumn.DataType = typeof(string);
            //dtColumn.ColumnName = "معرف";
            //dtColumn.AutoIncrement = true;
            //dtColumn.ReadOnly = true;
            //dtColumn.Unique = false;
            //datatable0.Columns.Add(dtColumn);


            // team name column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "اسم الفريق";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;
            datatable0.Columns.Add(dtColumn);


            // active centers column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "اسم مركز النشاط";

            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;
            datatable0.Columns.Add(dtColumn);

            // workers column
            dtColumn = new DataColumn();
            dtColumn.DataType = typeof(string);
            dtColumn.ColumnName = "اسم العمال";
            dtColumn.AutoIncrement = false;
            dtColumn.ReadOnly = true;
            dtColumn.Unique = false;
            datatable0.Columns.Add(dtColumn);

            foreach (DataRow dr in dt.Rows)
            {
                myDataRow = datatable0.NewRow();
                int id = (int)dr.ItemArray[0];
                string quy = "select worker_name from workers_names where active_center_id=" + id;
                DataSet workersds = new DataSet();
                da = new MySqlDataAdapter(quy, conn);
                da.Fill(workersds);
                string workersnames = "";
                foreach (DataRow item in workersds.Tables[0].Rows)
                {
                    workersnames += (item.ItemArray[0].ToString() + ", ");
                }
                myDataRow["اسم العمال"] = workersnames;
                myDataRow["اسم مركز النشاط"] = dr.ItemArray[1].ToString();
                myDataRow["اسم الفريق"] = dr.ItemArray[2].ToString();

                datatable0.Rows.Add(myDataRow);
            }

            return datatable0;
        }

        public static DataSet fillDataTable(string query)
        {
            DataSet ds = new DataSet();
            da = new MySqlDataAdapter(query, conn);
            da.Fill(ds);
            return ds;
        }

        public static string errorFunc(string error, string src)
        {
            string code = RandomString(10);
            executenq("insert int errorFunc(error,src,code) values('"+ error +"','"+ src +"','"+code+"')");
            return code;
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
