using MagicUpdaterCommon.SettingsTools;
using System.Data.Entity.Core.EntityClient;

namespace MagicUpdater.DL
{
	public partial class MagicUpdaterEntities
	{
		private static class JsonConnection
		{
			static JsonConnection()
			{
				MainSettings.LoadMonitorCommonGlobalSettings();
				EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder();
				entityBuilder.Metadata = "res://*/TaskDBModel.csdl|res://*/TaskDBModel.ssdl|res://*/TaskDBModel.msl";
				entityBuilder.Provider = "System.Data.SqlClient";
				string server = MainSettings.JsonSettings.ServerTask;
				string dataBase = MainSettings.JsonSettings.BaseTask;
				string user = MainSettings.JsonSettings.UserTask;
				string password = MainSettings.JsonSettings.PasswordTask;
				entityBuilder.ProviderConnectionString = $"data source={server};initial catalog={dataBase};persist security info=True;user id={user};password={password};MultipleActiveResultSets=True;App=EntityFramework";
				EntityConnectionString = entityBuilder.ToString();
			}

			public static string EntityConnectionString { get; private set; }
		}

		public MagicUpdaterEntities(bool isJson)
			: base(JsonConnection.EntityConnectionString)
		{
			ConnectionString = JsonConnection.EntityConnectionString;
		}

		public string ConnectionString { get; private set; }
	}
}
