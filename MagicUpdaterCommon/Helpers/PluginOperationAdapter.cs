using MagicUpdaterCommon.Common;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.SettingsTools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using static MagicUpdaterCommon.SettingsTools.MainSettings;

namespace MagicUpdaterCommon.Helpers
{
	public class Proxy : MarshalByRefObject
	{
		public Assembly GetAssembly(string assemblyPath)
		{
			try
			{
				return Assembly.LoadFile(assemblyPath);
			}
			catch (Exception ex)
			{
				return null;
				// throw new InvalidOperationException(ex);
			}
		}
	}
	#region TryResult
	public class TryPluginOperation : TryResult
	{
		public TryPluginOperation(PluginOperation pluginOperation, bool isComplete = true, string message = "") : base(isComplete, message)
		{
			PluginOperationInstance = pluginOperation;
		}

		public PluginOperation PluginOperationInstance { get; private set; }
	}

	public class TryRegisterPlugin : TryResult
	{
		public TryRegisterPlugin(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryRegisterOrUpdateAllPlugins : TryResult
	{
		public TryRegisterOrUpdateAllPlugins(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}

	public class TryGetOrUpdateFileFromFtp : TryResult
	{
		public TryGetOrUpdateFileFromFtp(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
	#endregion TryResult

	public class PluginOperation
	{
		public PluginOperation(string operationTypeName
							 , string sqlName
							 , string attributesTypeName
							 , string dllFullPath
							 , Dictionary<string, string> registrationParams = null)
		{
			OperationTypeName = operationTypeName;
			SqlName = sqlName;
			DllFullPath = dllFullPath;
			AttributesTypeName = attributesTypeName;
			RegistrationParams = registrationParams;
		}

		public string OperationTypeName { get; private set; }
		public string SqlName { get; private set; }
		public string AttributesTypeName { get; private set; }
		public string DllFullPath { get; private set; }
		public Dictionary<string, string> RegistrationParams { get; private set; }
	}

	public static class PluginOperationAdapter
	{
		public static AppDomain Domain { get; private set; }

		private static List<PluginOperation> GetPluginOperationList()
		{
			var list = new List<PluginOperation>();

			if (!Directory.Exists(MainSettings.Constants.PluginOperationDllDirectoryPath))
			{
				return list;
			}

			foreach (string fileOn in Directory.GetFiles(MainSettings.Constants.PluginOperationDllDirectoryPath))
			{
				FileInfo file = new FileInfo(fileOn);

				if (file.Extension.Equals(".dll"))
				{
					list.Add(GetFromDll(file.FullName).PluginOperationInstance);
				}
			}

			list.RemoveAll(x => x == null);
			return list;
		}

		//Тут используется "TryPluginOperation" на случай если эта процедура понадобиться как Public
		//Внутри этого класса проверка по "TryPluginOperation" отсутствует
		public static TryPluginOperation GetFromDll(string FullPath)
		{
			Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(FullPath));

			IEnumerable<Type> operationPlugin = pluginAssembly.GetTypes().OfType<Type>().Where(w => w.IsPublic && !w.IsAbstract && (w.GetInterface("MagicUpdaterCommon.Abstract.IOperation", true) != null));
			IEnumerable<Type> attributesPlugin = pluginAssembly.GetTypes().OfType<Type>().Where(w => w.IsPublic && !w.IsAbstract && (w.GetInterface("MagicUpdaterCommon.Abstract.IOperationAttributes", true) != null));
			IEnumerable<Type> registrationParams = pluginAssembly.GetTypes().OfType<Type>().Where(w => w.IsPublic && !w.IsAbstract && (w.GetInterface("MagicUpdaterCommon.Abstract.IRegistrationParams", true) != null));

			//Операция
			if (operationPlugin.Count() == 1)
			{
				Type operationPluginType = operationPlugin.First();
				Dictionary<string, string> registrationParamsDictionary = new Dictionary<string, string>();

				//Параметры для SQL
				if (registrationParams.Count() > 1)
				{
					return new TryPluginOperation(null, false, $"Неверный формат параметров SQL библиотеки подключаемой операции {FullPath}.  Интерфейс \"IRegistrationParams\" в количестве более одного.");
				}
				else if (registrationParams.Count() == 1)
				{
					Type registrationParamsType = registrationParams.First();
					var registrationParamsInstance = Activator.CreateInstance(registrationParamsType);
					foreach (PropertyInfo pi in registrationParamsType.GetProperties())
					{
						registrationParamsDictionary.Add(pi.Name, ConvertSafe.ToString(pi.GetValue(registrationParamsInstance, null)));
					}
				}

				//Атрибуты
				Type attributesPluginType = null;
				if (attributesPlugin.Count() == 1)
				{
					attributesPluginType = attributesPlugin.First();
				}
				else if (attributesPlugin.Count() > 1)
				{
					return new TryPluginOperation(null, false, $"Неверный формат атрибутов библиотеки подключаемой операции {FullPath}.  Интерфейс \"IOperationAttributes\" отсутствует или их более одного.");
				}

				return new TryPluginOperation(
						new PluginOperation(operationPluginType.ToString()
											, operationPluginType.Name
											, attributesPluginType?.ToString()
											, FullPath
											, registrationParamsDictionary
											));
			}
			else
			{
				return new TryPluginOperation(null, false, $"Неверный формат библиотеки подключаемой операции {FullPath}. Интерфейс \"IOperation\" отсутствует или их более одного.");
			}
		}

		private static TryGetOrUpdateFileFromFtp GetOrUpdateFileFromFtpLastModifiedDate(string fileName, DateTime? fileLastModifiedDateFromSql)
		{
			//Создаем папку для библиотек операций
			if (!Directory.Exists(MainSettings.Constants.PluginOperationDllDirectoryPath))
			{
				Directory.CreateDirectory(MainSettings.Constants.PluginOperationDllDirectoryPath);
			}

			if (!string.IsNullOrEmpty(fileName))
			{
				//Если файл существует, но его MD5 не совпадает с тем что в базе - удаляем его (он устаревший)
				if (File.Exists(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)) &&
					!Extensions.IsNullableDateTimeIdentical(File.GetLastWriteTime(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)), fileLastModifiedDateFromSql))
				{
					File.Delete(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName));
				}

				//Если файла не существует - закачиваем его
				if (!File.Exists(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)))
				{
					FtpWorks.DownloadFileFromFtpOld(
						Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName),
						Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpServer, MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME, fileName),
						MainSettings.GlobalSettings.SelfUpdateFtpUser,
						MainSettings.GlobalSettings.SelfUpdateFtpPassword);
				}

