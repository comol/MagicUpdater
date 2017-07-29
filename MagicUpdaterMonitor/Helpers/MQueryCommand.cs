using System.Threading.Tasks;
using System.Data;
using System.Linq;
using MagicUpdaterMonitor.Models;
using System;
using MagicUpdaterMonitor.Abstract;

namespace MagicUpdaterMonitor.Helpers
{
	public class TryInsert
	{
		public TryInsert(bool isComplete, string message)
		{
			IsComplete = isComplete;
			Message = message;
		}

		public bool IsComplete { get; private set; }
		public string Message { get; private set; }
	}
	public class MQueryCommand
	{
		#region Commands
		public async Task<TryInsert> TryInsertNewOperationAsync(int computerId, OperationsType operationType)
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				try
				{
					context.Operations.Add(new Operation
					{
						ComputerId = computerId,
						OperationType = (int)operationType,
						CreationDate = System.DateTime.UtcNow,
						CreatedUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name
					});

					await context.SaveChangesAsync();

					return new TryInsert(true, null);
				}
				catch (Exception ex)
				{
					return new TryInsert(false, ex.Message);
				}
			}
		}

		public async Task<TryInsert> TryInsertNewShopAsync(string shopId, string shopRegion)
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				if (context.Shops.Any(o => string.Compare(o.ShopId.Trim(), shopId.Trim(), true) == 0))
				{
					return new TryInsert(false, $"Магазин с ShopId = {shopId.Trim()} уже существует");
				}

				try
				{
					context.Shops.Add(new Shop
					{
						ShopId = shopId.Trim(),
						ShopName = shopId.Trim(),
						ShopRegion = shopRegion.Trim()
					});

					await context.SaveChangesAsync();

					return new TryInsert(true, null);
				}
				catch (Exception ex)
				{
					return new TryInsert(false, ex.Message);
				}
			}
		}
		#endregion Commands

		#region Queries
		public async Task<ActionsReportsModel[]> SelectActionsReportsGridAsync(int key)
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				IQueryable<ActionsReportsModel> queryActionsReportsGrid;
				if (key != 0)
				{
					queryActionsReportsGrid = from c in context.ActionsReports
											  where c.OperationId == key
											  select new ActionsReportsModel
											  {
												  ReportId = c.ReportId,
												  OperationId = c.OperationId,
												  ComputerId = c.ComputerId,
												  ActionName = c.ActionName,
												  IsFromLan = c.IsFromLan,
												  IsCompleted = c.IsCompleted,
												  DateCompleteOrError = c.DateCompleteOrError,
												  Result = c.Result
											  };
				}
				else
				{
					queryActionsReportsGrid = from c in context.ActionsReports											  
											  select new ActionsReportsModel
											  {
												  ReportId = c.ReportId,
												  OperationId = c.OperationId,
												  ComputerId = c.ComputerId,
												  ActionName = c.ActionName,
												  IsFromLan = c.IsFromLan,
												  IsCompleted = c.IsCompleted,
												  DateCompleteOrError = c.DateCompleteOrError,
												  Result = c.Result
											  };
				}

				return await queryActionsReportsGrid.ToArrayAsync();
			}
		}

		public async Task<ShopComputersModel[]> SelectComputersGridAsync()
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				var queryComputersGrid = from c in context.ShopComputers
										 select new ShopComputersModel
										 {
											 ShopId = c.ShopId,
											 ComputerId = c.ComputerId,
											 ComputerName = c.ComputerName,
											 Local_IP = c.ComputerAddress,
											 External_IP = c.External_IP,
											 Is1CServer = c.Is1CServer,
											 IsMainCashbox = c.IsMainCashbox,
											 IsTaskerAlive = c.IsTaskerAlive,
											 MagicUpdaterVersion = c.MagicUpdaterVersion,
											 IsOn = c.IsON
										 };

				return await queryComputersGrid.ToArrayAsync();
			}
		}

		public async Task<ViewComputerErrorsLogModel[]> SelectComputerErrorsLogsAsync()
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				DateTime time = DateTime.UtcNow.AddDays(-7);
				var queryComputerErrorsLogGrid = from c in context.ViewComputerErrorsLogs
												 where c.ErrorDate > time
												 select new ViewComputerErrorsLogModel
												 {
													 ComputerId = c.ComputerId,
													 ComputerName = c.ComputerName,
													 ErrorDate = c.ErrorDate,
													 ErrorId = c.ErrorId,
													 ErrorMessage = c.ErrorMessage,
													 External_IP = c.External_IP,
													 Is1CServer = c.Is1CServer,
													 IsMainCashbox = c.IsMainCashbox,
													 IsON = c.IsON,
													 IsTaskerAlive = c.IsTaskerAlive
												 };
				return await queryComputerErrorsLogGrid.ToArrayAsync();
			}
		}

		public async Task<ViewOperationsModel[]> SelectOperationsGridAsync()
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				var queryOperationsGrid = from o in context.ViewOperations
										  select new ViewOperationsModel
										  {
											  ID = o.ID,
											  OperationType = (OperationsType)o.OperationType,
											  CreatedUser = o.CreatedUser,
											  ComputerId = o.ComputerId,
											  ComputerName = o.ComputerName,
											  ShopId = o.ShopId,
											  CreationDate = o.CreationDate,
											  DateRead = o.DateRead,
											  DateCompleteOrError = o.DateCompleteOrError,
											  IsRead = o.IsRead,
											  OperState = o.OperState,
											  IsCompleted = o.IsCompleted,
											  Result = o.Result,
											  LogDate1C = o.LogDate1C,
											  LogMessage1C = o.LogMessage1C,
											  Is1CError = o.Is1CError,
											  IsActionError = o.IsActionError
										  };
				return await queryOperationsGrid.ToArrayAsync();
			}
		}

		public async Task<ShopsModel[]> SelectShopsGridAsync()
		{
			using (MagicUpdaterEntities context = new MagicUpdaterEntities())
			{
				var queryShopsGrid = from s in context.Shops
									 select new ShopsModel
									 {
										 ShopId = s.ShopId,
										 ShopRegion = s.ShopRegion
									 };
				return await queryShopsGrid.ToArrayAsync();
			}
		}
		#endregion Queries
	}
}
