using System;
using System.Data;
using System.Data.Common;
using PluginLoader.Plugins;

namespace AbDatabaseHelper
{
	/// <summary>
	/// Database transaction delegate.
	/// if it return true,then means Transaction is success
	/// else is fail,it may to rollback
	/// </summary>
	public delegate bool DatabaseTransactionDelegate (DbCommand cmd, DbTransaction trans);
	/// <summary>
	/// Ab connector.
	/// </summary>
	public abstract class AbDbHelper:IPlugin
	{

		protected AbDbHelper ()
		{
		}

		/// <summary>
		/// Executes the SQL
		/// </summary>
		/// <returns>the Execute Result</returns>
		/// <param name="SQL">SQL</param>
		/// <param name="args">Arguments.</param>
		public abstract DataSet ExecuteSQL (string SQL, params DbParameter[] args);

		/// <summary>
		/// Executes the SQL without result.
		/// </summary>
		/// <returns>the SQL used rows</returns>
		/// <param name="SQL">SQL</param>
		/// <param name="args">Arguments.</param>
		public abstract int ExecuteSQLWithoutResult (string SQL, params DbParameter[] args);
		#region IPlugin implementation
		public virtual bool Loading ()
		{
			return true;
		}

		public virtual bool UnLoading ()
		{
			return true;
		}
		#endregion
	}
}

