using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterMonitor.Common;
using System;

namespace MagicUpdaterMonitor.Abstract
{

	public class OperationBase
	{
		int _operationType;

		private bool _isForOnlyMainCashbox = false;

		public OperationBase(int operationType, bool isForOnlyMainCashbox = false)
		{
			_operationType = operationType;
			_isForOnlyMainCashbox = isForOnlyMainCashbox;
		}

		private async void SendOperToDb(int computerId, DateTime? poolDate)
		{
			await MQueryCommand.TryInsertNewOperationByUserAsync(computerId, _operationType, MainForm.UserId, poolDate);
		}

		public void SendForComputer(int computerId, DateTime? poolDate = null)
		{
			if (_isForOnlyMainCashbox)
			{
				ShopComputer sc = AppSettings.ShopComputers.Find(s => s.ComputerId == computerId);
				if (sc != null && sc.IsMainCashbox)
					SendOperToDb(sc.ComputerId, poolDate);
			}
			else
			{
				SendOperToDb(computerId, poolDate);
			}
		}

		public void SaveAttributes(object model, int userId = 0)
		{
			if(userId == 0)
			{
				userId = MainForm.UserId;
			}
			var res = MQueryCommand.TrySaveOperationAttributesByUser(_operationType, userId, model);
			if (!res.IsComplete)
			{
				MLogger.Error(res.Message);
			}
		}

		public object GetSavedAttributes()
		{
			return MQueryCommand.GetSavedOperationAttributesByUser(_operationType, MainForm.UserId);
		}
	}
}
