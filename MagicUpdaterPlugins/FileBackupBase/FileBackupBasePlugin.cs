using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;

namespace FileBackupBasePlugin
{
	public class FileBackupBasePlugin : OperationWithAttr<RenameAnyFilesAttr>
	{
		public FileBackupBasePlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{

			string BasePath = "";
			BasePath = MagicUpdaterCommon.SettingsTools.MainSettings.LocalSqlSettings.InformationBaseDirectory;
			String BackupPath = (String)this.Attributes.DestinationPath;
			BackupPath.Trim();
			String EndSymbol = BackupPath.Substring(BackupPath.Length - 1, 1);
			if (EndSymbol != "\\")
			{
				BackupPath = BackupPath + "\\";
			}

			String FileName = "Backup_" + DateTime.Now.ToString().Replace(".", "_") + ".zip";
			BackupPath = BackupPath + FileName;

			CompressDirectory(BasePath, BackupPath);			
		}

		static void CompressDirectory(string sInDir, string sOutFile)
		{
			string[] sFiles = Directory.GetFiles(sInDir, "*.*", SearchOption.AllDirectories);
			int iDirLen = sInDir[sInDir.Length - 1] == Path.DirectorySeparatorChar ? sInDir.Length : sInDir.Length + 1;

			using (FileStream outFile = new FileStream(sOutFile, FileMode.Create, FileAccess.Write, FileShare.None))
			using (GZipStream str = new GZipStream(outFile, CompressionMode.Compress))
				foreach (string sFilePath in sFiles)
				{
					string sRelativePath = sFilePath.Substring(iDirLen);
					CompressFile(sInDir, sRelativePath, str);
				}
		}

		static void CompressFile(string sDir, string sRelativePath, GZipStream zipStream)
		{
			//Compress file name
			char[] chars = sRelativePath.ToCharArray();
			zipStream.Write(BitConverter.GetBytes(chars.Length), 0, sizeof(int));
			foreach (char c in chars)
				zipStream.Write(BitConverter.GetBytes(c), 0, sizeof(char));

			//Compress file content
			byte[] bytes = File.ReadAllBytes(Path.Combine(sDir, sRelativePath));
			zipStream.Write(BitConverter.GetBytes(bytes.Length), 0, sizeof(int));
			zipStream.Write(bytes, 0, bytes.Length);
		}
	}



	public class RenameAnyFilesAttr : IOperationAttributes
	{
		[OperationAttributeDisplayName("Каталог резервной копии")]
		public string DestinationPath { get; set; } = @"";
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Резервная копия инфорационной базы";
	}
}
