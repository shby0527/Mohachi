using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PluginLoader.Plugins;

namespace AbPasswdPlugin
{
	/// <summary>
	/// Abs plugin for Passwd Secret.
	/// </summary>
	public abstract class AbsPlugin : IPlugin
	{
		protected AbsPlugin (string password)
		{
			this.Password = password;
		}
		/// <summary>
		/// Gets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password{ get; private set; }

		#region IPlugin implementation
		/// <summary>
		/// Loading this instance.
		/// </summary>
		public abstract bool Loading ();
		/// <summary>
		/// Uns the loading.
		/// </summary>
		/// <returns><c>true</c>, if loading was uned, <c>false</c> otherwise.</returns>
		public abstract bool UnLoading ();

		#endregion
		/// <summary>
		/// Gets the serect.
		/// </summary>
		/// <returns>The serect.</returns>
		public abstract PasswdBase Secret ();
		/// <summary>
		/// Gets the get binary.
		/// </summary>
		/// <value>The get binary.</value>
		public byte[] GetBinary{
			get{
				BinaryFormatter bf = new BinaryFormatter ();
				using (MemoryStream ms = new MemoryStream ()) {
					bf.Serialize (ms, this.Secret ());
					byte[] ret = ms.ToArray ();
					ms.Close ();
					return ret;
				}
			}
		}
		/// <summary>
		/// Checks the secret.
		/// </summary>
		/// <returns><c>true</c>, if secret was checked, <c>false</c> otherwise.</returns>
		/// <param name="info">Info.</param>
		protected abstract bool CheckSecret (PasswdBase info);
		/// <summary>
		/// Check the specified Date.
		/// </summary>
		/// <param name="Date">Date.</param>
		public bool Check(byte[] Date)
		{
			using (MemoryStream ms = new MemoryStream()) {
				BinaryFormatter bf = new BinaryFormatter ();
				ms.Write (Date, 0, Date.Length);
				PasswdBase tmp = bf.Deserialize (ms) as PasswdBase;
				if (tmp == null)
					return false;
				return this.CheckSecret (tmp);
			}
		}

	}
}

