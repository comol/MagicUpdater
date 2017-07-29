using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.SettingsTools
{
	public class JsonCommonSettings
	{
		public string ConnectionString => $"Data Source={ServerTask};Initial Catalog={BaseTask};User ID={UserTask};Password={PasswordTask}";
		public string ServerTask { get; set; }
		public string BaseTask { get; set; }
		public string UserTask { get; set; }
		public string PasswordTask { get; set; }
	}

	public class JsonAgentSettings
	{
		public string ConnectionString => $"Data Source={ServerTask};Initial Catalog={BaseTask};User ID={UserTask};Password={PasswordTask}";
		public string ServerTask { get; set; }
		public string BaseTask { get; set; }
		public string UserTask { get; set; }
		public string PasswordTask { get; set; }
	}

	public class JsonLocalSettings : JsonAgentSettings
	{
		public string Server1C { get; set; }
		public string Base1C { get; set; }
		public string User1C { get; set; }
		public string Password1C { get; set; }
		public string Platform { get; set; }
		public string AddressAst { get; set; }
		public string UserAst { get; set; }
		public string PasswordAst { get; set; }
	}
}
