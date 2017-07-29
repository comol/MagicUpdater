using MagicUpdaterCommon.Abstract;
using System;
using System.IO;

namespace MagicUpdater.Actions
{
	class DelCacheFolders1C : OperAction
	{
		public DelCacheFolders1C(int? _operationId) : base(_operationId) { }

		protected override void ActExecution()
		{
			string folder1C = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "1C");
			
			if(Directory.Exists(folder1C))
			{
				string[] directories = Directory.GetDirectories(folder1C);
				foreach(string dir in directories)
				{
					string[] subDirectories = Directory.GetDirectories(dir);

					foreach(string subDir in subDirectories)
					{
						Directory.Delete(subDir, true);
					}
				}
			}
		}
	}
}
