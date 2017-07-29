using MagicUpdater.DL.DB;
using MagicUpdater.DL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicUpdater.DL.Tools
{
	public static class OperationTools
	{
		private static OperationTypeModel[] _operationTypes;
		static OperationTools()
		{
			if (_operationTypes == null)
			{
				_operationTypes = MQueryCommand.GetOperationTypes();
			}
		}

		public static void Update(OperationTypeModel[] operationTypeModel)
		{
			_operationTypes = operationTypeModel;
		}

		public static string GetOperationNameEnById(int id)
		{
			return OperationTypes.Where(w => w.Id == id).Select(s => s.Name).First();
		}

		public static string GetOperationNameRuById(int id)
		{
			return OperationTypes.Where(w => w.Id == id).Select(s => s.NameRus).First();
		}

		public static string TryGetOperationNameRuById(int id)
		{
			string nameRu = OperationTypes.Where(w => w.Id == id).Select(s => s.NameRus).First();
			if (string.IsNullOrEmpty(nameRu))
			{
				return OperationTypes.Where(w => w.Id == id).Select(s => s.Name).First();
			}
			else
			{
				return nameRu;
			}
		}

		public static string TryGetOperationNameRuByEn(string nameEn)
		{
			string nameRu = OperationTypes.Where(w => w.Name == nameEn).Select(s => s.NameRus).First();
			if (string.IsNullOrEmpty(nameRu))
			{
				return nameEn;
			}
			else
			{
				return nameRu;
			}
		}

		public static string GetOperationFileNameById(int id)
		{
			return OperationTypes.Where(w => w.Id == id).Select(s => s.FileName).FirstOrDefault();
		}

		public static string GetOperationFileMd5ById(int id)
		{
			return OperationTypes.Where(w => w.Id == id).Select(s => s.FileMd5).First();
		}

		public static int GetOperationIdByName(string name)
		{
			return OperationTypes.Where(w => w.Name == name).Select(s => s.Id).First();
		}

		public static OperationTypeModel[] OperationTypes => _operationTypes;
	}
}
