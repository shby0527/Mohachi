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
	public class MySqlDbHelperPlugin:AbDbHelper
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
		public override DataSet ExecuteSQL (string SQL, params DbParameter[] args)
		{
			throw new NotImplementedException ();
		}

		public override int ExecuteSQLNonResult (string SQL, params DbParameter[] args)
		{
			throw new NotImplementedException ();
		}

		public override DataSet ExecuteSQLWithTrans (DatabaseTransactionDelegate method, 
		                                             IsolationLevel level = (IsolationLevel)4096)
		{
			throw new NotImplementedException ();
		}

		public override int ExecuteSQLWithTransNonResult (DatabaseTransactionDelegate method, 
		                                                  IsolationLevel level = (IsolationLevel)4096)
		{
			throw new NotImplementedException ();
		}

		public override DbDataReader ExecuteSQLWithReader (string SQL, params DbParameter[] args)
		{
			throw new NotImplementedException ();
		}

		public override DbConnection Connection {
			get {
				throw new NotImplementedException ();
			}
		}
		#endregion
	}
}

