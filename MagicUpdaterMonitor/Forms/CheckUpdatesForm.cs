using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class CheckUpdatesForm : BaseForm
	{
		private const string AGENT_UPDATE_SERVER_FOLDER = "Agent";
		private const string MONITOR_UPDATE_SERVER_FOLDER = "Monitor";

		private string _server;
		private string _login;
		private string _password;

		private CommonGlobalSettings _commonGlobalSettings;

		public CheckUpdatesForm()
		{
			_commonGlobalSettings = new CommonGlobalSettings();
			_commonGlobalSettings.LoadCommonGlobalSettings();

			InitializeComponent();

			tbServer.Text = _commonGlobalSettings.UpdateVersionRemoteServer ?? "";
			tbLogin.Text = _commonGlobalSettings.UpdateVersionRemoteLogin ?? "";
			tbPassword.Text = _commonGlobalSettings.UpdateVersionRemotePassword ?? "";
		}

		private void StartAgentProgressBarsMarquee()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					pbAgent.Style = ProgressBarStyle.Marquee;
					pbAgent.MarqueeAnimationSpeed = 30;
				}));
			}
			else
			{
				pbAgent.Style = ProgressBarStyle.Marquee;
				pbAgent.MarqueeAnimationSpeed = 30;
			}
		}

		private void StopAgentProgressBarsMarquee()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					pbAgent.Style = ProgressBarStyle.Blocks;
				}));
			}
			else
			{
				pbAgent.Style = ProgressBarStyle.Blocks;
			}
		}

		private void StartMonitorProgressBarsMarquee()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					pbMonitor.Style = ProgressBarStyle.Marquee;
					pbMonitor.MarqueeAnimationSpeed = 30;
				}));
			}
			else
			{
				pbMonitor.Style = ProgressBarStyle.Marquee;
				pbMonitor.MarqueeAnimationSpeed = 30;
			}
		}

		private void StopMonitorProgressBarsMarquee()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					pbMonitor.Style = ProgressBarStyle.Blocks;
				}));
			}
			else
			{
				pbMonitor.Style = ProgressBarStyle.Blocks;
			}
		}

		private void SetLbAgentText(string text)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					lbAgent.Text = text;
				}));
			}
			else
			{
				lbAgent.Text = text;
			}
		}

		private void SetLbMonitorText(string text)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					lbMonitor.Text = text;
				}));
			}
			else
			{
				lbMonitor.Text = text;
			}
		}

		private void SetBtnDownloadUpdateAgentEnabled(bool enabled)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					btnDownloadUpdateAgent.Enabled = enabled;
				}));
			}
			else
			{
				btnDownloadUpdateAgent.Enabled = enabled;
			}
		}

		private void SetBtnDownloadUpdateMonitorEnabled(bool enabled)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					btnDownloadUpdateMonitor.Enabled = enabled;
				}));
			}
			else
			{
				btnDownloadUpdateMonitor.Enabled = enabled;
			}
		}

		private async void btnCheckUpdates_Click(object sender, EventArgs e)
		{
			StartAgentProgressBarsMarquee();
			StartMonitorProgressBarsMarquee();
			_server = tbServer.Text;
			_login = tbLogin.Text;
			_password = tbPassword.Text;

			await Task.Factory.StartNew(() =>
			{
				if (string.IsNullOrEmpty(_server))
				{
					MessageBox.Show("Пустое имя сервера", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}
				if (string.IsNullOrEmpty(_login))
				{
					MessageBox.Show("Пустой логин", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}
				if (string.IsNullOrEmpty(_password))
				{
					MessageBox.Show("Пустой пароль", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}

				if (_commonGlobalSettings.UpdateVersionRemoteServer != _server ||
					_commonGlobalSettings.UpdateVersionRemoteLogin != _login ||
					_commonGlobalSettings.UpdateVersionRemotePassword != _password)
				{
					_commonGlobalSettings.UpdateVersionRemoteServer = _server;
					_commonGlobalSettings.UpdateVersionRemoteLogin = _login;
					_commonGlobalSettings.UpdateVersionRemotePassword = _password;
					SqlWorks.SaveCommonGlobalSettingsToBase(_commonGlobalSettings);
				}

				SetLbAgentText("Проверка...");
				#region Agent
				var localAgentFileExistsRes = FtpWorks.CheckFtpFileExists(_commonGlobalSettings.SelfUpdateFtpServer, _commonGlobalSettings.SelfUpdateFtpUser, _commonGlobalSettings.SelfUpdateFtpPassword, _commonGlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.MAGIC_UPDATER_EXE);
				if (!localAgentFileExistsRes.IsComplete)
				{
					SetLbAgentText("Ошибка получения локальной версии агента");
					MLogger.Error(localAgentFileExistsRes.Message);
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}

				TryGetFtpFileVersion localAgentVersionRes;

				if (localAgentFileExistsRes.IsFileExists)
				{
					localAgentVersionRes = FtpWorks.GetFtpFileVersion(_commonGlobalSettings.SelfUpdateFtpServer, _commonGlobalSettings.SelfUpdateFtpUser, _commonGlobalSettings.SelfUpdateFtpPassword, _commonGlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.MAGIC_UPDATER_EXE);
					if (!localAgentVersionRes.IsComplete)
					{
						SetLbAgentText("Ошибка получения локальной версии агента");
						MLogger.Error(localAgentVersionRes.Message);
						//lbAgent.Text = "Ошибка получения локальной версии";
						StopAgentProgressBarsMarquee();
						StopMonitorProgressBarsMarquee();
						return;
					}
				}
				else
				{
					localAgentVersionRes = new TryGetFtpFileVersion();
				}

				//var remoteVersionRes = FtpWorks.GetFtpFileVersion("sartre.timeweb.ru", "dublerin_mulic", "8CFeyXB3", "Agent", "MagicUpdater.exe");
				var remoteAgentVersionRes = FtpWorks.GetFtpFileVersion(_server, _login, _password, AGENT_UPDATE_SERVER_FOLDER, MainSettings.Constants.MAGIC_UPDATER_EXE);
				if (!remoteAgentVersionRes.IsComplete)
				{
					SetLbAgentText("Ошибка получения версии агента с сервера обновлений");
					MLogger.Error(remoteAgentVersionRes.Message);
					//lbAgent.Text = "Ошибка получения версии с сервера обновлений";
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}
				#endregion

				SetLbMonitorText("Проверка...");
				#region Monitor

				TryGetFileVersion localMonitorVersionRes;
				if (File.Exists(Path.Combine(_commonGlobalSettings.MonitorSelfUpdatePath, MainSettings.Constants.MAGIC_UPDATER_MONITOR_EXE)))
				{
					localMonitorVersionRes = FilesWorks.GetFileVersion(Path.Combine(_commonGlobalSettings.MonitorSelfUpdatePath, MainSettings.Constants.MAGIC_UPDATER_MONITOR_EXE));
					if (!localMonitorVersionRes.IsComplete)
					{
						SetLbMonitorText("Ошибка получения локальной версии монитора");
						MLogger.Error(localMonitorVersionRes.Message);
						StopAgentProgressBarsMarquee();
						StopMonitorProgressBarsMarquee();
						return;
					}
				}
				else
				{
					localMonitorVersionRes = new TryGetFileVersion();
				}

				var remoteMonitorVersionRes = FtpWorks.GetFtpFileVersion(_server, _login, _password, MONITOR_UPDATE_SERVER_FOLDER, MainSettings.Constants.MAGIC_UPDATER_MONITOR_EXE);
				if (!remoteMonitorVersionRes.IsComplete)
				{
					SetLbMonitorText("Ошибка получения версии монитор с сервера обновлений");
					MLogger.Error(remoteMonitorVersionRes.Message);
					StopAgentProgressBarsMarquee();
					StopMonitorProgressBarsMarquee();
					return;
				}
				#endregion

				switch (remoteAgentVersionRes.Ver.CompareTo(localAgentVersionRes.Ver))
				{
					case 0:
						//обновлений нет
						SetLbAgentText("Вы используете самую последнюю версию агента");
						//lbAgent.Text = "Вы используете самую последнюю версию агента";
						SetBtnDownloadUpdateAgentEnabled(false);
						break;
					case 1:
						SetLbAgentText("Есть обновление для агента");
						//lbAgent.Text = "Есть обновление для агента";
						SetBtnDownloadUpdateAgentEnabled(true);
						//обновления есть
						break;
					case -1:
						//обновлений нет
						SetLbAgentText("Вы используете самую последнюю версию агента");
						//lbAgent.Text = "Вы используете самую последнюю версию агента";
						SetBtnDownloadUpdateAgentEnabled(false);
						break;
				}

				switch (remoteMonitorVersionRes.Ver.CompareTo(localMonitorVersionRes.Ver))
				{
					case 0:
						//обновлений нет
						SetLbMonitorText("Вы используете самую последнюю версию монитора");
						//lbAgent.Text = "Вы используете самую последнюю версию агента";
						SetBtnDownloadUpdateMonitorEnabled(false);
						break;
					case 1:
						SetLbMonitorText("Есть обновление для монитора");
						//lbAgent.Text = "Есть обновление для агента";
						SetBtnDownloadUpdateMonitorEnabled(true);
						//обновления есть
						break;
					case -1:
						//обновлений нет
						SetLbMonitorText("Вы используете самую последнюю версию монитора");
						//lbAgent.Text = "Вы используете самую последнюю версию агента";
						SetBtnDownloadUpdateMonitorEnabled(false);
						break;
				}

				StopAgentProgressBarsMarquee();
				StopMonitorProgressBarsMarquee();
			});


			//FtpWorks.GetFtpFileVersion("sartre.timeweb.ru", "dublerin_mulic", "8CFeyXB3", "Agent", "MagicUpdater.exe");
			//FtpWorks.DeleteFileFromFtp("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest/Test", "MagicUpdater.exe");
			//FtpWorks.DeleteFilesFromFtpFolder("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest/Test");
			//FtpWorks.UploadFileToFtp(@"D:\", "mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", "Services-50.png");

			//FtpWorks.UploadFilesFromFolderToFtp("mskftp.sela.ru", "cis_obmen", "cisobmen836", "MagicUpdaterTest", @"D:\111");
		}

		private async void btnDownloadUpdateAgent_Click(object sender, EventArgs e)
		{
			SetBtnDownloadUpdateAgentEnabled(false);
			StartAgentProgressBarsMarquee();
			SetLbAgentText("Закачка новой версии агента...");
			try
			{
				await Task.Factory.StartNew(() =>
				{
					string tempDir = Path.Combine(Path.GetTempPath(), AGENT_UPDATE_SERVER_FOLDER);
					if (!Directory.Exists(tempDir))
					{
						Directory.CreateDirectory(tempDir);
					}
					else
					{
						FilesWorks.DeleteDirectoryFull(tempDir);
						Directory.CreateDirectory(tempDir);
					}

					string tempDirRestart = Path.Combine(Path.GetTempPath(), AGENT_UPDATE_SERVER_FOLDER, MainSettings.Constants.MU_RESTART_FOLDER_NAME);
					if (!Directory.Exists(tempDirRestart))
					{
						Directory.CreateDirectory(tempDirRestart);
					}

					var downloadRestartRes = FtpWorks.DownloadFilesFromFtpFolder(_server, _login, _password, Path.Combine(AGENT_UPDATE_SERVER_FOLDER, MainSettings.Constants.MU_RESTART_FOLDER_NAME), tempDirRestart);
					if (!downloadRestartRes.IsComplete)
					{
						SetLbAgentText("Ошибка скачивания новой версии [restart]");
						MLogger.Error(downloadRestartRes.Message);
						return;
					}

					if (Directory.Exists(tempDirRestart) && Directory.GetFiles(tempDirRestart).Count() > 0)
					{
						var createRestartFtpFolderRes = FtpWorks.CreateFtpFolder(_commonGlobalSettings.SelfUpdateFtpServer
																				, _commonGlobalSettings.SelfUpdateFtpUser
																				, _commonGlobalSettings.SelfUpdateFtpPassword
																				, Path.Combine(_commonGlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME));
						if (!createRestartFtpFolderRes.IsComplete)
						{
							SetLbAgentText("Ошибка создания папки [restart]");
							MLogger.Error(createRestartFtpFolderRes.Message);
							return;
						}

						var uploadRestartRes = FtpWorks.UploadFilesFromFolderToFtp(_commonGlobalSettings.SelfUpdateFtpServer
																				, _commonGlobalSettings.SelfUpdateFtpUser
																				, _commonGlobalSettings.SelfUpdateFtpPassword
																				, Path.Combine(_commonGlobalSettings.SelfUpdateFtpPath, MainSettings.Constants.MU_RESTART_FOLDER_NAME)
																				, tempDirRestart);
						if (!uploadRestartRes.IsComplete)
						{
							SetLbAgentText("Ошибка закачки новой версии [restart]");
							MLogger.Error(uploadRestartRes.Message);
							return;
						}
					}
					else
					{
						SetLbAgentText("Отсутствуют файлы во временной папке [restart]");
						return;
					}

					var downloadAgentRes = FtpWorks.DownloadFilesFromFtpFolder(_server, _login, _password, AGENT_UPDATE_SERVER_FOLDER, tempDir);
					if (!downloadAgentRes.IsComplete)
					{
						SetLbAgentText("Ошибка скачивания новой версии агента");
						MLogger.Error(downloadAgentRes.Message);
						return;
					}

					if (Directory.Exists(tempDir) && Directory.GetFiles(tempDir).Count() > 0)
					{
						var uploadAgentRes = FtpWorks.UploadFilesFromFolderToFtp(_commonGlobalSettings.SelfUpdateFtpServer
																				, _commonGlobalSettings.SelfUpdateFtpUser
																				, _commonGlobalSettings.SelfUpdateFtpPassword
																				, _commonGlobalSettings.SelfUpdateFtpPath
																				, tempDir);
						if (!uploadAgentRes.IsComplete)
						{
							SetLbAgentText("Ошибка закачки новой версии агента");
							MLogger.Error(uploadAgentRes.Message);
							return;
						}
					}
					else
					{
						SetLbAgentText("Отсутствуют файлы во временной папке");
						return;
					}

					FilesWorks.DeleteDirectoryFull(tempDir);
					SetLbAgentText("Вы используете самую последнюю версию агента");
				});
			}
			finally
			{
				StopAgentProgressBarsMarquee();
				SetBtnDownloadUpdateAgentEnabled(false);
			}
		}

		private async void btnDownloadUpdateMonitor_Click(object sender, EventArgs e)
		{
			SetBtnDownloadUpdateMonitorEnabled(false);
			StartMonitorProgressBarsMarquee();
			SetLbMonitorText("Закачка новой версии монитора...");

			try
			{
				await Task.Factory.StartNew(() =>
				{
					string tempDir = Path.Combine(Path.GetTempPath(), MONITOR_UPDATE_SERVER_FOLDER);
					if (!Directory.Exists(tempDir))
					{
						Directory.CreateDirectory(tempDir);
					}
					else
					{
						FilesWorks.DeleteDirectoryFull(tempDir);
						Directory.CreateDirectory(tempDir);
					}

					var downloadAgentRes = FtpWorks.DownloadFilesFromFtpFolder(_server, _login, _password, MONITOR_UPDATE_SERVER_FOLDER, tempDir);
					if (!downloadAgentRes.IsComplete)
					{
						SetLbMonitorText("Ошибка скачивания новой версии монитора");
						MLogger.Error(downloadAgentRes.Message);
						return;
					}

					if (Directory.Exists(tempDir) && Directory.GetFiles(tempDir).Count() > 0)
					{
						try
						{
							foreach (var filePath in Directory.GetFiles(tempDir))
							{
								File.Copy(filePath, Path.Combine(_commonGlobalSettings.MonitorSelfUpdatePath, Path.GetFileName(filePath)), true);
							}
						}
						catch (Exception ex)
						{
							SetLbMonitorText("Ошибка закачки новой версии монитора");
							MLogger.Error(ex.ToString());
							return;
						}
					}
					else
					{
						SetLbMonitorText("Отсутствуют файлы во временной папке");
						return;
					}

					FilesWorks.DeleteDirectoryFull(tempDir);
					SetLbMonitorText("Вы используете самую последнюю версию монитора");
				});
			}
			finally
			{
				StopMonitorProgressBarsMarquee();
				SetBtnDownloadUpdateMonitorEnabled(false);
			}
		}
	}
}
