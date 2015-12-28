using System;
using System.Data;
using System.Data.Common;
using MySql.Data;
using MySql.Data.MySqlClient;
using AbDatabaseHelper;
using PluginLoader.PluginAttribute;
using PluginLoader.Configure;

namespace MySqlDatabaseHelper
{
	[DingerInfo(Address = "null",EMail = "umi@hayama.cf",Name = "umi",Phone = "null")]
	[PluginExtraInfo(Commit = "It is Default Plugin , Prority is 0",Company = "null",ReleaseTime = "2015.12",UpdateTime = "2015.12")]
	[PluginInfo("738AD36A473A6D9028DE6EFDA0EAD8FFB8115F8F37D5B20EBA12CAC2AE4D591B",0,Name = "MySqlDbHelper",Author = "umi",Version = "1.0.0")]
	public sealed class MySqlDbHelperPlugin:AbDbHelper
	{
		public MySqlDbHelperPlugin ()
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
			using (MySqlConnection conn = new MySqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (MySqlCommand cmd = conn.CreateCommand()) {
						cmd.CommandText = SQL;
						cmd.CommandType = type;
						if (args.Length != 0)
							cmd.Parameters.AddRange (args);
						using (MySqlDataAdapter da = new MySqlDataAdapter(cmd)) {
							DataSet ds = new DataSet ();
							count = da.Fill (ds);
							return ds;
						}
					}
				} catch (MySqlException) {
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
			using (MySqlConnection conn = new MySqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (MySqlCommand cmd = conn.CreateCommand()) {
						cmd.CommandText = SQL;
						cmd.CommandType = type;
						if (args.Length != 0)
							cmd.Parameters.AddRange (args);
						return cmd.ExecuteNonQuery ();
					}
				} catch (MySqlException) {
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
			using (MySqlConnection conn = new MySqlConnection(this.ConnectionString)) {
				try {
					conn.Open ();
					using (MySqlTransaction tran = conn.BeginTransaction(level)) {
						using (MySqlCommand cmd = conn.CreateCommand()) {
							try {
								cmd.Transaction = tran;
								int count = method (cmd);
								tran.Commit ();
								return count;
							} catch (Exception) {
								tran.Rollback ();
								return 0;
							}
						}
					}
				} catch (MySqlException) {
					return 0;
				} finally {
					if (conn.State == ConnectionState.Open) {
						conn.Close ();
					}
				}
			}
		}

		[Obsolete("if you want to use this method ,pleasy close database connection and dispose it",false)]
		public override DbDataReader ExecuteSQLWithReader (string SQL, params DbParameter[] args)
		{
			MySqlConnection conn = new MySqlConnection (this.ConnectionString);
			try {
				conn.Open ();
				MySqlCommand cmd = conn.CreateCommand ();
				cmd.CommandText = SQL;
				cmd.CommandType = CommandType.Text;
				if (args.Length != 0)
					cmd.Parameters.AddRange (args);
				return cmd.ExecuteReader ();
			} catch (MySqlException) {
				if (conn.State == ConnectionState.Open) {
					conn.Close ();
					conn.Dispose ();
				}
				return null;
			}
		}

		public override DbConnection Connection {
			get {
				return new MySqlConnection (this.ConnectionString);
			}
		}
		#endregion
	}
}

