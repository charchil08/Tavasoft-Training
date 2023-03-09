using System.Data;
using System.Data.SqlClient;

namespace CommunityInvestment.SP
{
    public class Common2
    {
        SqlConnection sqlCon = null;
        String SqlconString = "Server=PCI141\\SQL2017;Initial Catalog = CommunityInvestment; Persist Security Info=False;User ID = sa; Password=tatva123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout = 30";
        public string Test()
        {
            string result="";
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("spDemo", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
 
                //sql_cmnd.Parameters.AddWithValue("@FIRST_NAME", SqlDbType.NVarChar).Value = firstName;
                //sql_cmnd.Parameters.AddWithValue("@LAST_NAME", SqlDbType.NVarChar).Value = lastName;
                //sql_cmnd.Parameters.AddWithValue("@AGE", SqlDbType.Int).Value = age;
                sql_cmnd.ExecuteNonQuery();
                sqlCon.Close();
            }
            return result;
        }

    }
}
