using MagicUpdateObfuscationBuilder.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace MagicUpdateObfuscationBuilder
{
	public partial class MainForm : Form
	{
		private const string SMART_ASSEMBLY_PATH = @"C:\Program Files\Red Gate\SmartAssembly 6\SmartAssembly.com";
		private const string SMART_ASSEMBLY_PROJECT = @"MagicUpdaterObfuscation.saproj";
		private const string ASSEMBLY_FOR_OBFUSCATE_PATH = "D:\\GitProjects\\MagicUpdater_ms\\MagicUpdater\\bin\\Release\\MagicUpdaterCommon.dll";

		private const string MAGIC_UPDATER = "MagicUpdater";
		private const string MAGIC_UPDATER_INSTALLER = "MagicUpdaterInstaller";
		private const string MAGIC_UPDATER_MONITOR = "MagicUpdaterMonitor";
		private const string MAGIC_UPDATER_SHEDULER = "MagicUpdaterSheduler";
		private const string MAGIC_UPDATER_RESTART = "MagicUpdaterRestart";

		//Названия папок сборки
		private const string AGENT = "Agent";
		private const string AGENT_INSTALLER = "AgentInstaller";
		private const string MONITOR = "Monitor";
		private const string SCHEDULER = "Scheduler";

		//Параметры проекта SmartAssembly
		private static string[] _arguments =
		{
			$"/create {SMART_ASSEMBLY_PROJECT}",
			$"/input=\"{ASSEMBLY_FOR_OBFUSCATE_PATH}\"",
			$"/output=\"Obfuscated\\MagicUpdaterCommon.dll\"",
			$"/typemethodobfuscation=3",
			$"/fieldobfuscation=3",
			$"/assembly=\"{Path.GetFileName(ASSEMBLY_FOR_OBFUSCATE_PATH).Replace(Path.GetExtension(Path.GetFileName(ASSEMBLY_FOR_OBFUSCATE_PATH)), "")}\";nameobfuscate:true,controlflowobfuscate:3,dynamicproxy:true",
			$"/stringsencoding=true;improved:true,compressencrypt:true,cache:true",
			$"/preventildasm=true",
		};

		//Файлы для обфускации
		private static string[] _filesForObfuscation =
		{
			"Communications.dll",
			"MagicUpdater.DL.dll",
			"MagicUpdater.exe",
			"MagicUpdaterCommon.dll",
			"MagicUpdaterInstaller.exe",
			"MagicUpdaterMonitor.exe",
			"MagicUpdaterRestart.exe",
			"MagicUpdaterSheduler.exe",
		};

		//Исключения для копирования файлов проектов
		private static string[] _copyFilesExtensionExceptions =
		{
			".pdb",
			".xml",
			".json",
		};

		public MainForm()
		{
			InitializeComponent();
		}

		private void btnOpenSolutionFile_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				tbSolutionFile.Text = openFileDialog1.FileName;
				if (Path.GetExtension(tbSolutionFile.Text) != ".sln")
				{
					MessageBox.Show($"Файл: \"{tbSolutionFile.Text}\" не является решением Ms VisualStudio");
					tbSolutionFile.Text = "";
					return;
				}

				LoadConfigurations();
			}
		}

		private void tbSolutionFile_Leave(object sender, EventArgs e)
		{
			LoadConfigurations();
		}

		private void LoadConfigurations()
		{
			string solutionFileText = File.ReadAllText(tbSolutionFile.Text);
			string startMarker = "GlobalSection(SolutionConfigurationPlatforms) = preSolution";
			string globalSection = solutionFileText.Substring(solutionFileText.IndexOf(startMarker) + startMarker.Length, solutionFileText.IndexOf("EndGlobalSection") - solutionFileText.IndexOf(startMarker) - startMarker.Length).Trim();
			globalSection = globalSection.Replace("|Any CPU", "").Replace("\r\n\t\t", "|");
			string[] configurations = globalSection.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < configurations.Length; i++)
			{
				configurations[i] = configurations[i].Substring(0, configurations[i].IndexOf("=") - 1).Trim();
			}

			cbConfiguration.Items.Clear();
			cbConfiguration.Items.AddRange(configurations);
			if (cbConfiguration.Items.Count > 0)
			{
				cbConfiguration.SelectedIndex = 0;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
			{
				tbSaveFolder.Text = folderBrowserDialog1.SelectedPath;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btnBuild_Click(object sender, EventArgs e)
		{
			chbAgent.Enabled =
			chbAgentInstaller.Enabled =
			chbMonitor.Enabled =
			chbScheduler.Enabled =
			tbSaveFolder.Enabled =
			tbSolutionFile.Enabled =
			cbConfiguration.Enabled =
			btnBuild.Enabled =
			btnCancel.Enabled =
			btnOpenSolutionFile.Enabled =
			btnChooseSaveFolder.Enabled = false;
			try
			{
				if (string.IsNullOrEmpty(tbSolutionFile.Text))
				{
					MessageBox.Show("Пустое имя файла решения");
					return;
				}

				string solutionPath = Path.GetDirectoryName(tbSolutionFile.Text);
				string assemblyAgentDir = Path.Combine(solutionPath, MAGIC_UPDATER, "bin", cbConfiguration.Text);
				string assemblyRestartDir = Path.Combine(solutionPath, MAGIC_UPDATER_RESTART, "bin", cbConfiguration.Text);
				string assemblyAgentInstallerDir = Path.Combine(solutionPath, MAGIC_UPDATER_INSTALLER, "bin", cbConfiguration.Text);
				string assemblyMonitorDir = Path.Combine(solutionPath, MAGIC_UPDATER_MONITOR, "bin", cbConfiguration.Text);
				string assemblySchedulerDir = Path.Combine(solutionPath, MAGIC_UPDATER_SHEDULER, "bin", cbConfiguration.Text);

				#region Checks

				if (string.IsNullOrEmpty(tbSaveFolder.Text))
				{
					MessageBox.Show("Не указана папка сохранения");
					return;
				}
				if (Path.GetExtension(tbSolutionFile.Text) != ".sln")
				{
					MessageBox.Show("Неверный фал решения. Файл должен иметь расширение (.sln)");
					return;
				}
				if (!Directory.Exists(assemblyAgentDir))
				{
					MessageBox.Show($"Отсутствует сборка конфигурации по пути: {assemblyAgentDir}");
					return;
				}
				if (!Directory.Exists(assemblyRestartDir))
				{
					MessageBox.Show($"Отсутствует сборка конфигурации по пути: {assemblyRestartDir}");
					return;
				}
				if (!Directory.Exists(assemblyAgentInstallerDir))
				{
					MessageBox.Show($"Отсутствует сборка конфигурации по пути: {assemblyAgentInstallerDir}");
					return;
				}
				if (!Directory.Exists(assemblyMonitorDir))
				{
					MessageBox.Show($"Отсутствует сборка конфигурации по пути: {assemblyMonitorDir}");
					return;
				}
				if (!Directory.Exists(assemblySchedulerDir))
				{
					MessageBox.Show($"Отсутствует сборка конфигурации по пути: {assemblySchedulerDir}");
					return;
				}
				#endregion

				if (chbAgent.Checked)
				{
					string[] filesAgent = Directory.GetFiles(assemblyAgentDir);
					string destinationAgentDir = Path.Combine(tbSaveFolder.Text, AGENT);
					if (!Directory.Exists(destinationAgentDir))
					{
						Directory.CreateDirectory(destinationAgentDir);
					}
					PrintLog("Agent. Копирование файлов без обфускации...");
					CopyNotObfuscatedFilesFiles(filesAgent, destinationAgentDir);
					PrintLog("Agent. Копирование файлов с обфускацией:");
					var resAgent = BuildSmartAssembly(filesAgent, destinationAgentDir);
					if (!resAgent.IsComplete)
					{
						PrintLog($"Ошибка: {resAgent.Message}");
						return;
					}
					PrintLine();

					string[] filesRestart = Directory.GetFiles(assemblyRestartDir);
					string destinationRestartDir = Path.Combine(destinationAgentDir, "restart");
					if (!Directory.Exists(destinationRestartDir))
					{
						Directory.CreateDirectory(destinationRestartDir);
					}
					PrintLog("Restart for Agent. Копирование файлов без обфускации...");
					CopyNotObfuscatedFilesFiles(filesRestart, destinationRestartDir);
					PrintLog("Restart for Agent. Копирование файлов с обфускацией:");
					var resRestart = BuildSmartAssembly(filesRestart, destinationRestartDir);
					if (!resRestart.IsComplete)
					{
						PrintLog($"Ошибка: {resRestart.Message}");
						return;
					}
					PrintLine();
				}

				if (chbAgentInstaller.Checked)
				{
					string[] filesAgentInstaller = Directory.GetFiles(assemblyAgentInstallerDir);
					string destinationAgentInstallerDir = Path.Combine(tbSaveFolder.Text, AGENT_INSTALLER);
					if (!Directory.Exists(destinationAgentInstallerDir))
					{
						Directory.CreateDirectory(destinationAgentInstallerDir);
					}
					PrintLog("AgentInstaller. Копирование файлов без обфускации...");
					CopyNotObfuscatedFilesFiles(filesAgentInstaller, destinationAgentInstallerDir);
					PrintLog("AgentInstaller. Копирование файлов с обфускацией:");
					var res = BuildSmartAssembly(filesAgentInstaller, destinationAgentInstallerDir);
					if (!res.IsComplete)
					{
						PrintLog($"Ошибка: {res.Message}");
						return;
					}
					PrintLine();
				}

				if (chbMonitor.Checked)
				{
					string[] filesMonitor = Directory.GetFiles(assemblyMonitorDir);
					string destinationMonitorDir = Path.Combine(tbSaveFolder.Text, MONITOR);
					if (!Directory.Exists(destinationMonitorDir))
					{
						Directory.CreateDirectory(destinationMonitorDir);
					}
					PrintLog("Monitor. Копирование файлов без обфускации...");
					CopyNotObfuscatedFilesFiles(filesMonitor, destinationMonitorDir);
					PrintLog("Monitor. Копирование файлов с обфускацией:");
					var res = BuildSmartAssembly(filesMonitor, destinationMonitorDir);
					if (!res.IsComplete)
					{
						PrintLog($"Ошибка: {res.Message}");
						return;
					}
					PrintLine();
				}

				if (chbScheduler.Checked)
				{
					string[] filesScheduler = Directory.GetFiles(assemblySchedulerDir);
					string destinationSchedulerDir = Path.Combine(tbSaveFolder.Text, SCHEDULER);
					if (!Directory.Exists(destinationSchedulerDir))
					{
						Directory.CreateDirectory(destinationSchedulerDir);
					}
					PrintLog("Scheduler. Копирование файлов без обфускации...");
					CopyNotObfuscatedFilesFiles(filesScheduler, destinationSchedulerDir);
					PrintLog("Scheduler. Копирование файлов с обфускацией:");
					var res = BuildSmartAssembly(filesScheduler, destinationSchedulerDir);
					if (!res.IsComplete)
					{
						PrintLog($"Ошибка: {res.Message}");
						return;
					}
					PrintLine();
				}
			}
			finally
			{
				chbAgent.Enabled =
				chbAgentInstaller.Enabled =
				chbMonitor.Enabled =
				chbScheduler.Enabled =
				tbSaveFolder.Enabled =
				tbSolutionFile.Enabled =
				cbConfiguration.Enabled =
				btnBuild.Enabled =
				btnCancel.Enabled =
				btnOpenSolutionFile.Enabled =
				btnChooseSaveFolder.Enabled = true;
			}
		}

		private void CopyNotObfuscatedFilesFiles(string[] files, string destinationDir)
		{
			foreach (var filePath in files)
			{
				if (!_copyFilesExtensionExceptions.Contains(Path.GetExtension(filePath)))
				{
					if (!_filesForObfuscation.Contains(Path.GetFileName(filePath)))
					{
						File.Copy(filePath, Path.Combine(destinationDir, Path.GetFileName(filePath)), true);
					}
				}
			}
		}

		private string[] CreateSmartAssemblyParams(string inputPath, string outputPath)
		{
			string[] arguments =
			{
				$"/create {SMART_ASSEMBLY_PROJECT}",
				$"/input=\"{inputPath}\"",
				$"/output=\"{outputPath}\"",
				$"/typemethodobfuscation=3",
				$"/fieldobfuscation=3",
				$"/assembly=\"{Path.GetFileName(inputPath).Replace(Path.GetExtension(Path.GetFileName(inputPath)), "")}\";nameobfuscate:true,controlflowobfuscate:3,dynamicproxy:true",
				$"/stringsencoding=true;improved:true,compressencrypt:true,cache:true",
				$"/preventildasm=true",
			};

			return arguments;
		}

		private TryBuildSmartAssembly BuildSmartAssembly(string[] smartAssemblyParams)
		{
			try
			{
				Process processCreate = new Process();
				ProcessStartInfo processCreateStartInfo = new ProcessStartInfo(SMART_ASSEMBLY_PATH, string.Join(" ", smartAssemblyParams));
				processCreateStartInfo.CreateNoWindow = true;
				processCreateStartInfo.RedirectStandardOutput = true;
				processCreateStartInfo.RedirectStandardError = true;
				processCreateStartInfo.UseShellExecute = false;
				processCreate.StartInfo = processCreateStartInfo;
				processCreate.Start();
				processCreate.WaitForExit();
				Thread.Sleep(1000);
				if (File.Exists(SMART_ASSEMBLY_PROJECT))
				{
					Process processBuild = new Process();
					ProcessStartInfo processBuildStartInfo = new ProcessStartInfo(SMART_ASSEMBLY_PATH, $"/build {SMART_ASSEMBLY_PROJECT}");
					processBuildStartInfo.CreateNoWindow = true;
					processBuildStartInfo.RedirectStandardOutput = true;
					processBuildStartInfo.RedirectStandardError = true;
					processBuildStartInfo.UseShellExecute = false;
					processBuild.StartInfo = processBuildStartInfo;
					processBuild.Start();
					processBuild.WaitForExit();
				}
			}
			catch (Exception ex)
			{
				return new TryBuildSmartAssembly(false, ex.ToString());
			}

			return new TryBuildSmartAssembly();
		}

		private TryBuildSmartAssembly BuildSmartAssembly(string[] files, string destinationDir)
		{
			foreach (var filePath in files)
			{
				if (_filesForObfuscation.Contains(Path.GetFileName(filePath)))
				{
					var res = BuildSmartAssembly(CreateSmartAssemblyParams(filePath, Path.Combine(destinationDir, Path.GetFileName(filePath))));
					if (!res.IsComplete)
					{
						return new TryBuildSmartAssembly(false, res.Message);
					}
					PrintLog($"Копирование файлоа с обфускацией [{Path.GetFileName(filePath)}]");
				}
			}

			return new TryBuildSmartAssembly();
		}

		private void PrintLog(string message)
		{
			DateTime dt = DateTime.Now;
			rtbLog.AppendText($"{dt.ToString("HH:mm:ss")}: {message}{Environment.NewLine}");
			// set the current caret position to the end
			rtbLog.SelectionStart = rtbLog.Text.Length;
			// scroll it automatically
			rtbLog.ScrollToCaret();
		}

		private void PrintLine()
		{
			rtbLog.AppendText(Environment.NewLine);
			// set the current caret position to the end
			rtbLog.SelectionStart = rtbLog.Text.Length;
			// scroll it automatically
			rtbLog.ScrollToCaret();
		}


	}

	public class TryBuildSmartAssembly : TryResult
	{
		public TryBuildSmartAssembly(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
}
