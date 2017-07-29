using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MagicUpdaterCommon.Helpers
{
	public static class LatestVersion1C
	{
		public static Version Version1C { get; private set; } = null;
		/// <summary>
		/// Перед чтением свойств обязательно вызвать Refresh() с проверкой!
		/// </summary>
		public static string ExePath1C { get; private set; } = null;

		public static bool Refresh()
		{
			bool result = false;

			DriveInfo[] drives = DriveInfo.GetDrives();

			string targetDrive = null;
			string programFiles = null;
			string folder1C = null;
			string exeName = null;

			foreach (DriveInfo drive in drives)
			{
				if (Directory.Exists(Path.Combine(drive.Name, "Program Files (x86)")))
					programFiles = "Program Files (x86)";
				else if (Directory.Exists(Path.Combine(drive.Name, "Program Files")))
					programFiles = "Program Files";

				Dictionary<Version, string> versions = new Dictionary<Version, string>();

				if (!string.IsNullOrEmpty(programFiles))
				{
					targetDrive = drive.Name;
					string vers;

					for (int i = 80; i <= 99; i++)
					{
						if (i % 10 == 0)
							vers = ((int)(i / 10)).ToString();
						else
							vers = i.ToString();

						if (Directory.Exists(Path.Combine(targetDrive, programFiles, $"1cv{vers}")))
						{
							exeName = $"1cv{vers[0]}";

							foreach (string directory in Directory.GetDirectories(Path.Combine(targetDrive, programFiles, $"1cv{vers}")))
							{
								Version version;
								if (Version.TryParse(Path.GetFileName(directory), out version))
									versions.Add(version, directory);
							}
						}
					}
				}

				if (versions.Count > 0)
				{
					var list = versions.Keys.ToList();
					list.Sort();
					var key = list[list.Count - 1];
					Version1C = key;
					folder1C = versions[key];
				}
				else
					return false;

				if (!string.IsNullOrEmpty(folder1C) &&
					!string.IsNullOrEmpty(exeName))
				{
					ExePath1C = Path.Combine(folder1C, "bin", $"{exeName}.exe");
					result = true;
					break;
				}
			}
			return result;
		}
	}
}
