using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using Oracle.ManagedDataAccess.Client;

namespace TestomaticSupport
{
    public class SqlRunner
    {
        public void Execute(string tnsName, string userId, string password, string directory, string sqlFile)
        {
            var script = File.ReadAllText(Path.Combine(directory, sqlFile));
            var csb = new OracleConnectionStringBuilder {DataSource = tnsName, UserID = userId, Password = password};
            using (var conn = new OracleConnection(csb.ConnectionString))
            {
                conn.Open();
                using (var cmd = new OracleCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = Prep(script);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string Prep(string script)
        {
            var s = Regex.Replace(script, "^(REM|SET) .*$", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return "BEGIN\n" + s + "\nEND;\n";
        }
    }
}