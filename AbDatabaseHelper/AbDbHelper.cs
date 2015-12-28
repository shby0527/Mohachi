using System;
using System.Data;
using System.Data.Common;
using PluginLoader.Plugins;
using PluginLoader.Configure;

namespace AbDatabaseHelper
{
	/// <summary>
	/// Database transaction delegate.
	/// if it return true,then means Transaction is success
	/// else is fail,it may to rollback
	/// </summary>
	public delegate int DatabaseTransactionDelegate (DbCommand cmd);
	/// <summary>
	/// Database Helper's abstract
	/// for the plugin to load
	/// </summary>
	public abstract class AbDbHelper:IPlugin
	{
		/// <summary>
		/// Gets or sets the database address.
		/// </summary>
		/// <value>The database address.</value>
		protected string DatabaseAddr{ get; set; }

		/// <summary>
		/// Gets or sets the database ports.
		/// </summary>
		/// <value>The database ports.</value>
		protected string DatabasePorts{ get; set; }

		/// <summary>
		/// Gets or sets the database user.
		/// </summary>
		/// <value>The database user.</value>
		protected string DatabaseUser{ get; set; }

		/// <summary>
		/// Gets or sets the database password.
		/// </summary>
		/// <value>The database password.</value>
		protected string DatabasePassword{ get; set; }

		/// <summary>
		/// Gets or sets the default databse.
		/// </summary>
		/// <value>The default databse.</value>
		protected string DefaultDatabse{ get; set; }

		/// <summary>(a,b)
		/// Gets the connection string.
		/// </summary>
		/// <value>The connection string.</value>
		protected virtual string ConnectionString {
			get {
				return string.Format ("server={0},{1};uid={2};pwd={3};database={4}",
				                      this.DatabaseAddr,
				                      this.DatabasePorts,
				                      this.DatabaseUser,
				                      this.DatabasePassword,
				                      this.DefaultDatabse);
			}
		}

		protected AbDbHelper ()
		{
		}

		/// <summary>
		/// Executes the SQL.
		/// </summary>
		/// <returns>The SQL</returns>
		/// <param name="SQL">SQL</param>
		/// <param name="count">count of SQL</param> 
		/// <param name="type">Type.</param>
		/// <param name="args">Arguments.</param>
		public abstract DataSet ExecuteSQL (string SQL, 
		                                    out int count,
		                                    CommandType type = CommandType.Text, 
		                                    params DbParameter[] args);

		/// <summary>
		/// Executes the SQL non result.
		/// </summary>
		/// <returns>The SQL non result.</returns>
		/// <param name="SQL">SQ.</param>
		/// <param name="type">Type.</param>
		/// <param name="args">Arguments.</param>
		public abstract int ExecuteSQLNonResult (string SQL, 
		                                         CommandType type = CommandType.Text, 
		                                         params DbParameter[] args);

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
		[Obsolete("if you want to use this method ,pleasy close database connection and dispose it",false)]
		public abstract DbDataReader ExecuteSQLWithReader (string SQL, params DbParameter[] args);
		#region IPlugin implementation
		public virtual bool Loading ()
		{
			ConfigureManager cfg = new ConfigureManager (this);
			bool isChange = false;
			if (cfg.IsConfigKeyExists ("Address")) {
				this.DatabaseAddr = cfg ["Address"];
			} else {
				cfg ["Address"] = "127.0.0.1";
				isChange = true;
			}
			if (cfg.IsConfigKeyExists ("Ports")) {
				this.DatabasePorts = cfg ["Ports"];
			} else {
				cfg ["Ports"] = "3306";
				isChange = true;
			}
			if (cfg.IsConfigKeyExists ("User")) {
				this.DatabaseUser = cfg ["User"];
			} else {
				cfg ["User"] = "root";
				isChange = true;
			}
			if (cfg.IsConfigKeyExists ("Password")) {
				this.DatabasePassword = cfg ["Password"];
			} else {
				cfg ["Password"] = "root";
				isChange = true;
			}
			if (cfg.IsConfigKeyExists ("Database")) {
				this.DefaultDatabse = cfg ["Database"];
			} else {
				cfg ["Database"] = "Default";
				isChange = true;
			}
			if (isChange) {
				cfg.SaveAllConfig ();
			}
			return true;
		}

		public virtual bool UnLoading ()
		{
			return true;
		}
		#endregion
	}
}

