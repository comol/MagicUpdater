using MagicUpdaterCommon.Abstract;
using System.ComponentModel;
using MagicUpdaterCommon.CommonActions;
using System.Diagnostics;
using System;
using System.IO;

namespace CacheClear1CPlugin
{
	public class CacheClear1CPlugin : OperationWithAttr<PluginAttrib>
	{
		public CacheClear1CPlugin(int? operationId, string attrsJson) : base(operationId, attrsJson)
		{
		}

		protected override void Execution(object sender = null, DoWorkEventArgs e = null)
		{
			new KillProcess1CCommon(this.Id).ActRun(); //Грохаем все «1сv8 *.exe»
			DelCacheFolders1C(); //Удаляем папки с кэшем
		}

		private void DelCacheFolders1C()
		{

			string folder1C = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "1C");

			if (Directory.Exists(folder1C))
			{
				string[] directories = Directory.GetDirectories(folder1C);
				foreach (string dir in directories)
				{
					string[] subDirectories = Directory.GetDirectories(dir);

					foreach (string subDir in subDirectories)
					{
						Directory.Delete(subDir, true);
					}
				}
			}

		}
	}

	

	// Класс объявляет аттрибуты операции
	// аттрибуты задаются как параметры в пользовательском режиме
	// но можно указать параметры по умолчанию
	public class PluginAttrib : IOperationAttributes
	{
		//[OperationAttributeDisplayName("Attr1")] // название аттрибута, как оно будет отображаться пользователю
		//public string Attr1 { get; set; } = "Value1"; // значение аттрибута

		//[OperationAttributeDisplayName("Attr2")] //название аттрибута
		//public string Attr2 { get; set; } = "Value2"; //значение аттрибута

	}

	public class RegistrationParams : IRegistrationParams
	{
		public string Description => $"Очистка КЭШ-а 1С"; //Описание операции - будет отображаться в подсказке пользователей

		public int GroupId => 1;  // группа операции - чем меньше тем приоритетнее

		public string NameRus => "Очистка КЭШ-а 1С"; //Название операции как оно будет отображаться пользователю
	}

}
