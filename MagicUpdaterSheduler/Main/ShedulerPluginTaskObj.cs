using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdaterSheduler.Main
{
	public class ShedulerPluginTaskObj : ShedulerPluginTask
	{
		private const string PLUGIN_DIRECTORY = "OperationPlugin";
		private readonly string _pluginDirectiryPath = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), PLUGIN_DIRECTORY);

		public static List<ShedulerPluginTaskObj> GetShedulerTasks()
		{
			var tasks = MQueryCommand.SelectShedulerPluginTasks();

			return tasks.Select(c => new ShedulerPluginTaskObj
			{
				Id = c.Id,
				LastEndTime = c.LastEndTime,
				LastStartTime = c.LastStartTime,
				Mode = c.Mode,
				Name = c.Name,
				NextStartTime = c.NextStartTime,
				PluginFileName = c.PluginFileName,
				Status = c.Status,
				Enabled = c.Enabled,
				RepeatValue = c.RepeatValue
			}).ToList();
		}

		public void Run()
		{
			MQueryCommand.TryUpdatePluginTaskLastStartTime(Id, DateTime.UtcNow);
			if (!Directory.Exists(_pluginDirectiryPath))
			{
				MLogger.Error($"Directory not exists: {_pluginDirectiryPath}");
				return;
			}

			string pluginFileFullPath = Path.Combine(_pluginDirectiryPath, PluginFileName);

			if (!File.Exists(pluginFileFullPath))
			{
				MLogger.Error($"File not exists: {pluginFileFullPath}");
				return;
			}

			var res = PluginOperationAdapter.GetFromDll(pluginFileFullPath);

			if (!res.IsComplete)
			{
				MLogger.Error(res.Message);
			}

			try
			{
				PluginOperation pluginOperation = res.PluginOperationInstance;
				Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginOperation.DllFullPath));
				Type pluginOperationAttributesType = null;

				if (!string.IsNullOrEmpty(pluginOperation.AttributesTypeName))
				{
					pluginOperationAttributesType = pluginAssembly.GetType(pluginOperation.AttributesTypeName);
				}

				Type pluginOperationType = pluginAssembly.GetType(pluginOperation.OperationTypeName);
				MagicUpdaterCommon.Abstract.Operation operation = null;
				string settingsJson = null;
				if (pluginOperationAttributesType != null)
				{
					string pluginSettingsJsonFileFullPath = Path.Combine(_pluginDirectiryPath, $"{PluginFileName.Replace($"{Path.GetExtension(PluginFileName)}", "")}.json");

					if (!File.Exists(pluginSettingsJsonFileFullPath))
					{
						MLogger.Error($"Missing settings json for plugin operation: {pluginSettingsJsonFileFullPath}");
						return;
					}

					settingsJson = File.ReadAllText(pluginSettingsJsonFileFullPath);

				}

				if (pluginOperationType.GetConstructors()[0].GetParameters().Length == 2)
				{
					operation = (MagicUpdaterCommon.Abstract.Operation)Activator.CreateInstance(pluginOperationType, 0, settingsJson);
				}
				else
				{
					operation = (MagicUpdaterCommon.Abstract.Operation)Activator.CreateInstance(pluginOperationType, 0);
				}

				if (operation != null)
				{
					operation.Run();
				}
				else
				{
					MLogger.Error("Plugin operation instance is null!");
				}
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());
			}

			MQueryCommand.TryUpdatePluginTaskLastEndTime(Id, DateTime.UtcNow);

			DateTime period = new DateTime(1889, 1, 1, 0, 0, 0);
			switch ((ShedulerTaskModes)Mode)
			{
				case ShedulerTaskModes.Once:
					MQueryCommand.TrySetPluginTaskEnabled(Id, false);

					period = new DateTime(1889, 1, 1, 0, 0, 0);
					break;
				case ShedulerTaskModes.Daily:
					period = DateTime.UtcNow.AddDays(1);
					break;
				case ShedulerTaskModes.Weekly:
					period = DateTime.UtcNow.AddDays(7);
					break;
				case ShedulerTaskModes.Monthly:
					period = DateTime.UtcNow.AddMonths(1);
					break;
				case ShedulerTaskModes.Hours:
					period = DateTime.UtcNow.AddHours(RepeatValue ?? 0);
					break;
				case ShedulerTaskModes.Minutes:
					period = DateTime.UtcNow.AddMinutes(RepeatValue ?? 0);
					break;
			}

			MQueryCommand.TryUpdatePluginTaskNextStartTime(Id, period);
		}

		public ShedulerStatuses ShedulerStatus => (ShedulerStatuses)Status;
		public bool IsTimeToRun => (Enabled ?? false) && (DateTime.UtcNow >= NextStartTime);
	}
}
