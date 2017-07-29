using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterMonitor.Common;
using System;

namespace MagicUpdaterMonitor.Abstract
{
	//public enum OperationsType
	//{
	//	SetOperationsListCheckTimeout = 0,
	//	SelfUpdate = 1,
	//	DynamicUpdate1C = 2,
	//	StaticUpdate1C = 3,
	//	CacheClear1C = 4,
	//	ServerRestart1C = 5,
	//	ForceStaticUpdate1C = 6,

	//	//Сервисные операции
	//	SetLanMacToDB_Service = 1000,
	//	RegisterViaMac_Service = 1001,
	//	SetExternalIp_Service = 1002,
	//	RestartMagicUpdater_Service = 1003
	//}

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
			//await MQueryCommand.TryInsertNewOperationAsync(computerId, _operationType, poolDate);
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
			//var res = MQueryCommand.TrySaveOperationAttributes(_operationType, model);
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
			//return MQueryCommand.GetSavedOperationAttributes(_operationType);
			return MQueryCommand.GetSavedOperationAttributesByUser(_operationType, MainForm.UserId);
		}

		//public void SendForAllComputers()
		//{
		//	if (IsOnlyMainCashbox)
		//	{
		//		foreach (ShopComputer sc in AppSettings.ShopComputers)
		//		{
		//			if (sc.IsMainCashbox)
		//				SendForComputer(sc.ComputerId);
		//		}
		//	}
		//	else
		//	{
		//		foreach (ShopComputer sc in AppSettings.ShopComputers)
		//		{
		//			SendForComputer(sc.ComputerId);
		//		}
		//	}
		//}
	}
}
