using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterSettings
{
    public static class ApplicationSettings
    {
        public static JsonLocalSettings JsonConnectionSettings { get; private set; }
        public static void LoadConnectionInfoFromJson()
        {
            JsonConnectionSettings = NewtonJson.ReadJsonFile<JsonLocalSettings>(Constants.JsonConnectionSettingsFileName);
        }

        public static bool UpdateConnectionSettings()
        {
            try
            {
                NewtonJson.WriteJsonFile(JsonConnectionSettings, Constants.JsonConnectionSettingsFileName);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static string _connectionString;

        public static string BuildConnectionString(string serverTask, 
                                                    string baseTask, 
                                                    string userTask, 
                                                    string passwordTask)
        {
            ApplicationSettings.JsonConnectionSettings.ServerTask = serverTask;
            ApplicationSettings.JsonConnectionSettings.BaseTask = baseTask;
            ApplicationSettings.JsonConnectionSettings.UserTask = userTask;
            ApplicationSettings.JsonConnectionSettings.PasswordTask = passwordTask;
            ApplicationSettings.UpdateConnectionSettings();
            _connectionString = $"Data Source={serverTask};Initial Catalog={baseTask};User ID={userTask};Password={passwordTask}";
            return _connectionString;
        }
        private static string BuildConnectionString()
        {
            var serverTask = ApplicationSettings.JsonConnectionSettings.ServerTask;
            var baseTask = ApplicationSettings.JsonConnectionSettings.BaseTask;
            var userTask = ApplicationSettings.JsonConnectionSettings.UserTask;
            var passwordTask = ApplicationSettings.JsonConnectionSettings.PasswordTask;
            _connectionString = $"Data Source={serverTask};Initial Catalog={baseTask};User ID={userTask};Password={passwordTask}";

            JsonConnectionSettings.ConnectionString = _connectionString;

            return _connectionString;
        }

        public static string ConnectionString
        {
            get { return string.IsNullOrEmpty(_connectionString) ? BuildConnectionString() : _connectionString; }
            private set { value = _connectionString; }
        }
    }
}
