using System.Data;
using System.Data.SQLite;

namespace ENoticeBoard.Models
{
    public class SqlLite
    {
        private SQLiteConnection myConnection;

        public SqlLite()
        {
            //myConnection = new SQLiteConnection(
            //    "Data Source=//vapp01/c$/Program Files (x86)/Spiceworks/db/spiceworks_prod.db;Version=3");
            myConnection = new SQLiteConnection(
                "Data Source=C://Users/tle/Desktop/SQLite/spiceworks_prod.db;Version=3");
        }

        public DataTable ConnectSqLite()
        {
            

            SQLiteCommand cmd= new SQLiteCommand();
            myConnection.Open();
            cmd.Connection = myConnection;
            cmd.CommandText =
                "select t.id, summary, status, t.created_at, t.updated_at, category, assigned_to, first_name, last_name   " +
                "from tickets t " +
                "left outer join users u on u.id = assigned_to " +
                "where status='open'";

            SQLiteDataAdapter da=new SQLiteDataAdapter(cmd);
            
             //Create DT
                DataTable dt = new DataTable();
                da.Fill(dt);
                
                myConnection.Close();
            return dt;
            

        }
    }
}