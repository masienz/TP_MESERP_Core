using System.Data.OleDb;

public static class AccessConnectionHelper
{
    private static string dbPath = @"C:\Projects\TP_MESERP_Core\Database\TP_Database.accdb";

    public static OleDbConnection GetConnection()
    {
        string connString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={dbPath};Persist Security Info=False;";
        return new OleDbConnection(connString);
    }
}
