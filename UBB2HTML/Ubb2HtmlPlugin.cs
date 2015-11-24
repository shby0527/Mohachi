using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AbTextProcess;
using PluginLoader.PluginAttribute;

namespace UBB2HTML
{
	[DingerInfo(Address = "null",EMail = "umi@hayama.cf",Name = "umi",Phone = "null")]
	[PluginExtraInfo(Commit = "Prority is 0",Company = "null",ReleaseTime = "2015.11",UpdateTime = "2015.11")]
	[PluginInfo("F56ED1A54003B1996960D82186DC10DA9849FED0CE0844396507EE8502C962DD",0,Author = "umi",Name = "Ubb2HtmlPlugin",Version = "1.0.0")]
	public class Ubb2HtmlPlugin : AbTextProc
	{
		private List<Regex> m_allRegex;
		public Ubb2HtmlPlugin ()
		{
			this.m_allRegex = null;
		}
		/// <summary>
		/// we should add all regex to the list
		/// from a config file
		/// </summary>
		public override bool Loading ()
		{
			this.m_allRegex = new List<Regex> ();
			string res = Path.GetFullPath ("./regex.conf");

			return base.Loading ();
		}

		public override bool UnLoading ()
		{
			this.m_allRegex.Clear ();
			return base.UnLoading ();
		}
		#region implemented abstract members of AbTextProc
		protected override string Processing ()
		{
			return "";
		}
		#endregion
	}
}

