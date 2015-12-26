using System;
using UBB2HTML;

namespace UBB2HTMLTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AbTextProcess.AbTextProc u = new Ubb2HtmlPlugin ();
			u.Loading ();
			u.InputText = 
				"[url=http://abc.bcd.cdf/u]abc123!@#[/url]"+
				"[url=https://abc.bcd.cdf/u]abc123!@#[/url]\n"+
					"[url]http://abc.bcd.cdf/u[/url]\n"+
					"[url]https://abc.bcd.cdf/u[/url]\n"+
					"[url=https://abc.bcd.cdf/u][img]https://aaa.bbb.ccc[/img][/url]";
			Console.WriteLine (u.InputText);
			Console.WriteLine (u.ProcessedText);
			u.UnLoading ();
		}
	}
}
