using System;
using System.Data;
using System.Data.Odbc;

namespace NoxShared
{
	public class PlayerDb
	{
		public static PlayerDb FromOdbc(string connStr) {return new PlayerDb(connStr);}

		protected PlayerDb(string connStr)
		{
			try
			{
				//DataSet data = new DataSet();
				DataTable data = new DataTable();
				OdbcConnection conn = new OdbcConnection(connStr);
				//OdbcDataAdapter ad = new OdbcDataAdapter("SELECT * FROM players WHERE id=?id AND name=?name", conn);
				//ad.SelectCommand.Parameters.Add("id", OdbcType.Int);
				//ad.SelectCommand.Parameters.Add("name", OdbcType.VarChar);
				OdbcDataAdapter ad = new OdbcDataAdapter("SELECT * FROM players", conn);
				ad.Fill(data);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}
	}
}