				return new TryGetOrUpdateFileFromFtp();
			}
			else
			{
				return new TryGetOrUpdateFileFromFtp(false, "Ошибка вызова функции (GetOrUpdateFileFromFtp), неверные входные параметры");
			}
		}

		private static TryGetOrUpdateFileFromFtp GetOrUpdateFileFromFtpMd5(string fileName, string fileMD5FromSql)
		{
			//Создаем папку для библиотек операций
			if (!Directory.Exists(MainSettings.Constants.PluginOperationDllDirectoryPath))
			{
				Directory.CreateDirectory(MainSettings.Constants.PluginOperationDllDirectoryPath);
			}

			if (!string.IsNullOrEmpty(fileName))
			{
				//Если файл существует, но его MD5 не совпадает с тем что в базе - удаляем его (он устаревший)
				if (File.Exists(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)) &&
					!MD5Works.CompareHahes(MD5Works.GetFileHash(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)), fileMD5FromSql))
				{
					File.Delete(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName));
				}

				//Если файла не существует - закачиваем его
				if (!File.Exists(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)))
				{
					var res = FtpWorks.DownloadFileFromFtp(MainSettings.Constants.PluginOperationDllDirectoryPath
						, MainSettings.GlobalSettings.SelfUpdateFtpServer
						, MainSettings.GlobalSettings.SelfUpdateFtpUser
						, MainSettings.GlobalSettings.SelfUpdateFtpPassword
						, Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
						, fileName);

					if (!res.IsComplete)
					{
						return new TryGetOrUpdateFileFromFtp(false, res.Message);
					}

					//FtpWorks.DownloadFileFromFtpOld(
					//	Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName),
					//	Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpServer, MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME, fileName),
					//	MainSettings.GlobalSettings.SelfUpdateFtpUser,
					//	MainSettings.GlobalSettings.SelfUpdateFtpPassword);
				}

				if (!File.Exists(Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName)))
				{
					return new Helpers.TryGetOrUpdateFileFromFtp(false, $"Ошибка закачки файла плагин-операции {fileName} по ftp");
				}

				return new TryGetOrUpdateFileFromFtp();
			}
			else
			{
				return new TryGetOrUpdateFileFromFtp(false, "Ошибка вызова функции (GetOrUpdateFileFromFtp), неверные входные параметры");
			}
		}

		private static PluginOperation GetPluginOperation(string name, string fileName, string fileMD5)
		{
			//Актуализируем файл из фтп, если нужно
			var res = GetOrUpdateFileFromFtpMd5(fileName, fileMD5);
			if (!res.IsComplete)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, $"Ошибка скачивания файла ({fileName}) с ftp в директорию ({MainSettings.Constants.PluginOperationDllDirectoryPath})");
				return null;
			}

			//Если директории не существует или она пустая
			if (!Directory.Exists(MainSettings.Constants.PluginOperationDllDirectoryPath) || Directory.GetFiles(MainSettings.Constants.PluginOperationDllDirectoryPath).Length == 0)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, $"Директория ({MainSettings.Constants.PluginOperationDllDirectoryPath}) пуста или не существует");
				return null;
			}

			//Пробуем найти плагин операции по зарегистрированному в sql имени (имени класса операции)
			return GetPluginOperationList().Where(w => w.SqlName == name).FirstOrDefault();
		}

		public static Type GetPluginOperationType(string name, string fileName, string fileMD5)
		{
			try
			{
				PluginOperation pluginOperation = GetPluginOperation(name, fileName, fileMD5);

				Assembly pluginAssembly = Assembly.Load(File.ReadAllBytes(pluginOperation.DllFullPath));

				return pluginAssembly.GetType(pluginOperation.OperationTypeName);
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
				return null;
			}
		}

		public static Type GetPluginOperationAttributesType(string name, string fileName, string fileMD5)
		{
			try
			{
				PluginOperation pluginOperation = GetPluginOperation(name, fileName, fileMD5);
				byte[] arr = File.ReadAllBytes(pluginOperation.DllFullPath);
				Assembly pluginAssembly = Assembly.Load(arr);
				if (pluginOperation.AttributesTypeName == null)
				{
					return null;
				}
				else
				{
					return pluginAssembly.GetType(pluginOperation.AttributesTypeName);
				}
			}
			catch (Exception ex)
			{
				NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, ex.ToString(), MainSettings.Constants.MAGIC_UPDATER);
				return null;
			}
		}

		public static List<string> GetUnregistredOrUnrelevantFileNamesMd5()
		{
			var res = new List<string>();

			var list = FtpWorks.GetFilesList(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , ".dll");
			foreach (var fileName in list)
			{
				if (!IsFileRegisteregAndRelevantMd5(fileName))
				{
					res.Add(fileName);
				}
			}

			return res;
		}

		public static List<string> GetUnregistredOrUnrelevantFileNamesLastmodifiedDate()
		{
			var res = new List<string>();

			var list = FtpWorks.GetFilesList(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , ".dll");

			foreach (var fileName in list)
			{
				if (!IsFileRegisteregAndRelevantlastModifiedDate(fileName))
				{
					res.Add(fileName);
				}
			}

			return res;
		}

		public static TryRegisterOrUpdateAllPlugins RegisterOrUpdateAllPlugins()
		{
			string errMessage = "";
			foreach (var fileName in GetUnregistredOrUnrelevantFileNamesLastmodifiedDate())
			{
				var res = RegisterOrUpdatePluginLastModifiedDate(fileName);
				if (!res.IsComplete)
				{
					if (string.IsNullOrEmpty(errMessage))
					{
						errMessage = $"[{fileName}] {res.Message}";
					}
					else
					{
						errMessage = $"{errMessage}{Environment.NewLine}[{fileName}] {res.Message}";
					}
				}
				//RegisterOrUpdatePluginMd5("", fileName);
			}

			if (string.IsNullOrEmpty(errMessage))
			{
				return new TryRegisterOrUpdateAllPlugins();
			}
			else
			{
				return new TryRegisterOrUpdateAllPlugins(false, errMessage);
			}
		}

		private static bool IsFileRegisteregAndRelevantlastModifiedDate(string fileName)
		{
			DataSet ds = SqlWorks.ExecProc("GetOperationTypeByFileName", fileName);
			if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			else
			{
				string dllFullPath = Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath
																, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME
																, fileName);
				DateTime? ftpFileLastModifiedDate = FtpWorks.GetFileLastModifiedDate(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , fileName);

				if (ftpFileLastModifiedDate.HasValue)
				{
					try
					{
						DataRow dr = ds.Tables[0].Rows[0];
						return Extensions.IsNullableDateTimeIdentical(ftpFileLastModifiedDate, (DateTime?)dr["LastModifiedDate"]);
					}
					catch
					{
						return false;
					}
				}
				else
				{
					NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, $"Ошибка получения последней даты изменения файла ({dllFullPath})");
					return false;
				}
			}
		}

		private static bool IsFileRegisteregAndRelevantMd5(string fileName)
		{
			DataSet ds = SqlWorks.ExecProc("GetOperationTypeByFileName", fileName);
			if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
			{
				return false;
			}
			else
			{
				string dllFullPath = Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath
																, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME
																, fileName);

				//var ftpFileMd5Result = FtpWorks.GetFileMD5(MainSettings.GlobalSettings.SelfUpdateFtpServer
				//								   , MainSettings.GlobalSettings.SelfUpdateFtpUser
				//								   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
				//								   , dllFullPath);

				var ftpFileMd5Result = FtpWorks.GetFtpFileMd5(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , fileName);

				if (ftpFileMd5Result.IsComplete)
				{
					try
					{
						DataRow dr = ds.Tables[0].Rows[0];
						return MD5Works.CompareHahes(ftpFileMd5Result.Value, Convert.ToString(dr["FileMD5"]));
					}
					catch
					{
						return false;
					}
				}
				else
				{
					NLogger.LogErrorToBaseAndHdd(MainSettings.MainSqlSettings?.ComputerId, $"Ошибка получения MD5 файла ({dllFullPath})");
					return false;
				}
			}
		}

		public static TryRegisterPlugin RegisterOrUpdatePluginLastModifiedDate(string fileName)
		{
			string dllLocalFullPath = Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName);

			string dllFtpFullPath = Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath
																, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME
																, fileName);
			DateTime? ftpFileLastModifiedDate = FtpWorks.GetFileLastModifiedDate(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , fileName);

			if (ftpFileLastModifiedDate.HasValue)
			{
				try
				{
					DataSet ds = SqlWorks.ExecProc("GetOperationTypeByFileName", fileName);
					if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
					{
						//Регистрируем
						//Актуализируем файл из фтп, если нужно
						var res = GetOrUpdateFileFromFtpLastModifiedDate(fileName, null);
						if (!res.IsComplete)
						{
							return new TryRegisterPlugin(false, $"Ошибка скачивания файла ({fileName}) с ftp в директорию ({MainSettings.Constants.PluginOperationDllDirectoryPath})");
						}
						else
						{
							/*
							 @Name nvarchar(512)
							,@NameRus nvarchar(512)
							,@GroupId int
							,@FileName nvarchar(510)
							,@FileMD5 nvarchar(64)
							,@Description nvarchar(MAX)
							 */

							//Пробуем получить имя типа операции
							var dllResult = GetFromDll(dllLocalFullPath);
							if (!dllResult.IsComplete)
							{
								return new TryRegisterPlugin(false, $"Ошибка получения типа операции из файла ({dllLocalFullPath})");
							}

							var addDs = SqlWorks.ExecProc("AddOperationType"
											 , dllResult.PluginOperationInstance.SqlName
											 , ""
											 , 1
											 , fileName
											 , ftpFileLastModifiedDate
											 , MD5Works.GetFileHash(dllLocalFullPath).ToUpper()
											 , "");

							if (addDs != null && addDs.Tables.Count > 0 && addDs.Tables[0].Rows.Count > 0)
							{
								int id = ConvertSafe.ToInt32(addDs.Tables[0].Rows[0]["Id"]);
								foreach (var param in dllResult.PluginOperationInstance.RegistrationParams)
								{
									var resDs = SqlWorks.ExecProc("UpdateOperationTypesFieldsById"
											 , id
											 , param.Key
											 , param.Value);

									if (resDs != null && resDs.Tables.Count > 0 && resDs.Tables[0].Rows.Count > 0)
									{
										if (Convert.ToInt32(resDs.Tables[0].Rows[0]["ResultId"]) == -1)
										{
											return new TryRegisterPlugin(false, $"Ошибка записи параметров SQL из файла ({dllLocalFullPath}). SQL Error: {Convert.ToString(resDs.Tables[0].Rows[0]["Message"])}");
										}
									}
									else
									{
										return new TryRegisterPlugin(false, $"Ошибка записи параметров SQL из файла ({dllLocalFullPath})");
									}
								}
							}

							return new TryRegisterPlugin();
						}
					}
					else
					{
						DataRow dr = ds.Tables[0].Rows[0];
						DateTime? sqlDateTime = null;
						try
						{
							sqlDateTime = (DateTime?)dr["LastModifiedDate"];
						}
						catch
						{
							sqlDateTime = null;
						}
						//Обновляем, если нужно
						if (!Extensions.IsNullableDateTimeIdentical(ftpFileLastModifiedDate, sqlDateTime))
						{
							//Актуализируем файл из фтп, если нужно
							var res = GetOrUpdateFileFromFtpLastModifiedDate(fileName, sqlDateTime);
							if (!res.IsComplete)
							{
								return new TryRegisterPlugin(false, $"Ошибка скачивания файла ({fileName}) с ftp в директорию ({MainSettings.Constants.PluginOperationDllDirectoryPath})");
							}
							else
							{
								var dllResult = GetFromDll(dllLocalFullPath);
								if (!dllResult.IsComplete)
								{
									return new TryRegisterPlugin(false, $"Ошибка получения типа операции из файла ({dllLocalFullPath})");
								}

								SqlWorks.ExecProc("UpdateOperationTypeById"
												  , Convert.ToInt32(dr["Id"])
												  , dllResult.PluginOperationInstance.SqlName
												  , ""
												  , 1
												  , fileName
												  , ftpFileLastModifiedDate
												  , MD5Works.GetFileHash(dllLocalFullPath).ToUpper()
												  , "");

								foreach (var param in dllResult.PluginOperationInstance.RegistrationParams)
								{
									var resDs = SqlWorks.ExecProc("UpdateOperationTypesFieldsById"
											 , Convert.ToInt32(dr["Id"])
											 , param.Key
											 , param.Value);

									if (resDs != null && resDs.Tables.Count > 0 && resDs.Tables[0].Rows.Count > 0)
									{
										if (Convert.ToInt32(resDs.Tables[0].Rows[0]["ResultId"]) == -1)
										{
											return new TryRegisterPlugin(false, $"Ошибка записи параметров SQL из файла ({dllLocalFullPath}). SQL Error: {Convert.ToString(resDs.Tables[0].Rows[0]["Message"])}");
										}
									}
									else
									{
										return new TryRegisterPlugin(false, $"Ошибка записи параметров SQL из файла ({dllLocalFullPath})");
									}
								}

								return new TryRegisterPlugin();
							}
						}

						return new TryRegisterPlugin();
					}
				}
				catch (Exception ex)
				{
					return new TryRegisterPlugin(false, ex.ToString());
				}
			}
			else
			{
				return new TryRegisterPlugin(false, $"Не удалось получить дату последнего изменения файла ({fileName}) по ftp");
			}
		}

		public static TryRegisterPlugin RegisterOrUpdatePluginMd5(string nameRus, string fileName, int groupId = 1 /*стандартные операции*/, string description = "")
		{
			string dllLocalFullPath = Path.Combine(MainSettings.Constants.PluginOperationDllDirectoryPath, fileName);

			string dllFtpFullPath = Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath
																, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME
																, fileName);
			var ftpFileMd5Result = FtpWorks.GetFtpFileMd5(MainSettings.GlobalSettings.SelfUpdateFtpServer
												   , MainSettings.GlobalSettings.SelfUpdateFtpUser
												   , MainSettings.GlobalSettings.SelfUpdateFtpPassword
												   , Path.Combine(MainSettings.GlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.OPERATION_PLUGIN_DIRECTORY_NAME)
												   , fileName);
			if (ftpFileMd5Result.IsComplete)
			{
				try
				{
					string ftpFileMd5 = ftpFileMd5Result.Value;
					DataSet ds = SqlWorks.ExecProc("GetOperationTypeByFileName", fileName);
					if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
					{
						//Регистрируем
						//Актуализируем файл из фтп, если нужно
						var res = GetOrUpdateFileFromFtpMd5(fileName, null);
						if (!res.IsComplete)
						{
							return new TryRegisterPlugin(false, $"Ошибка скачивания файла ({fileName}) с ftp в директорию ({MainSettings.Constants.PluginOperationDllDirectoryPath})");
						}
						else
						{
							/*
							 @Name nvarchar(512)
							,@NameRus nvarchar(512)
							,@GroupId int
							,@FileName nvarchar(510)
							,@FileMD5 nvarchar(64)
							,@Description nvarchar(MAX)
							 */

							//Пробуем получить имя типа операции
							var dllResult = GetFromDll(dllLocalFullPath);
							if (!dllResult.IsComplete)
							{
								return new TryRegisterPlugin(false, $"Ошибка плучения типа операции из файла ({dllLocalFullPath})");
							}

							SqlWorks.ExecProc("AddOperationType"
											 , dllResult.PluginOperationInstance.SqlName
											 , nameRus
											 , groupId
											 , fileName
											 , null
											 , ftpFileMd5
											 , description);

							return new TryRegisterPlugin();
						}
					}
					else
					{
						DataRow dr = ds.Tables[0].Rows[0];
						//Обновляем, если нужно
						if (!MD5Works.CompareHahes(ftpFileMd5, Convert.ToString(dr["FileMD5"])))
						{
							//Актуализируем файл из фтп, если нужно
							var res = GetOrUpdateFileFromFtpMd5(fileName, Convert.ToString(dr["FileMD5"]));
							if (!res.IsComplete)
							{
								return new TryRegisterPlugin(false, $"Ошибка скачивания файла ({fileName}) с ftp в директорию ({MainSettings.Constants.PluginOperationDllDirectoryPath})");
							}
							else
							{
								var dllResult = GetFromDll(dllLocalFullPath);
								if (!dllResult.IsComplete)
								{
									return new TryRegisterPlugin(false, $"Ошибка плучения типа операции из файла ({dllLocalFullPath})");
								}

								SqlWorks.ExecProc("UpdateOperationTypeById"
												  , Convert.ToInt32(dr["Id"])
												  , dllResult.PluginOperationInstance.SqlName
												  , nameRus
												  , groupId
												  , fileName
												  , null
												  , ftpFileMd5
												  , description);

								return new TryRegisterPlugin();
							}
						}

						return new TryRegisterPlugin();
					}
				}
				catch (Exception ex)
				{
					return new TryRegisterPlugin(false, ex.ToString());
				}
			}
			else
			{
				return new TryRegisterPlugin(false, $"Не удалось получить MD5 файла ({fileName}) по ftp");
			}
		}
	}
}
