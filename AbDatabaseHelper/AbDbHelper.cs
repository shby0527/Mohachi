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
	/// Database Helper's abstract
	/// for the plugin to load
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
		public abstract int ExecuteSQLNonResult (string SQL, params DbParameter[] args);

		/// <summary>
		/// Executes the SQL with transaction.
		/// </summary>
		/// <returns>sql execute resulte</returns>
		/// <param name="method">Method.</param>
		/// <param name="level">Level.</param>
		public abstract DataSet ExecuteSQLWithTrans (DatabaseTransactionDelegate method, 
		                                             IsolationLevel level = IsolationLevel.ReadCommitted);

		/// <summary>
		/// Executes the SQL with trans non result.
		/// </summary>
		/// <returns>count of used rows</returns>
		/// <param name="method">Method.</param>
		/// <param name="level">Level.</param>
		public abstract int ExecuteSQLWithTransNonResult (DatabaseTransactionDelegate method, 
		                                                  IsolationLevel level = IsolationLevel.ReadCommitted);

		/// <summary>
		/// Gets the connection.
		/// </summary>
		/// <value>The connection.</value>
		public abstract DbConnection Connection{ get; }

		/// <summary>
		/// Executes the SQL with reader.
		/// </summary>
		/// <returns>the data reader</returns>
		/// <param name="SQL">SQL</param>
		/// <param name="args">Arguments.</param>
		public abstract DbDataReader ExecuteSQLWithReader (string SQL, params DbParameter[] args);
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

