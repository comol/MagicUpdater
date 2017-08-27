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

				string solutionFileText = File.ReadAllText(tbSolutionFile.Text);
				string startMarker = "GlobalSection(SolutionConfigurationPlatforms) = preSolution";
				string globalSection = solutionFileText.Substring(solutionFileText.IndexOf(startMarker) + startMarker.Length, solutionFileText.IndexOf("EndGlobalSection") - solutionFileText.IndexOf(startMarker) - startMarker.Length).Trim();
				globalSection = globalSection.Replace("|Any CPU", "").Replace("\r\n\t\t", "|");
				string[] configurations = globalSection.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < configurations.Length; i++)
				{
					configurations[i] = configurations[i].Substring(0, configurations[i].IndexOf("=") - 1).Trim();
				}

				cbConfiguration.Items.AddRange(configurations);
				if (cbConfiguration.Items.Count > 0)
				{
					cbConfiguration.SelectedIndex = 0;
				}
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
			#region Checks
			if (string.IsNullOrEmpty(tbSolutionFile.Text))
			{
				MessageBox.Show("Пустое имя файла решения");
				return;
			}
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
			#endregion

			string solutionPath = Path.GetDirectoryName(tbSolutionFile.Text);

			if (chbAgent.Checked)
			{
				string assemblyAgentDir = Path.Combine(solutionPath, MAGIC_UPDATER, "bin", cbConfiguration.Text);
				string[] filesAgent = Directory.GetFiles(assemblyAgentDir);
				string destinationAgentDir = Path.Combine(tbSaveFolder.Text, "Agent");
				if (!Directory.Exists(destinationAgentDir))
				{
					Directory.CreateDirectory(destinationAgentDir);
				}
				CopyNotObfuscatedFilesFiles(filesAgent, destinationAgentDir);
				BuildSmartAssembly(filesAgent, destinationAgentDir);

				string assemblyRestartDir = Path.Combine(solutionPath, MAGIC_UPDATER_RESTART, "bin", cbConfiguration.Text);
				string[] filesRestart = Directory.GetFiles(assemblyRestartDir);
				string destinationRestartDir = Path.Combine(destinationAgentDir, "restart");
				if (!Directory.Exists(destinationRestartDir))
				{
					Directory.CreateDirectory(destinationRestartDir);
				}
				CopyNotObfuscatedFilesFiles(filesRestart, destinationRestartDir);
				BuildSmartAssembly(filesRestart, destinationRestartDir);
			}

			if (chbAgentInstaller.Checked)
			{

			}

			if (chbMonitor.Checked)
			{

			}

			if (chbScheduler.Checked)
			{

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
				Process processCreate = Process.Start(SMART_ASSEMBLY_PATH, string.Join(" ", smartAssemblyParams));
				processCreate.WaitForExit();
				Thread.Sleep(1000);
				if (File.Exists(SMART_ASSEMBLY_PROJECT))
				{
					Process processBuild = Process.Start(SMART_ASSEMBLY_PATH, $"/build {SMART_ASSEMBLY_PROJECT}");
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
				}
			}

			return new TryBuildSmartAssembly();
		}
	}

	public class TryBuildSmartAssembly : TryResult
	{
		public TryBuildSmartAssembly(bool isComplete = true, string message = "") : base(isComplete, message)
		{
		}
	}
}
