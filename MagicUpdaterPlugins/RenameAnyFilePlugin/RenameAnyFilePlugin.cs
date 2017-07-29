using MagicUpdaterCommon.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace RenameAnyFilePlugin
{
	public class RenameAnyFilePlugin : OperationWithAttr<RenameAnyFilesAttr>
	{
		public RenameAnyFilePlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			File.Move(Path.GetFullPath(Attributes.SourcePath), Path.GetFullPath(Attributes.DestinationPath));

			if (File.Exists(Path.GetFullPath(Attributes.DestinationPath))&&!File.Exists(Path.GetFullPath(Attributes.SourcePath)))
			{
				AddCompleteMessage("Файл переименован");
			}
			else
			{
				AddErrorMessage("Не удалось переименовать файл");
			}
		}
	}

	public class RenameAnyFilesAttr : IOperationAttributes
	{
		[OperationAttributeDisplayName("Исходный путь")]
		public string SourcePath { get; set; } = @"";
		[OperationAttributeDisplayName("Конечный путь")]
		public string DestinationPath { get; set; } = @"";
	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => "";

		public int GroupId => 2;

		public string NameRus => "Переименование произвольного файла";
	}
}
