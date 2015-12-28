using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using AbDatabaseHelper;
using PluginLoader.PluginAttribute;

namespace SQLServerHelper
{
	[DingerInfo(Address = "null",EMail = "umi@hayama.cf",Name = "umi",Phone = "null")]
	[PluginExtraInfo(Commit = "SQL Server Connector Helper Plugin , Prority is 1",Company = "null",ReleaseTime = "2015.12",UpdateTime = "2015.12")]
	[PluginInfo("BB696AF76F53EAC5003C1460D7DB2D1D9B686526D296FA4D2A001BA260B9C0B8",1,Name = "SQLServerHelper",Author = "umi",Version = "1.0.0")]
	public class SQLServerHelper:AbDbHelper
	{
		public SQLServerHelper ()
			:base()
		{
		}

		public override bool Loading ()
		{
			return base.Loading ();
		}

		public override bool UnLoading ()
		{
			return base.UnLoading ();
		}
		#region implemented abstract members of AbDbHelper
		public override DataSet ExecuteSQL (string SQL, out int count, 
		                                    CommandType type = (CommandType)1, 
		                                    params DbParameter[] args)
		{
			count = 0;
			using (SqlConnection conn = new SqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (SqlCommand cmd = conn.CreateCommand()) {
						cmd.CommandText = SQL;
						cmd.CommandType = type;
						if (args.Length != 0)
							cmd.Parameters.AddRange (args);
						using (SqlDataAdapter da = new SqlDataAdapter(cmd)) {
							DataSet ds = new DataSet ();
							count = da.Fill (ds);
							return ds;
						}
					}
				} catch (SqlException) {
					return null;
				} finally {
					if (conn.State == ConnectionState.Open) {
						conn.Close ();
					}
				}
			}
		}

		public override int ExecuteSQLNonResult (string SQL, 
		                                         CommandType type = (CommandType)1, 
		                                         params DbParameter[] args)
		{
			using (SqlConnection conn = new SqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (SqlCommand cmd = conn.CreateCommand()) {
						cmd.CommandText = SQL;
						cmd.CommandType = type;
						if (args.Length != 0)
							cmd.Parameters.AddRange (args);
						return cmd.ExecuteNonQuery ();
					}
				} catch (SqlException) {
					return 0;
				} finally {
					if (conn.State == ConnectionState.Open) {
						conn.Close ();
					}
				}
			}
		}

		public override int ExecuteSQLWithTransNonResult (DatabaseTransactionDelegate method, 
		                                                  IsolationLevel level = (IsolationLevel)4096)
		{
			using (SqlConnection conn = new SqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (SqlTransaction trans = conn.BeginTransaction(level)) {
						using (SqlCommand cmd = conn.CreateCommand()) {
							try {
								cmd.Transaction = trans;
								int count = method (cmd);
								trans.Commit ();
								return count;
							} catch (Exception) {
								trans.Rollback ();
								return 0;
							}
						}
					}
				} catch (SqlException) {
					return 0;
				} finally {
					if (conn.State == ConnectionState.Open) {
						conn.Close ();
					}
				}
			}
		}

		[Obsolete("please close and dispose it")]
		public override DbDataReader ExecuteSQLWithReader (string SQL, 
		                                                   params DbParameter[] args)
		{
			SqlConnection conn = new SqlConnection (this.ConnectionString);
			try {
				conn.Open ();
				SqlCommand cmd = conn.CreateCommand ();
				cmd.CommandText = SQL;
				cmd.CommandType = CommandType.Text;
				if (args.Length != 0)
					cmd.Parameters.AddRange (args);
				return cmd.ExecuteReader ();
			} catch (SqlException) {
				if (conn.State == ConnectionState.Open) {
					conn.Close ();
					conn.Dispose ();
				}
				return null;
			}
		}

		public override DbConnection Connection {
			get {
				return new SqlConnection (this.ConnectionString);
			}
		}
		#endregion
	}
}

