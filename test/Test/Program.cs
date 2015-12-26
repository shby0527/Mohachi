using System;
using System.IO;
using AbPasswdPlugin;
namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AbsPassword plug = new PBKDF2 ();
			plug.Loading ();
			plug.Password = Console.ReadLine ();
			byte[] testo = plug.GetBinary;
			Console.WriteLine("加密计算后的密文:\n" + BitConverter.ToString(testo).Replace("-",""));
			Console.WriteLine ("加密算法版本Hash：\n" + AbsPassword.GetCryptoVersion (testo));
			Console.WriteLine ("最后验证测试："+plug.Check (testo));
		}
	}
}
