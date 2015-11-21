using System;
using System.Text;
using System.Security.Cryptography;
using PluginLoader.PluginAttribute;
using PluginLoader.Loader;

namespace AbPasswdPlugin
{
	/// <summary>
	/// Crypt.
	/// </summary>
	[DingerInfo(Address = "null",EMail = "umi@hayama.cf",Name = "umi",Phone = "null")]
	[PluginExtraInfo(Commit = "It is Default Plugin , Prority is 0",Company = "null",ReleaseTime = "2015.11",UpdateTime = "2015.11")]
	[PluginInfo("202B40094A34793BBE521B1CFB8AA651A4242B499A8704E9053549F29048716E",0,Name = "Crypt", Version = "1.0.0",Author = "umi")]
	public class Crypt:AbsPlugin
	{
		/// <summary>
		/// The date.
		/// </summary>
		private PasswdBase date;

		/// <summary>
		/// Initializes a new instance of the <see cref="AbPasswdPlugin.Crypt"/> class.
		/// </summary>
		public Crypt ()
			:base("")
		{
			this.date = new PasswdBase ();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AbPasswdPlugin.Crypt"/> class.
		/// </summary>
		/// <param name="passwd">Passwd.</param>
		public Crypt (string passwd)
			:base(passwd)
		{
			this.date = new PasswdBase ();
		}
		#region implemented abstract members of AbsPlugin
		public override bool Loading ()
		{
			//the version field is the Plugin GUID
			this.date.VersionHash = this.GetGUID ();
			return true;
		}

		public override bool UnLoading ()
		{
			return true;
		}

		protected override PasswdBase Secret ()
		{
			if (this.date.VersionHash == "")
				throw new Exception ("the VERSION is not set");
			using (HMACSHA512 hash512 = new HMACSHA512()) {
				using (RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider()) {
					Random cntRnd = new Random ();
					CryExtInfo extra = new CryExtInfo ();
					extra.Count = cntRnd.Next (100, 200);
					using (SHA512CryptoServiceProvider sha512 = new SHA512CryptoServiceProvider()) {
						byte[] KeyBuffer = new byte[512];
						rnd.GetNonZeroBytes (KeyBuffer);
						KeyBuffer = sha512.ComputeHash (KeyBuffer);
						extra.Key = KeyBuffer;
						this.date.ExtraInfo = extra;
						byte[] pwdBuffer;
						byte[] pwdDate = Encoding.UTF8.GetBytes (this.Password);
						pwdBuffer = hash512.ComputeHash (pwdDate);
						for (int i=0; i<= extra.Count; i++) {
							pwdBuffer = hash512.ComputeHash (pwdBuffer);
						}
						this.date.Password = pwdBuffer;
					}
				}
			}
			return this.date;
		}

		protected override bool CheckSecret (PasswdBase info)
		{
			CryExtInfo infos = info.ExtraInfo as CryExtInfo;
			if (infos == null)
				return false;
			using (HMACSHA512 hash512 = new HMACSHA512(infos.Key)) {
				byte[] pwdDate = Encoding.UTF8.GetBytes (this.Password);
				byte[] pwdBuffer = hash512.ComputeHash (pwdDate);
				for (int i =0; i<infos.Count; i++) {
					pwdBuffer = hash512.ComputeHash (pwdBuffer);
				}
				return this.SlowCompose (info.Password, pwdBuffer);
			}
		}
		#endregion
	}

	/// <summary>
	/// Cry extra info.
	/// </summary>
	[Serializable]
	public sealed class CryExtInfo
	{
		/// <summary>
		/// Gets or sets the count.
		/// </summary>
		/// <value>The count.</value>
		public int Count{ get; set; }

		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		/// <value>The key.</value>
		public byte[] Key{ get; set; }
	}
}

