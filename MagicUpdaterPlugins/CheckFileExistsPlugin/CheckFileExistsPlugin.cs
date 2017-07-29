using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace CheckFileExistsPlugin
{
	public class CheckFileExistsPlugin : OperationWithAttr<FilesAttr>
	{
		public CheckFileExistsPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			if (File.Exists(Attributes.FilePath))
			{
				AddCompleteMessage($"Файл {Attributes.FilePath} - присутствует");
			}
			else
			{
				AddErrorMessage($"Файл {Attributes.FilePath} - отсутствует");
			}
		}
	}

	public class FilesAttr : IOperationAttributes
	{
		[OperationAttributeDisplayName("Путь к файлу")]
		public string FilePath { get; set; } = "";
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Проверка существования файла";
	}
}
