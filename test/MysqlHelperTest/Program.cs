using System;
using System.Data;
using AbDatabaseHelper;
using MySqlDatabaseHelper;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MysqlHelperTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			AbDbHelper help = new MySqlDbHelperPlugin ();
			help.Loading ();
			help.UnLoading ();
		}
	}
}
