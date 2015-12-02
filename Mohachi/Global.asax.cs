using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.SessionState;
using System.Web.Configuration;
using PluginLoader.Loader;
using AbPasswdPlugin;

namespace Mohachi
{

	public class Global : System.Web.HttpApplication
	{
		
		protected void Application_Start (Object sender, EventArgs e)
		{
			/*Configuration cfg = WebConfigurationManager.OpenWebConfiguration ("~");
			AppSettingsSection settings = (AppSettingsSection)cfg.GetSection ("appSettings");
			string plugin = settings.Settings ["crypto"].Value;
			string mpath = this.Server.MapPath ("~/bin");
			IPluginArray<AbsPlugin> arr = PluginLoader<AbsPlugin>.Load (mpath + "/" + plugin);
			this.Application.Lock ();
			this.Application.Add ("crypto", arr);
			this.Application.UnLock ();*/
		}

		protected void Session_Start (Object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_EndRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_AuthenticateRequest (Object sender, EventArgs e)
		{
		}

		protected void Application_Error (Object sender, EventArgs e)
		{
		}

		protected void Session_End (Object sender, EventArgs e)
		{
		}

		protected void Application_End (Object sender, EventArgs e)
		{
		}
	}
}

