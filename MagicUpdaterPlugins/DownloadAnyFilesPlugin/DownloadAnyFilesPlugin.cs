using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using MagicUpdaterCommon.Helpers;
using System.Diagnostics;
using MagicUpdaterCommon.SettingsTools;

namespace DownloadAnyFilesPlugin
{
	public class DownloadAnyFilesPlugin : OperationWithAttr<DownloadAnyFilesAttr>
	{
		private const string WGET_EXE = @"wget.exe";

		private string _ftpHelpFilesPath = Path.Combine(MainSettings.LocalSqlSettings.SelfUpdateFtpPath, MainSettings.Constants.HELP_FILES_FOLDER);
		private string _localWgetPath = Path.Combine(Path.GetTempPath(), WGET_EXE);

		public DownloadAnyFilesPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			#region Качаем wget
			if (File.Exists(_localWgetPath))
			{
				File.Delete(_localWgetPath);
			}

			FtpWorks.DownloadFileFromFtp(Path.GetDirectoryName(_localWgetPath)
										, MainSettings.LocalSqlSettings.SelfUpdateFtpServer
										, MainSettings.LocalSqlSettings.SelfUpdateFtpUser
										, MainSettings.LocalSqlSettings.SelfUpdateFtpPassword
										, _ftpHelpFilesPath
										, WGET_EXE);

			if (!File.Exists(_localWgetPath))
			{
				AddErrorMessage($"Не найден файл {_localWgetPath}");
				return;
			}
			#endregion

			if (Attributes.IsForceCreatePath)
			{
				if (!Directory.Exists(Attributes.LocalPath))
				{
					Directory.CreateDirectory(Attributes.LocalPath);
				} 
			}

			if (Attributes.IsOverwrite)
			{
				if (File.Exists(Path.Combine(Attributes.LocalPath, Attributes.FileName)))
				{
					File.Delete(Path.Combine(Attributes.LocalPath, Attributes.FileName));
				}
			}

			Process process = Process.Start($"{_localWgetPath}", $" -c -nc ftp://{Attributes.FtpLogin}:{Attributes.FtpPassword}@{Attributes.FtpServer}/{Attributes.FtpPath}/{Attributes.FileName} -P \"{Attributes.LocalPath}\"");

			if (Attributes.IsWaitForDownload)
			{
				process.WaitForExit();

				if (File.Exists(Path.Combine(Attributes.LocalPath, Attributes.FileName)))
				{
					AddCompleteMessage($"Файл {Path.Combine(Attributes.LocalPath, Attributes.FileName)} скачан успешно");
				}
				else
				{
					AddErrorMessage($"Ошибка закачки файла. Файл на клиенте не обнаружен {Path.Combine(Attributes.LocalPath, Attributes.FileName)}");
					return;
				}

				if (Attributes.IsExecuteAfterDownload)
				{
					AddCompleteMessage($"Запуск файла {Path.Combine(Attributes.LocalPath, Attributes.FileName)}");

					try
					{
						ExecuteApplication(Path.Combine(Attributes.LocalPath, Attributes.FileName)
													, Attributes.StartupParameters
													, Attributes.IsWaitForFileFinishExcecute);

						if (!Attributes.IsWaitForFileFinishExcecute)
						{
							AddCompleteMessage($"Файл {Path.Combine(Attributes.LocalPath, Attributes.FileName)} - запущен успешно");
						}
						else
						{
							AddCompleteMessage($"Файл {Path.Combine(Attributes.LocalPath, Attributes.FileName)} - запущен успешно и его выполнение завершено");
						}
					}
					catch (Exception ex)
					{
						AddErrorMessage($"Ошибка выполнения файла {Path.Combine(Attributes.LocalPath, Attributes.FileName)}. Original: {ex.ToString()}");
					}
				}
			}
			else
			{
				AddCompleteMessage($"Закачка файла {Path.Combine(Attributes.LocalPath, Attributes.FileName)} начата.");
			}
		}

		private static int ExecuteApplication(string exeName, string arguments, bool isWaitForExit = false)
		{
			ProcessStartInfo start = new ProcessStartInfo();
			start.Arguments = arguments;
			start.FileName = exeName;
			start.WindowStyle = ProcessWindowStyle.Hidden;
			start.CreateNoWindow = true;

			int exitCode = 0;

			using (Process proc = Process.Start(start))
			{
				if (isWaitForExit)
				{
					proc.WaitForExit();
					exitCode = proc.ExitCode; 
				}
			}
			return exitCode;
		}

	}


	public class DownloadAnyFilesAttr : IOperationAttributes
	{

		[OperationAttributeDisplayName("Путь локальный")]
		public string LocalPath { get; set; } = @"C:\Distr";

		[OperationAttributeDisplayName("Сервер ftp")]
		public string FtpServer { get; set; }

		[OperationAttributeDisplayName("Логин ftp")]
		public string FtpLogin { get; set; }

		[OperationAttributeDisplayName("Пароль ftp")]
		public string FtpPassword { get; set; }

		[OperationAttributeDisplayName("Путь ftp")]
		public string FtpPath { get; set; }

		[OperationAttributeDisplayName("Имя файла с расширением")]
		public string FileName { get; set; }

		[OperationAttributeDisplayName("Перезаписать файл")]
		public bool IsOverwrite { get; set; } = true;

		[OperationAttributeDisplayName("Создать папки по пути")]
		public bool IsForceCreatePath { get; set; } = true;

		[OperationAttributeDisplayName("Ждать закачки файла")]
		public bool IsWaitForDownload { get; set; } = true;

		[OperationAttributeDisplayName("Выполнить")]
		public bool IsExecuteAfterDownload { get; set; } = false;

		[OperationAttributeDisplayName("Параметры запуска")]
		public string StartupParameters { get; set; } = "";

		[OperationAttributeDisplayName("Ждать завершения выполнения")]
		public bool IsWaitForFileFinishExcecute { get; set; } = false;
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "Закачка/Запуск произвольного файла из ftp";

		public int GroupId => 2;

		public string NameRus => "Закачка/Запуск произвольного файла";
	}
}
