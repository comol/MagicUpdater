using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Helpers;
using MagicUpdater.DL.Models;
using MagicUpdater.DL.Tools;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Abstract;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Common;
using MagicUpdaterMonitor.Controls;
using MagicUpdaterMonitor.Forms;
using MagicUpdaterMonitor.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;
using MagicUpdaterCommon.Common;
using System.Data.Entity.Core.EntityClient;

namespace MagicUpdaterMonitor
{
	public partial class MainForm : BaseForm
	{
		private const int GRIDS_REFRESH_TIMEOUT = 5000;
		private const int FILITER_BY_AGENT_REFRESH_TIMEOUT = 700;

		private static readonly string _blockedUser1 = @"SELA\service.msk";
		private static readonly string _otrsLink = "http://help.sela.ru/otrs/index.pl?Action=AgentTicketStatusView;Filter=Open;View=;SortBy=Age;OrderBy=Down;ColumnFilterCustomerID";

		private readonly List<string> _blockedUsers = new List<string>
		{
			_blockedUser1
		};

		private static System.Threading.Timer _checkLicTimer;

		/*
		 if (AppSettings.IsServiceMode)
				{
					await rgvComputers.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridFullAsync());
				}
				else
				{
					await rgvComputers.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridAsync());
				}

				var operationTypeModel = await MQueryCommand.GetOperationTypesAsync();
				OperationTools.Update(operationTypeModel);
				await rgvSendOpers.RefreshDataSourceAsync(operationTypeModel);
				await rgvOperations.RefreshDataSourceAsync(await MQueryCommand.SelectOperationsGridAsync());
				await rgvShops.RefreshDataSourceAsync(await MQueryCommand.SelectShopsGridAsync());
				await rgvComputerErrorsLog.RefreshDataSourceAsync(await MQueryCommand.SelectComputerErrorsLogsAsync());
			 */



		private System.Threading.Timer RefreshComputersTimer;
		private System.Threading.Timer RefreshSendOpersTimer;
		private System.Threading.Timer RefreshOperationsTimer;
		private System.Threading.Timer RefreshShopsTimer;
		private System.Threading.Timer RefreshComputerErrorsLogTimer;
		private System.Threading.Timer FilterByAgentTimer;
		private bool _isAllAgentsUpdateFormShowing = false;

		private delegate void FilterDel();

		private DateTime lastOperationDateUtc = DateTime.UtcNow;

		private DateTime LastOperationDateLocal => lastOperationDateUtc.Add(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now));

		public static int UserId { get; set; }
		public static string HwId { get; set; }

		public static CommonGlobalSettings MonitorCommonGlobalSettings { get; private set; }

		public MainForm()
		{
#if LIC
			_checkLicTimer = new System.Threading.Timer(CheckLicTimerCallback, null, 300000, System.Threading.Timeout.Infinite);
#endif
			InitializeComponent();

#if LIC
			tsddbLic.Visible = true;
#else
			tsddbLic.Visible = false;
#endif

			FilterByAgentTimer = new System.Threading.Timer(FilterByAgentTimerCallback, null, Timeout.Infinite, Timeout.Infinite);
			operationAttributes1.OnValueChanged += OperationAttributes1_OnValueChanged;

			//Создаем пользователя для шедулера
			//MQueryCommand.CreateUser(MainSettings.Constants.MU_SHEDULER_USER_LOGIN_GUID, MainSettings.Constants.MU_SHEDULER_USER_NAME);

			MonitorCommonGlobalSettings = new CommonGlobalSettings();
			var loadCommonGlobalSettingsRes = MonitorCommonGlobalSettings.LoadCommonGlobalSettings();
			if (!loadCommonGlobalSettingsRes.IsComplete)
			{
				MessageBox.Show($"Ошибка получения глобальных настроек", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				Application.Exit();
			}

			tabControl1.TabPages.Remove(tabPage2);
#if !DEBUG
			
			//tabControl1.TabPages.Remove(tabPage3);
#endif
			//tsddbSpecialSendOpers.Visible = AppSettings.IsServiceMode;
			//LoadUserSettings();
#if DEMO
			ShowOtrs.Visible = false; 
#endif
		}

		private void CheckLicTimerCallback(object state)
		{
			try
			{
				var res = MQueryCommand.CheckMonitorLic(MainForm.UserId, MainForm.HwId);
				if (!res.IsComplete)
				{
					if (this.InvokeRequired)
					{
						this.Invoke(new MethodInvoker(() =>
						{
							MessageBox.Show(this, $"Ошибка лицензии, приложение завершает работу!{Environment.NewLine}{res.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						}));
					}
					else
					{
						MessageBox.Show(this, $"Ошибка лицензии, приложение завершает работу!{Environment.NewLine}{res.Message}", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					Environment.Exit(0);
				}
			}
			finally
			{
				_checkLicTimer.Change(300000, System.Threading.Timeout.Infinite);
			}
		}

		//Пользовательские настройки
		#region UserSettings
		private void SaveUserSettings()
		{
			try
			{
				Properties.Settings.Default.MainWindowLocation = this.Location;
				Properties.Settings.Default.MainWindowState = this.WindowState;
				Properties.Settings.Default.MainWindowSize = this.Size;
				Properties.Settings.Default.MainWindowStartPosition = this.StartPosition;

				Properties.Settings.Default.scShopsOpersDistance = this.scShopsOpers.SplitterDistance;
				Properties.Settings.Default.scOpersDistance = this.scOpers.SplitterDistance;
				Properties.Settings.Default.scMainDistance = this.scMain.SplitterDistance;
				Properties.Settings.Default.scSendOpersDistance = this.scSendOpers.SplitterDistance;
				Properties.Settings.Default.cbOperationGroups = this.cbOperationGroups.SelectedIndex;
				Properties.Settings.Default.Save();
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());
			}
		}

		private void LoadUserSettings()
		{
			Point location = new Point(this.Location.X, this.Location.Y);
			FormWindowState windowState = this.WindowState;
			FormStartPosition startPosition = this.StartPosition;
			Size size = new Size(this.Size.Width, this.Size.Height);
			int scShopsOpersSplitterDistance = this.scShopsOpers.SplitterDistance;
			int scOpersSplitterDistance = this.scOpers.SplitterDistance;
			int scMainSplitterDistance = this.scMain.SplitterDistance;
			int scSendOpersSplitterDistance = this.scSendOpers.SplitterDistance;

			try
			{
				if (Properties.Settings.Default.MainWindowLocation.X != 0 || Properties.Settings.Default.MainWindowLocation.Y != 0)
					this.Location = Properties.Settings.Default.MainWindowLocation;

				this.WindowState = Properties.Settings.Default.MainWindowState;
				this.StartPosition = Properties.Settings.Default.MainWindowStartPosition;

				if (Properties.Settings.Default.MainWindowSize.Height != 0 && Properties.Settings.Default.MainWindowSize.Width != 0)
					this.Size = Properties.Settings.Default.MainWindowSize;

				if (Properties.Settings.Default.scShopsOpersDistance != 0)
					this.scShopsOpers.SplitterDistance = Properties.Settings.Default.scShopsOpersDistance;

				if (Properties.Settings.Default.scOpersDistance != 0)
					this.scOpers.SplitterDistance = Properties.Settings.Default.scOpersDistance;

				if (Properties.Settings.Default.scMainDistance != 0)
					this.scMain.SplitterDistance = Properties.Settings.Default.scMainDistance;

				if (Properties.Settings.Default.scSendOpersDistance != 0)
					this.scSendOpers.SplitterDistance = Properties.Settings.Default.scSendOpersDistance;

				if (Properties.Settings.Default.cbOperationGroups != -1 && this.cbOperationGroups.Items.Count - 1 >= Properties.Settings.Default.cbOperationGroups)
				{
					this.cbOperationGroups.SelectedIndex = Properties.Settings.Default.cbOperationGroups;
				}
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());

				this.Location = location;
				this.WindowState = windowState;
				this.StartPosition = FormStartPosition.CenterScreen;
				this.Size = size;
				this.scShopsOpers.SplitterDistance = scShopsOpersSplitterDistance;
				this.scOpers.SplitterDistance = scOpersSplitterDistance;
				this.scMain.SplitterDistance = scMainSplitterDistance;
				this.scSendOpers.SplitterDistance = scSendOpersSplitterDistance;
				this.Opacity = 1;

				SaveUserSettings();
			}
		}
		#endregion UserSettings

		public async void LoadForm()
		{
			cbPoolDate.DisplayMember = "PoolDate";
			cbPoolDate.ValueMember = "PoolDate";

			await UpdatePoolDateCb();
#if !DEBUG
			ApplicationLoadingForm applicationLoadingForm = new ApplicationLoadingForm();
			//applicationLoadingForm.StartPosition = this.StartPosition;
			//applicationLoadingForm.WindowState = this.WindowState;
			//applicationLoadingForm.Left = this.Left;
			//applicationLoadingForm.Top = this.Top;
			//applicationLoadingForm.Height = this.Height;
			//applicationLoadingForm.Width = this.Width;
			applicationLoadingForm.TopMost = true;
			applicationLoadingForm.TopMost = false;
			applicationLoadingForm.Show();

			await Task.Run(() => Thread.Sleep(500));

			this.Opacity = 0;

			try
			{
				this.Show();
				const string errMessage = "Ошибка получения версии";

				string version = LastAgentVersionChecker.GetLatestVersion() != null ? LastAgentVersionChecker.GetLatestVersion().ToString() : errMessage;

				new LastAgentVersionChecker(true).VersionRefresh += Form1_VersionRefresh;
				tslLastVersion.Text = $"Последняя версия агента на сервере: {version}";

				UpdateUnregistredFilesListControl();
				await applicationLoadingForm?.ReportProgress(30);
				await InitComputersGridAsync();
				await applicationLoadingForm?.ReportProgress(50);
				ResizeColumnsComputersGrid();
				//await InitShopsGridAsync();
				await applicationLoadingForm?.ReportProgress(100);
				await InitOperationsGridAsync();
				//await applicationLoadingForm?.ReportProgress(100);
				await InitSendOpersGrid();
				//await applicationLoadingForm?.ReportProgress(100);
				//await InitComputerErrorsLogGridAsync();
			}
			finally
			{
				for (int i = 100; i >= 0; i--)
				{
					applicationLoadingForm.Opacity = ((double)i / 100);
					Thread.Sleep(3);
				}

				applicationLoadingForm.Close();

				this.TopMost = true;
				for (int i = 0; i <= 100; i++)
				{
					this.Opacity = ((double)i / 100);
					Thread.Sleep(3);
				}
				this.TopMost = false;
			}
#endif

#if DEBUG
			this.Show();
			const string errMessage = "Ошибка получения версии";

			string version = LastAgentVersionChecker.GetLatestVersion() != null ? LastAgentVersionChecker.GetLatestVersion().ToString() : errMessage;

			new LastAgentVersionChecker(true).VersionRefresh += Form1_VersionRefresh;
			tslLastVersion.Text = $"Последняя версия агента на сервере: {version}";

			UpdateUnregistredFilesListControl();

			await rgvComputers.ShowLoading(true);
			await rgvOperations.ShowLoading(true);
			await rgvSendOpers.ShowLoading(true);

			await InitComputersGridAsync();
			ResizeColumnsComputersGrid();
			//await InitShopsGridAsync();
			await InitOperationsGridAsync();
			await InitSendOpersGrid();
			//await InitComputerErrorsLogGridAsync();
#endif

			RefreshComputersTimer = new System.Threading.Timer(RefreshComputersTimerCallback, null, GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			RefreshSendOpersTimer = new System.Threading.Timer(RefreshSendOpersTimerCallback, null, GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			RefreshOperationsTimer = new System.Threading.Timer(RefreshOperationsTimerCallback, null, GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			//RefreshShopsTimer = new System.Threading.Timer(RefreshShopsTimerCallback, null, GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			//RefreshComputerErrorsLogTimer = new System.Threading.Timer(RefreshComputerErrorsLogTimerCallback, null, GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			LoadUserSettings();
		}

		private void FilterByAgentTimerCallback(object state)
		{
			SetFilterByAgent();
			FilterByAgentTimer.Change(FILITER_BY_AGENT_REFRESH_TIMEOUT, Timeout.Infinite);
		}

		private async void RefreshComputerErrorsLogTimerCallback(object state)
		{
			try
			{
				await rgvComputerErrorsLog.RefreshDataSourceAsync(await MQueryCommand.SelectComputerErrorsLogsAsync());
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблиц в таймере RefreshComputerErrorsLogTime. Original: {ex.ToString()}");
			}
			finally
			{
				RefreshComputerErrorsLogTimer.Change(GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			}
		}

		private async void RefreshShopsTimerCallback(object state)
		{
			try
			{
				await rgvShops.RefreshDataSourceAsync(await MQueryCommand.SelectShopsGridAsync());
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблиц в таймере RefreshShopsTimer. Original: {ex.ToString()}");
			}
			finally
			{
				RefreshShopsTimer.Change(GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			}
		}

		private async void RefreshSendOpersTimerCallback(object state)
		{
			try
			{
				var operationTypeModel = await MQueryCommand.GetOperationTypesAsync();
				OperationTools.Update(operationTypeModel);
				await rgvSendOpers.RefreshDataSourceAsync(operationTypeModel);
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблиц в таймере RefreshSendOpersTimer. Original: {ex.ToString()}");
			}
			finally
			{
				RefreshSendOpersTimer.Change(GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			}
		}

		private async void RefreshComputersTimerCallback(object state)
		{
			try
			{
				if (AppSettings.IsServiceMode)
				{
					await rgvComputers.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridFullAsync());
				}
				else
				{
					await rgvComputers.RefreshDataSourceAsync(await MQueryCommand.SelectShopComputersServerViewGridAsync());
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблиц в таймере RefreshComputersTimer. Original: {ex.ToString()}");
			}
			finally
			{
				RefreshComputersTimer.Change(GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			}
		}

		private async void RefreshOperationsTimerCallback(object state)
		{
			try
			{
				await rgvOperations.RefreshDataSourceAsync(await MQueryCommand.SelectOperationsGridAsync());
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка обновления таблиц в таймере RefreshOperationsTimer. Original: {ex.ToString()}");
			}
			finally
			{
				RefreshOperationsTimer.Change(GRIDS_REFRESH_TIMEOUT, Timeout.Infinite);
			}
		}

		private async Task UpdatePoolDateCb()
		{
			cbPoolDate.DataSource = await MQueryCommand.GetLast_10_OperationPoolDates();
		}

		private void Form1_VersionRefresh(object sender, VersionRefreshEventArgs e)
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new MethodInvoker(() =>
				{
					tslLastVersion.Text = $"Последняя версия агента на сервере: {e.ver}";
				}));
			}
			else
			{
				tslLastVersion.Text = $"Последняя версия агента на сервере: {e.ver}";
			}
		}

		private void OperationAttributes1_OnValueChanged(object sender, OperationAttributesEventArgs e)
		{
			if (!e.IsTypeError)
			{
				new OperationBase(e.OperTypeId).SaveAttributes(e.Model);
			}
			else
			{
				MessageBox.Show(e.TypeErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public void ResizeColumnsComputersGrid()
		{
			// Выбранные
			rgvComputers.dataGridView.Columns["Selected"].Width = 80;
			rgvComputers.dataGridView.Columns["IsOn"].Width = 80;

			rgvComputers.dataGridView.Columns["IsOnBitmap"].Width = 80;
		}

		private void UpdateUnregistredFilesListControl()
		{
			lbsUnregistredFiles.DataSource = PluginOperationAdapter.GetUnregistredOrUnrelevantFileNamesLastmodifiedDate();
		}

		#region InitGrids
		private async Task InitComputersGridAsync()
		{
			try
			{
				const string BASE_FILTER = "IsClosed = false";
				rgvComputers.KeyField = "ComputerId";
				rgvComputers.PaintCells += PaintComputersGrid;
				rgvComputers.SelectedValueChanged += RgvComputers_SelectedValueChanged;
				rgvComputers.MappingColumns = Mapping.ComputersGridColMap;


				if (AppSettings.IsServiceMode)
				{
					rgvComputers.DataSource = await MQueryCommand.SelectShopComputersServerViewGridFullAsync();
				}
				else
				{
					rgvComputers.DataSource = await MQueryCommand.SelectShopComputersServerViewGridAsync();
					if (rgvComputers.DataSource != null && rgvComputers.DataSource.Count() == 0)
					{
						rgvComputers.DataSource = new ShopComputersModel[]
						{
							new ShopComputersModel()
						};
					}
				}

				rgvComputers.BaseFilter = BASE_FILTER;

				rgvComputers.dataGridView.RowHeadersVisible = false;

				rgvComputers.HideColumns("Is1CServer", "IsMainCashbox", "IsClosed");

				SetFilterOnStart();

				#region ContextMenuAdd
				//Показать/скрыть закрытые магазины
				const string SHOW_CLOSED_SHOPS = "Показать закрытые магазины";
				const string HIDE_CLOSED_SHOPS = "Скрыть закрытые магазины";
				var miShowHideClosedShops = new ToolStripMenuItem();
				miShowHideClosedShops.Name = "miShowHideColumns";
				miShowHideClosedShops.Text = SHOW_CLOSED_SHOPS;
				miShowHideClosedShops.Image = Images.plus;
				miShowHideClosedShops.Click += (sender, e) =>
				{
					if (miShowHideClosedShops.Text == SHOW_CLOSED_SHOPS)
					{
						miShowHideClosedShops.Text = HIDE_CLOSED_SHOPS;
						rgvComputers.BaseFilter = "";
						rgvComputers.ShowColumns("IsClosed");
						miShowHideClosedShops.Image = Images.minus;
					}
					else
					{
						miShowHideClosedShops.Text = SHOW_CLOSED_SHOPS;
						rgvComputers.BaseFilter = BASE_FILTER;
						rgvComputers.HideColumns("IsClosed");
						miShowHideClosedShops.Image = Images.plus;
					}
				};
				rgvComputers.AddMenuItem(miShowHideClosedShops);

				//Показать/скрыть системные ресурсы
				const string SHOW_PC_COLUMNS = "Показать системные ресурсы";
				const string HIDE_PC_COLUMNS = "Скрыть системные ресурсы";
				string[] additionalPcColumns = new string[]
				{
					"AvgPerformanceCounterValuesDateTimeUtc",
					"AvgCpuTimeVis",
					"AvgRamAvailableMBytesVis",
					"AvgDiskQueueLengthVis"
				};
				rgvComputers.HideColumns(additionalPcColumns);
				var miShowHidePcColumns = new ToolStripMenuItem();
				miShowHidePcColumns.Name = "miShowHideColumns";
				miShowHidePcColumns.Text = SHOW_PC_COLUMNS;
				miShowHidePcColumns.Image = Images.plus;
				miShowHidePcColumns.Click += (sender, e) =>
				{
					if (miShowHidePcColumns.Text == SHOW_PC_COLUMNS)
					{
						miShowHidePcColumns.Text = HIDE_PC_COLUMNS;
						rgvComputers.ShowColumns(additionalPcColumns);
						miShowHidePcColumns.Image = Images.minus;
					}
					else
					{
						miShowHidePcColumns.Text = SHOW_PC_COLUMNS;
						rgvComputers.HideColumns(additionalPcColumns);
						miShowHidePcColumns.Image = Images.plus;
					}
				};
				rgvComputers.AddMenuItem(miShowHidePcColumns);

				//Показать/скрыть дополнительные поля
				const string SHOW_ADDITIONAL_COLUMNS = "Показать дополнительные столбцы";
				const string HIDE_ADDITIONAL_COLUMNS = "Скрыть дополнительные столбцы";
				string[] additionalColumns = new string[]
				{
					"ComputerId",
					"External_IP",
					"ComputerName",
					"LastErrorString"
				};
				rgvComputers.HideColumns(additionalColumns);
				var miShowHideColumns = new ToolStripMenuItem();
				miShowHideColumns.Name = "miShowHideColumns";
				miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
				miShowHideColumns.Image = Images.plus;
				miShowHideColumns.Click += (sender, e) =>
				{
					if (miShowHideColumns.Text == SHOW_ADDITIONAL_COLUMNS)
					{
						miShowHideColumns.Text = HIDE_ADDITIONAL_COLUMNS;
						rgvComputers.ShowColumns(additionalColumns);
						miShowHideColumns.Image = Images.minus;
					}
					else
					{
						miShowHideColumns.Text = SHOW_ADDITIONAL_COLUMNS;
						rgvComputers.HideColumns(additionalColumns);
						miShowHideColumns.Image = Images.plus;
					}
				};
				rgvComputers.AddMenuItem(miShowHideColumns);

				//Настройки агента
				var miAgentSettings = new ToolStripMenuItem();
				miAgentSettings.Name = "miAgentSettings";
				miAgentSettings.Text = "Настройки агента";
				miAgentSettings.Image = Images.settings1;
				miAgentSettings.Click += (sender, e) =>
				  {
					  int? selectedKey = rgvComputers.SelectedValue as int?;
					  if (selectedKey.HasValue)
					  {
						  new AgentSettingsForm(selectedKey.Value).ShowDialog();
					  }
				  };
				rgvComputers.AddMenuItem(miAgentSettings);

				//Настройки магазина
				var miShopSettings = new ToolStripMenuItem();
				miShopSettings.Name = "miShopSettings";
				miShopSettings.Text = "Настройки магазина";
				miShopSettings.Image = Images.settings2;
				miShopSettings.Click += (sender, e) =>
				{
					int? selectedKey = rgvComputers.SelectedValue as int?;
					if (selectedKey.HasValue)
					{
						new ShopSettingsForm(selectedKey.Value).ShowDialog();
					}
				};
				rgvComputers.AddMenuItem(miShopSettings);

				rgvComputers.ContextMenuOpening += (sender, e) =>
				{
					ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
					int? selectedId = rgvComputers.SelectedValue as int?;

					if (contextMenuStrip != null && selectedId.HasValue)
					{
						miAgentSettings.Enabled =
						miShopSettings.Enabled = selectedId.Value > 0;
					}
				};
				#endregion ContextMenuAdd
			}
			finally
			{
				await rgvComputers.ShowLoading(false);
			}
		}

		private void InitCbOperationGroups()
		{
			cbOperationGroups.ValueMember = "Id";
			cbOperationGroups.DisplayMember = "Name";
			cbOperationGroups.DataSource = MQueryCommand.SelectOperationGroups();
		}

		private async Task InitSendOpersGrid()
		{
			try
			{
				InitCbOperationGroups();

				rgvSendOpers.SelectedValueChanged += RgvSendOpers_SelectedValueChanged;

				rgvComputers.dataGridView.CellPainting += rgvSendOpers_DataGridView_CellPainting;

				rgvSendOpers.KeyField = "Id";
				rgvSendOpers.IsMultiselect = false;
				rgvSendOpers.PaintCells += RgvSendOpers_PaintCells;

				rgvSendOpers.LbFilterVisible = false;

				rgvSendOpers.MappingColumns = Mapping.SendOpersGridColMap;
				rgvSendOpers.DataSource = await MQueryCommand.GetOperationTypesAsync();
				if (rgvSendOpers.DataSource != null && rgvSendOpers.DataSource.Count() == 0)
				{
					rgvSendOpers.DataSource = new OperationTypeModel[]
					{
						new OperationTypeModel()
					};
				}

				rgvSendOpers.Filter = $"GroupId = {Convert.ToInt32(cbOperationGroups.SelectedValue)}";
				rgvSendOpers.dataGridView.RowHeadersVisible = false;

				#region ContextMenuAdd
				//Переместить в группу
				var miMoveToOperationGroup = new ToolStripMenuItem();
				miMoveToOperationGroup.Name = "miMoveToOperationGroup";
				miMoveToOperationGroup.Text = "Переместить в группу";
				miMoveToOperationGroup.Click += (sender, e) =>
				{
					int? selectedKey = rgvSendOpers.SelectedValue as int?;
					if (selectedKey.HasValue)
					{
						string operTypeName = Convert.ToString(rgvSendOpers.SelectedRow.Cells["DisplayGridName"].Value);
						int currentGroupId = Convert.ToInt32(rgvSendOpers.SelectedRow.Cells["GroupId"].Value);

						MoveToOperationGroupForm moveToOperationGroupForm = new MoveToOperationGroupForm(operTypeName, currentGroupId);
						if (moveToOperationGroupForm.ShowDialog() == DialogResult.OK)
						{
							if (moveToOperationGroupForm.OperationGroupId > 0)
							{
								var res = MQueryCommand.MoveOperationToGroup(selectedKey.Value, moveToOperationGroupForm.OperationGroupId);
								if (!res.IsComplete)
								{
									MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
							else
							{
								MessageBox.Show("Ошибка GroupId", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				};
				rgvSendOpers.AddMenuItem(miMoveToOperationGroup);

				//Описание операции
				var miOperationDescription = new ToolStripMenuItem();
				miOperationDescription.Name = "miOperationDescription";
				miOperationDescription.Text = "Описание операции";
				miOperationDescription.Click += (sender, e) =>
				{
					int? selectedKey = rgvSendOpers.SelectedValue as int?;
					if (selectedKey.HasValue)
					{
						rgvSendOpers.ShowCellExtensionForm(rgvSendOpers.SelectedRow.Cells["Description"]);
					}
				};
				rgvSendOpers.AddMenuItem(miOperationDescription);
				#endregion
			}
			finally
			{
				await rgvSendOpers.ShowLoading(false);
			}
		}

		private void RgvSendOpers_SelectedValueChanged(object sender, EventArgs e)
		{
			try
			{
				lbCurrentOper.Text = $"{ConvertSafe.ToString((rgvSendOpers.dataGridView.CurrentRow.Cells["NameRus"]).Value)}";
			}
			catch { }

			int id = Convert.ToInt32(rgvSendOpers.SelectedValue);
			string fileName = OperationTools.GetOperationFileNameById(id);
			if (string.IsNullOrEmpty(fileName))
			{
				operationAttributes1.InitializeControl(null);
				return;
			}

			Type attrType = null;
			string operName = OperationTools.GetOperationNameEnById(id);
			string operFileName = OperationTools.GetOperationFileNameById(id);
			string operFileMd5 = OperationTools.GetOperationFileMd5ById(id);

			if (!string.IsNullOrEmpty(operName) && !string.IsNullOrEmpty(operFileName) && !string.IsNullOrEmpty(operFileMd5))
			{
				attrType = PluginOperationAdapter.GetPluginOperationAttributesType(
						operName
					  , operFileName
					  , operFileMd5);
			}

			if (attrType != null)
			{
				IOperationAttributes operationAttributesInstance = (IOperationAttributes)Activator.CreateInstance(attrType);

				//Ищем сохраненные атрибуты
				JObject savedModel = new OperationBase(id).GetSavedAttributes() as JObject;
				JToken token = null;
				if (savedModel != null && savedModel.HasValues)
				{
					foreach (var prop in operationAttributesInstance.GetType().GetProperties())
					{
						savedModel.TryGetValue(prop.Name, out token);
						if (token != null)
						{
							object propValue = ExtTools.ConvertStringToType(token.ToString(), prop.PropertyType);
							if (propValue != null)
							{
								prop.SetValue(operationAttributesInstance, propValue);
							}
						}
					}
				}

				operationAttributes1.InitializeControl(operationAttributesInstance, id);
			}
			else
			{
				operationAttributes1.InitializeControl(null);
			}
		}

		//Выделение строки жирным, при установки флага
		private void rgvSendOpers_DataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			if (e.ColumnIndex == 0 && e.RowIndex > -1)
			{
				if (Convert.ToInt32(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == 1)
				{
					foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
					{
						cell.Style.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
					}
				}
				else
				{
					foreach (DataGridViewCell cell in dgv.Rows[e.RowIndex].Cells)
					{
						cell.Style.Font = new Font(SystemFonts.DefaultFont, FontStyle.Regular);
					}
				}
			}
		}

		private async Task InitComputerErrorsLogGridAsync()
		{
			rgvComputerErrorsLog.KeyField = "ErrorId";
			rgvComputerErrorsLog.IsMultiselect = false;
			rgvComputerErrorsLog.ResetFilterRadioButton = rbComputerErrorsLogAll;
			rgvComputerErrorsLog.MappingColumns = Mapping.ComputerErrorsLogGridColMap;
			rgvComputerErrorsLog.DataSource = await MQueryCommand.SelectComputerErrorsLogsAsync();
			SetFilterOnStart();
		}

		private async Task InitOperationsGridAsync()
		{
			try
			{
				rgvOperations.KeyField = "ID";
				rgvOperations.PaintCells += PaintOperationsGrid;
				rgvOperations.IsMultiselect = false;
				rgvOperations.IsDetailsEnabled = true;
				rgvOperations.ResetFilterRadioButton = rbOperationsAll;
				rgvOperations.ShowDetailsEvent += RgvOperations_ShowDetailsEvent;
				rgvOperations.Filter = $"creationdate >= '{LastOperationDateLocal}' and CreatedUser = '{System.Security.Principal.WindowsIdentity.GetCurrent().Name}'";
				rgvOperations.MappingColumns = Mapping.OperationsGridColMap;
				rgvOperations.DataSource = await MQueryCommand.SelectOperationsGridAsync();
				if (rgvOperations.DataSource != null && rgvOperations.DataSource.Count() == 0)
				{
					rgvOperations.DataSource = new ViewOperationsModel[]
					{
						new ViewOperationsModel()
					};
				}
				rgvOperations.DataView.Sort = "CreationDate DESC";
				rgvOperations.dataGridView.RowHeadersVisible = false;
				SetFilterOnStart();

				int? keyValue = rgvComputers.SelectedValue as int?;
				if (keyValue.HasValue)
				{
					rgvOperations.Filter = $"ComputerId = {keyValue.Value}";
				}
			}
			finally
			{
				await rgvOperations.ShowLoading(false);
			}
		}

		//Форма Details для Actions
		private async void RgvOperations_ShowDetailsEvent(RefreshingGridView sender)
		{
			DetailsForm df = new DetailsForm();
			df.rgvDetails.IsMultiselect = false;
			df.rgvDetails.KeyField = "ReportId";
			df.rgvDetails.PaintCells += RgvDetails_PaintCells;
			df.rgvDetails.DataSource = await MQueryCommand.SelectActionsReportsGridAsync((int)sender.SelectedValue);
			df.rgvDetails.HideColumns("ReportId", "OperationId");
			df.ShowDialog();
		}

		//Тбалица магазинов, скрыта
		private async Task InitShopsGridAsync()
		{
			rgvShops.KeyField = "ShopId";

			rgvShops.MappingColumns = Mapping.ShopsGridColMap;
			rgvShops.DataSource = await MQueryCommand.SelectShopsGridAsync();
		}

		//Установка фильтров по умолчанию для таблиц
		private void SetFilterOnStart()
		{
			//if (!AppSettings.IsServiceMode)
			//{
			//	rgvComputers.HideColumns("Is1CServer", "IsMainCashbox", "IsClosed");
			//	//rgvComputers.Filter = "Is1CServer = 1";
			//}
			//rgvOperations.Filter = $"CreationDate > '{DateTime.UtcNow.Date.AddDays(-1)}'";
			rbOperationsByAgent.Checked = false;
			rbOperationsByAgent.Checked = true;

			//rbOperationsLast.Checked = false;
			//rbOperationsLast.Checked = true;
		}
		#endregion InitGrids

		#region PaintGrids
		private void RgvSendOpers_PaintCells(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow dr in rows)
			{
				if (Convert.ToString(dr.Cells["DisplayGridName"].Value) == "RestartMagicUpdater_Service")
				{
					dr.Cells["DisplayGridName"].Style.BackColor = Color.Pink;
				}
			}
		}

		private void PaintOperationsGrid(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow dr in rows)
			{
				if (!Convert.ToBoolean(dr.Cells["IsCompleted"].Value == DBNull.Value ? false : dr.Cells["IsCompleted"].Value)
					&& Convert.ToBoolean(dr.Cells["IsRead"].Value == DBNull.Value ? false : dr.Cells["IsRead"].Value))
				{
					dr.Cells.OfType<DataGridViewCell>().ForEach(c => c.Style.BackColor = Color.Pink);
				}

				if (Convert.ToBoolean(dr.Cells["IsCompleted"].Value == DBNull.Value ? false : dr.Cells["IsCompleted"].Value)
					&& Convert.ToBoolean(dr.Cells["IsRead"].Value == DBNull.Value ? false : dr.Cells["IsRead"].Value))
				{
					dr.Cells.OfType<DataGridViewCell>().ForEach(c => c.Style.BackColor = Color.LightGreen);
				}

				if (Convert.ToInt32(dr.Cells["Is1CError"].Value) == 1
					&& Convert.ToBoolean(dr.Cells["IsRead"].Value == DBNull.Value ? false : dr.Cells["IsRead"].Value))
				{
					dr.Cells.OfType<DataGridViewCell>().ForEach(c => c.Style.BackColor = Color.Orange);
				}

				if (Convert.ToInt32(dr.Cells["IsActionError"].Value) == 1
					&& Convert.ToBoolean(dr.Cells["IsRead"].Value == DBNull.Value ? false : dr.Cells["IsRead"].Value))
				{
					dr.Cells["Details"].Style.BackColor = Color.IndianRed;
					dr.Cells["Details"].Value = "Double click";
				}
			}
		}

		private void RgvDetails_PaintCells(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow dr in rows)
			{
				if (Convert.ToInt32(dr.Cells["IsCompleted"].Value) == 0)
				{
					dr.Cells.OfType<DataGridViewCell>().ForEach(c => c.Style.BackColor = Color.Pink);
				}

				if (Convert.ToInt32(dr.Cells["IsCompleted"].Value) == 1)
				{
					dr.Cells.OfType<DataGridViewCell>().ForEach(c => c.Style.BackColor = Color.LightGreen);
				}
			}
		}

		private void PaintComputersGrid(DataGridView sender)
		{
			DataGridViewRow[] rows = sender.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow dr in rows)
			{
				if (Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["Selected"].ReadOnly = false;
					foreach (DataGridViewCell cell in dr.Cells)
					{
						cell.Style.BackColor = Color.White;
					}
				}

				if (LastAgentVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Before)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.Pink;
				}

				if (LastAgentVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Equal)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.LightGreen;
				}

				if (LastAgentVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Subsequent)
				{
					dr.Cells["MagicUpdaterVersion"].Style.BackColor = Color.DeepSkyBlue;
				}

				if (Convert.ToInt32(dr.Cells["ComputerId"].Value) < 0)
				{
					dr.Cells["Selected"].ReadOnly = true;
					foreach (DataGridViewCell cell in dr.Cells)
					{
						cell.Style.BackColor = Color.LightGray;
					}
				}

				//Включен или нет
				if (Convert.ToInt32(dr.Cells["IsOn"].Value) == 1)
				{
					dr.Cells["IsOn"].Style.BackColor = Color.Green;
				}
				else
				{
					dr.Cells["IsOn"].Style.BackColor = Color.Red;
				}

				//Ошибка обмена
				if (Convert.ToBoolean(dr.Cells["ExchangeError"].Value)
					&& string.IsNullOrEmpty(Convert.ToString(dr.Cells["OperState"].Value))
					&& Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.PaleVioletRed;
				}

				//Статусы операций
				#region OperStates
				if (Convert.ToString(dr.Cells["OperState"].Value) == "Завершена с ошибкой" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.PaleVioletRed;
				}
				else if (Convert.ToString(dr.Cells["OperState"].Value) == "Выполняется" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.LightYellow;
				}
				else if (Convert.ToString(dr.Cells["OperState"].Value) == "Завершена успешно" && Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.FromArgb(215, 255, 220);
				}
				#endregion OperStates

				//Заливаем белым
				if (!Convert.ToBoolean(dr.Cells["ExchangeError"].Value)
					&& string.IsNullOrEmpty(Convert.ToString(dr.Cells["OperState"].Value))
					&& Convert.ToInt32(dr.Cells["ComputerId"].Value) > 0)
				{
					dr.Cells["ShopName"].Style.BackColor =
					dr.Cells["LastSuccessfulReceive"].Style.BackColor =
					dr.Cells["LastSuccessfulUpload"].Style.BackColor =
					dr.Cells["OperationTypeRu"].Style.BackColor =
					dr.Cells["OperationCreationDate"].Style.BackColor =
					dr.Cells["OperState"].Style.BackColor = Color.White;
				}
			}

			if (rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().Any(dr => Convert.ToBoolean(dr.Cells["IsOn"].Value) && LastAgentVersionChecker.CompareVersions(Convert.ToString(dr.Cells["MagicUpdaterVersion"].Value)) == VersionCompareResult.Before))
			{
				if (sbStatus.InvokeRequired)
				{
					sbStatus.Invoke(new MethodInvoker(() =>
					{
						tsbUpdateMU.Enabled = true;
						tsbUpdateMU.BackColor = Color.Pink;
					}));
				}
				else
				{
					tsbUpdateMU.Enabled = true;
					tsbUpdateMU.BackColor = Color.Pink;
				}
			}
			else
			{
				tsbUpdateMU.Enabled = false;
				tsbUpdateMU.BackColor = SystemColors.Control;
			}
		}
		#endregion PaintGrids

		private void rbOperationsLast_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvOperations.Filter = $"creationdate >= '{LastOperationDateLocal}' and CreatedUser = '{System.Security.Principal.WindowsIdentity.GetCurrent().Name}'"; ;
			}
		}

		private void rbOperationsToday_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvOperations.Filter = $"CreationDate > '{DateTime.UtcNow.Date}'";
			}
		}

		private void rbOperationsAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvOperations.Filter = "";
			}
		}

		private void rbOperationsByAgent_CheckedChanged(object sender, EventArgs e)
		{
			if (rbOperationsByAgent.Checked)
			{
				FilterByAgentTimer?.Change(0, Timeout.Infinite);
			}
			else
			{
				FilterByAgentTimer?.Change(Timeout.Infinite, Timeout.Infinite);
			}

			//SetFilterByAgent();
		}

		private void SetFilterByAgent()
		{
			if (rbOperationsByAgent.Checked)
			{
				if (rgvComputers.InvokeRequired)
				{
					rgvComputers.Invoke(new MethodInvoker(() =>
					{
						int? keyValue = rgvComputers.SelectedValue as int?;
						if (keyValue.HasValue)
						{
							if (rgvOperations.Filter != $"ComputerId = {keyValue.Value}")
							{
								rgvOperations.Filter = $"ComputerId = {keyValue.Value}";
							}
						}
					}));
				}
				else
				{
					int? keyValue = rgvComputers.SelectedValue as int?;
					if (keyValue.HasValue)
					{
						if (rgvOperations.Filter != $"ComputerId = {keyValue.Value}")
						{
							rgvOperations.Filter = $"ComputerId = {keyValue.Value}";
						}
					}
				}
			}
		}

		private void rbPoolDate_CheckedChanged(object sender, EventArgs e)
		{
			cbPoolDate.Enabled = rbPoolDate.Checked;
			SetFilterByPoolDate();
		}

		private void cbPoolDate_SelectedValueChanged(object sender, EventArgs e)
		{
			SetFilterByPoolDate();
		}

		private void SetFilterByPoolDate()
		{
			if (rbPoolDate.Checked)
			{
				DateTime? poolDate = cbPoolDate.SelectedValue as DateTime?;
				if (poolDate.HasValue)
				{
					if (rgvOperations.Filter != $"PoolDate = '{poolDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}'")
					{
						rgvOperations.Filter = $"PoolDate = '{poolDate.Value.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture)}'";
					}
				}
			}
		}

		private void SetOperationsLastFilter(DateTime operationDate)
		{
			if (rbOperationsLast.Checked)
				rgvOperations.Filter = $"creationdate >= '{operationDate}' and CreatedUser = '{System.Security.Principal.WindowsIdentity.GetCurrent().Name}'";
		}

		private async void tsbUpdateMU_Click(object sender, EventArgs e)
		{
			if (_isAllAgentsUpdateFormShowing)
			{
				return;
			}

			_isAllAgentsUpdateFormShowing = true;
			if (sbStatus.InvokeRequired)
			{
				sbStatus.Invoke(new MethodInvoker(() => tsbUpdateMU.Enabled = false));
			}
			else
			{
				tsbUpdateMU.Enabled = false;
			}

			try
			{
				ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
				cf.rgvInfo.IsMultiselect = false;
				cf.rgvInfo.KeyField = "ComputerId";
				cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
				cf.rgvInfo.DataSource = await MQueryCommand.SelectShopComputersServerViewGridAsync();
				cf.OperationTypeName = OperationTools.TryGetOperationNameRuByEn("SelfUpdate");
				cf.rgvInfo.Filter = $"MagicUpdaterVersion <> '{LastAgentVersionChecker.GetLatestVersion()}' and IsON = 1";
				cf.rgvInfo.HideColumns("LicStatusBitmap");
				if (cf.ShowDialog() == DialogResult.OK)
				{
					lastOperationDateUtc = DateTime.UtcNow;
					foreach (DataGridViewRow row in cf.rgvInfo.dataGridView.Rows)
					{
						new OperationBase(OperationTools.GetOperationIdByName("SelfUpdate")).SendForComputer(Convert.ToInt32(row.Cells["ComputerId"].Value), lastOperationDateUtc);
					}
					SetOperationsLastFilter(LastOperationDateLocal);
				}
			}
			finally
			{
				_isAllAgentsUpdateFormShowing = false;
				if (sbStatus.InvokeRequired)
				{
					sbStatus.Invoke(new MethodInvoker(() => tsbUpdateMU.Enabled = true));
				}
				else
				{
					tsbUpdateMU.Enabled = true;
				}
			}
		}

		private void rbComputerErrorsLogToday_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvComputerErrorsLog.Filter = $"ErrorDate >= '{DateTime.UtcNow.Date}'";
			}
		}

		private void rbComputerErrorsLogAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvComputerErrorsLog.Filter = $"";
			}
		}

		private async void btnCreateShop_Click(object sender, EventArgs e)
		{
			CreateShopForm csf = new CreateShopForm();
			if (csf.ShowDialog() == DialogResult.OK)
			{
				await rgvShops.RefreshDataSourceAsync(await MQueryCommand.SelectShopsGridAsync());
				MessageBox.Show($"Успешное создание магазина {csf.ShopId}", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		public void SelectByComputerId(int computerId, bool isSelected = true)
		{
			DataGridViewRow dr = rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(f => Convert.ToInt32(f.Cells["ComputerId"].Value) == computerId);
			if (dr != null)
			{
				dr.Cells["Selected"].Value = isSelected;
			}
		}

		private void SelectComputersForDoneOperations(int index)
		{
			IEnumerable operations = from s in rgvOperations.DataView.OfType<DataRowView>()
									 orderby Convert.ToInt32(s["ID"]) descending
									 select new ViewOperationsModel
									 {
										 ComputerId = Convert.ToInt32(s["ComputerId"]),
										 IsCompleted = s["IsCompleted"] == DBNull.Value ? false : Convert.ToBoolean(s["IsCompleted"]),
										 Is1CError = Convert.ToInt32(s["Is1CError"]),
										 LogMessage1C = s["LogMessage1C"] == DBNull.Value ? "" : Convert.ToString(s["LogMessage1C"])
									 };

			List<ViewOperationsModel> operationsList = new List<ViewOperationsModel>();
			foreach (var oper in operations.OfType<ViewOperationsModel>())
			{
				if (operationsList.FirstOrDefault(f => f.ComputerId == oper.ComputerId && f.IsCompleted == oper.IsCompleted && f.Is1CError == oper.Is1CError) == null)
				{
					operationsList.Add(oper);
				}
			}

			rgvComputers.ClearSelection();

			switch (index)
			{
				//Выполненные
				case 0:
					foreach (int computerId in operations.OfType<ViewOperationsModel>().Where(w => w.IsCompleted ?? false).Select(s => s.ComputerId))
					{
						SelectByComputerId(computerId);
					}
					break;
				//Ошибочные
				case 1:
					foreach (int computerId in operations.OfType<ViewOperationsModel>().Where(w => !w.IsCompleted ?? false).Select(s => s.ComputerId))
					{
						SelectByComputerId(computerId);
					}
					break;
				//С ошибкой 1С
				case 2:
					foreach (int computerId in operations.OfType<ViewOperationsModel>().Where(w => w.Is1CError == 1).Select(s => s.ComputerId))
					{
						SelectByComputerId(computerId);
					}
					break;
				//Выполненные и с пустым логом 1С
				case 3:
					foreach (int computerId in operations.OfType<ViewOperationsModel>().Where(w => string.IsNullOrEmpty(w.LogMessage1C) && (w.IsCompleted ?? false)).Select(s => s.ComputerId))
					{
						SelectByComputerId(computerId);
					}
					break;
			}
		}

		private void btnRegisterSelectedOperation_Click(object sender, EventArgs e)
		{

		}

		private void btnRegisterAllFiles_Click(object sender, EventArgs e)
		{

		}

		private void tsbScheduler_Click(object sender, EventArgs e)
		{

		}

		private void CheckAll_Click(object sender, EventArgs e)
		{
			DataGridViewRow[] rows = rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().Where(w => Convert.ToInt32(w.Cells["ComputerId"].Value) > 0).ToArray();
			foreach (DataGridViewRow row in rows)
			{
				row.Cells["Selected"].Value = true;
			}

		}

		private void UnCheckAll_Click(object sender, EventArgs e)
		{
			DataGridViewRow[] rows = rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().Where(w => Convert.ToInt32(w.Cells["ComputerId"].Value) > 0).ToArray();
			foreach (DataGridViewRow row in rows)
			{
				row.Cells["Selected"].Value = false;
			}
		}

		private async void SendOperation_Click(object sender, EventArgs e)
		{
			ToolStripButton tsb = sender as ToolStripButton;
			tsb.Enabled = false;
			try
			{
				int blockedUserIndex = _blockedUsers.IndexOf(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
				if (blockedUserIndex >= 0)
				{
					string blockedUser = _blockedUsers[blockedUserIndex];
					MessageBox.Show($"Пользователю {blockedUser} запрещено отправлять операции", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (rgvComputers.SelectedValues.Count == 0)
				{
					MessageBox.Show("Компьютеры не выбраны\r\nНеобходимо выбрать компьютеры в таблице \"Компьютеры\"", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}

				ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
				cf.rgvInfo.KeyField = "ComputerId";
				cf.rgvInfo.IsMultiselect = false;
				cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
				cf.rgvInfo.DataSource = await MQueryCommand.SelectComputersGridAsync();
				cf.OperationTypeName = OperationTools.TryGetOperationNameRuById(Convert.ToInt32(rgvSendOpers.SelectedValue));
				cf.rgvInfo.Filter = $"ComputerId in ({string.Join(",", rgvComputers.SelectedValues.ToArray())})";
				cf.rgvInfo.HideColumns("LicStatusBitmap");

				if (cf.ShowDialog() == DialogResult.OK)
				{
					lastOperationDateUtc = DateTime.UtcNow;
					foreach (object val in rgvComputers.SelectedValues)
					{
						new OperationBase(Convert.ToInt32(rgvSendOpers.SelectedValue)).SendForComputer(Convert.ToInt32(val), lastOperationDateUtc);
					}

					await UpdatePoolDateCb();
					SetOperationsLastFilter(LastOperationDateLocal);
				}
			}
			finally
			{
				tsb.Enabled = true;
			}
		}

		private void RemoteControl_Click(object sender, EventArgs e)
		{
			DataGridViewRow[] rows = rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow row in rows)
			{
				if (row.Selected)
				{
					String Address = (String)row.Cells["AddressToConnect"].Value;
					if (Address != "")
					{
						try
						{
							Process.Start(Address);
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							MLogger.Error(ex.ToString());
						}
					}
				}
			}
		}

		private void NoFilter_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(rgvComputers.Filter))
			{
				rgvComputers.Filter = "";
				tbFind.Text = "";
			}
		}

		private void Clear_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(rgvComputers.Filter))
			{
				rgvComputers.Filter = "";
				tbFind.Text = "";
			}
		}

		private void Find_Click(object sender, EventArgs e)
		{
			rgvComputers.Filter = $"ShopName like '%{tbFind.Text}%'";
		}

		private void FindTextBox_TextChanged(object sender, EventArgs e)
		{
			//rgvComputers.Filter = $"ShopName like '%{tbFind.Text}%'";
		}

		private void tsbSettings_Click_1(object sender, EventArgs e)
		{
			new SettingsForm().ShowDialog();
		}

		private void cbOperationGroups_SelectedValueChanged(object sender, EventArgs e)
		{
			ComboBox cb = sender as ComboBox;
			if (cb == null)
			{
				return;
			}
			rgvSendOpers.Filter = $"GroupId = {Convert.ToInt32(cb.SelectedValue)}";
		}

		private async void miSendMainCashbox_Click(object sender, EventArgs e)
		{
			ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
			cf.rgvInfo.IsMultiselect = false;
			cf.rgvInfo.KeyField = "ComputerId";
			cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
			cf.rgvInfo.DataSource = await MQueryCommand.SelectComputersGridAsync();
			cf.OperationTypeName = OperationTools.TryGetOperationNameRuById(Convert.ToInt32(rgvSendOpers.SelectedValue));
			cf.rgvInfo.Filter = $"IsMainCashbox = 1 and IsON = 1";
			cf.rgvInfo.HideColumns("LicStatusBitmap");

			if (cf.ShowDialog() == DialogResult.OK)
			{
				lastOperationDateUtc = DateTime.UtcNow;
				foreach (DataGridViewRow row in cf.rgvInfo.dataGridView.Rows)
				{
					new OperationBase(Convert.ToInt32(rgvSendOpers.SelectedValue)).SendForComputer(Convert.ToInt32(row.Cells["ComputerId"].Value), lastOperationDateUtc);
				}
				SetOperationsLastFilter(LastOperationDateLocal);
			}
		}

		private async void miSendServer_Click(object sender, EventArgs e)
		{
			ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
			cf.rgvInfo.IsMultiselect = false;
			cf.rgvInfo.KeyField = "ComputerId";
			cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
			cf.rgvInfo.DataSource = await MQueryCommand.SelectComputersGridAsync();
			cf.OperationTypeName = OperationTools.TryGetOperationNameRuById(Convert.ToInt32(rgvSendOpers.SelectedValue));
			cf.rgvInfo.Filter = $"Is1CServer = 1 and IsON = 1";
			cf.rgvInfo.HideColumns("LicStatusBitmap");
			if (cf.ShowDialog() == DialogResult.OK)
			{
				lastOperationDateUtc = DateTime.UtcNow;
				foreach (DataGridViewRow row in cf.rgvInfo.dataGridView.Rows)
				{
					new OperationBase(Convert.ToInt32(rgvSendOpers.SelectedValue)).SendForComputer(Convert.ToInt32(row.Cells["ComputerId"].Value), lastOperationDateUtc);
				}
				SetOperationsLastFilter(LastOperationDateLocal);
			}
		}

		private async void miSendComputers_Click(object sender, EventArgs e)
		{
			if (rgvComputers.SelectedValues.Count == 0)
			{
				MessageBox.Show("Компьютеры не выбраны\r\nНеобходимо выбрать компьютеры в таблице \"Компьютеры\"", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
			cf.rgvInfo.KeyField = "ComputerId";
			cf.rgvInfo.IsMultiselect = false;
			cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
			cf.rgvInfo.DataSource = await MQueryCommand.SelectComputersGridAsync();
			cf.OperationTypeName = OperationTools.TryGetOperationNameRuById(Convert.ToInt32(rgvSendOpers.SelectedValue));
			cf.rgvInfo.Filter = $"ComputerId in ({string.Join(",", rgvComputers.SelectedValues.ToArray())})";
			cf.rgvInfo.HideColumns("LicStatusBitmap");

			if (cf.ShowDialog() == DialogResult.OK)
			{
				lastOperationDateUtc = DateTime.UtcNow;
				foreach (object val in rgvComputers.SelectedValues)
				{
					new OperationBase(Convert.ToInt32(rgvSendOpers.SelectedValue)).SendForComputer(Convert.ToInt32(val), lastOperationDateUtc);
				}
				SetOperationsLastFilter(LastOperationDateLocal);
			}
		}

		private async void miSendShops_Click(object sender, EventArgs e)
		{
			if (rgvShops.SelectedValues.Count == 0)
			{
				MessageBox.Show("Магазины не выбраны\r\nНеобходимо выбрать магазины в таблице \"Магазины\"", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
			cf.rgvInfo.KeyField = "ComputerId";
			cf.rgvInfo.IsMultiselect = false;
			cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
			cf.rgvInfo.DataSource = await MQueryCommand.SelectComputersGridAsync();
			cf.OperationTypeName = OperationTools.TryGetOperationNameRuById(Convert.ToInt32(rgvSendOpers.SelectedValue));
			string[] selVals = new string[rgvShops.SelectedValues.Count];
			rgvShops.SelectedValues.CopyTo(selVals);
			for (int i = 0; i < selVals.Length; i++)
			{
				selVals[i] = $"'{selVals[i]}'";
			}
			cf.rgvInfo.Filter = $"ShopId in ({string.Join(",", selVals)})";
			cf.rgvInfo.HideColumns("LicStatusBitmap");

			if (cf.ShowDialog() == DialogResult.OK)
			{
				lastOperationDateUtc = DateTime.UtcNow;
				foreach (object val in rgvShops.SelectedValues)
				{
					new OperationBase(Convert.ToInt32(rgvSendOpers.SelectedValue)).SendForComputer(Convert.ToInt32(val), lastOperationDateUtc);
				}
				SetOperationsLastFilter(LastOperationDateLocal);
			}
		}

		private void miCompleted_Click(object sender, EventArgs e)
		{
			SelectComputersForDoneOperations(0);
		}

		private void miErrors_Click(object sender, EventArgs e)
		{
			SelectComputersForDoneOperations(1);
		}

		private void miErrors1C_Click(object sender, EventArgs e)
		{
			SelectComputersForDoneOperations(2);
		}

		private void miCompletedWithEmptyLog_Click(object sender, EventArgs e)
		{
			SelectComputersForDoneOperations(3);
		}

		private void miEmty1CLog_Click(object sender, EventArgs e)
		{
			SelectComputersForDoneOperations(3);
		}

		private void ErrFilter_Click(object sender, EventArgs e)
		{
			rgvComputers.Filter = $"IsExchangeError = 1";
		}

		private void OffFilter_Click(object sender, EventArgs e)
		{
			rgvComputers.Filter = $"IsOn = 0";
		}

		private void tbFind_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				rgvComputers.Filter = $"ShopName like '%{tbFind.Text}%'";
			}
		}

		private void RgvComputers_SelectedValueChanged(object sender, EventArgs e)
		{
			//SetFilterByAgent();
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			Application.Exit();
		}

		private void lbsUnregistredFiles_SelectedValueChanged(object sender, EventArgs e)
		{
			tsbRegisterSelectedOperation.Enabled = lbsUnregistredFiles.SelectedValue != null;
		}

		private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
		{
			var x = this.scShopsOpers.SplitterDistance;
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			SaveUserSettings();
		}

		private void MainForm_Shown(object sender, EventArgs e)
		{
			//LoadUserSettings();
		}

		private void вЦентреToolStripMenuItem_Click(object sender, EventArgs e)
		{

			DataGridViewRow[] rows = rgvComputers.dataGridView.Rows.OfType<DataGridViewRow>().ToArray();
			foreach (DataGridViewRow row in rows)
			{
				if (row.Selected)
				{
					String ShopName = (String)row.Cells["ShopName"].Value;
					if (ShopName != "")
					{
						Task.Factory.StartNew(() =>
						{
							try
							{
								var v8ComConnector = TypeDelegator.GetTypeFromProgID("V83.COMConnector");
								dynamic V8 = Activator.CreateInstance(v8ComConnector);

								MainSettings.LoadMonitorCommonGlobalSettings();
								string Server1C = MainSettings.GlobalSettings.Server1C;
								string Base1C = MainSettings.GlobalSettings.Base1C;
								string User1C = MainSettings.GlobalSettings.User1C;
								string Password1C = MainSettings.GlobalSettings.Password1C;
								string t = "srvr=\"" + Server1C + "\"; ref=\"" + Base1C + "\"; usr=\"" + User1C + "\"; pwd=\"" + Password1C + "\"";

								dynamic Base = V8.Connect(t);
								Base.VypolnitObmenDannymiDljaUzlaInformacionnojBazy(ShopName);
								Base = null;
								V8 = null;
							}
							catch (Exception ex)
							{

							}
						});
					}
				}
			}

		}

		async private void вУзлеToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (rgvComputers.SelectedValues.Count == 0)
			{
				MessageBox.Show("Компьютеры не выбраны\r\nНеобходимо выбрать компьютеры в таблице \"Компьютеры\"", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}

			lastOperationDateUtc = DateTime.UtcNow;
			foreach (object val in rgvComputers.SelectedValues)
			{
				new OperationBase(10014).SendForComputer(Convert.ToInt32(val), lastOperationDateUtc); //Операция обмена с центром
			}

			await UpdatePoolDateCb();
			SetOperationsLastFilter(LastOperationDateLocal);
		}

		//Неиспользуемый код
		#region NotUsed
		private DataView ArrayToDataView<T>(T[] arr)
		{
			if (arr == null || arr.Length == 0 || arr[0] == null)
				return null;

			DataTable dataTable = new DataTable();
			PropertyInfo[] propsInfo = arr[0].GetType().GetProperties();

			foreach (PropertyInfo pi in propsInfo)
			{
				dataTable.Columns.Add(pi.Name);
			}

			foreach (T obj in arr)
			{
				DataRow row = dataTable.NewRow();
				int i = 0;
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					row[i] = pi.GetValue(obj);
					i++;
				}
				dataTable.Rows.Add(row);
			}

			return dataTable.DefaultView;
		}

		private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;
			DataGridViewColumn col = dgv.Columns[e.ColumnIndex];

			ListSortDirection direction = ListSortDirection.Ascending;
			dgv.Sort(col, direction);

			col.HeaderCell.SortGlyphDirection =
				direction == ListSortDirection.Ascending ?
				SortOrder.Descending : SortOrder.Ascending;
		}

		private void rbComputersAll_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvComputers.Filter = "";
			}
		}

		private void rbComputersMainCashbox_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvComputers.Filter = $"IsMainCashbox = 1 and IsON = 1";
			}
		}

		private void rbComputersServers_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rb = (RadioButton)sender;
			if (rb.Checked)
			{
				rgvComputers.Filter = $"Is1CServer = 1 and IsON = 1";
			}
		}
		#endregion NotUsed

		private void tsbRefresh_Click(object sender, EventArgs e)
		{
			UpdateUnregistredFilesListControl();
		}

		private void tsbRegisterSelectedOperation_Click(object sender, EventArgs e)
		{
			tsbRegisterSelectedOperation.Enabled = false;
			try
			{
				if (lbsUnregistredFiles.SelectedValue == null)
				{
					return;
				}

				var res = PluginOperationAdapter.RegisterOrUpdatePluginLastModifiedDate(Convert.ToString(lbsUnregistredFiles.SelectedValue));
				if (res.IsComplete)
				{
					UpdateUnregistredFilesListControl();
				}
				else
				{
					MLogger.Error(res.Message);
				}
			}
			finally
			{
				tsbRegisterSelectedOperation.Enabled = true;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			toolStripButton1.Enabled = false;
			try
			{
				var res = PluginOperationAdapter.RegisterOrUpdateAllPlugins();
				if (res.IsComplete)
				{
					UpdateUnregistredFilesListControl();
				}
				else
				{
					MLogger.Error(res.Message);
				}
			}
			finally
			{
				toolStripButton1.Enabled = true;
			}
		}

		private void MainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (tabControl1.SelectedTab == tabPageShops)
			{
				if (e.Control && e.KeyCode.ToString().ToUpper() == "R")
				{
					RemoteControl_Click(sender, e);
				}
				else if (e.Control && e.KeyCode == Keys.Enter)
				{
					miSendComputers.PerformClick();
				}
				else if (e.Control && e.KeyCode.ToString().ToUpper() == "Q")
				{
					tbFind.Text = "";
					NoFilter.PerformClick();
				}
			}
			//else if (!tbFind.Focused)
			//{
			//	//tbFind.Focus();
			//	tbFind.Text = e.KeyCode.ToString();
			//}
		}

		private void ShowOtrs_Click(object sender, EventArgs e)
		{
			try
			{
				DataGridViewRow dgvr = rgvComputers.dataGridView.CurrentRow;
				if (dgvr != null)
				{
					string email = ConvertSafe.ToString(dgvr.Cells["Email"].Value);
					if (!string.IsNullOrEmpty(email))
					{
						Process.Start($"{_otrsLink}={email}");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				MLogger.Error(ex.ToString());
			}
		}

		private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Char.IsLetterOrDigit(e.KeyChar) && !tbFind.Focused && tabControl1.SelectedTab == tabPageShops && rgvComputers.dataGridView.Focused)
			{
				tbFind.Text += e.KeyChar.ToString();
				tbFind.Focus();
				tbFind.Select(tbFind.Text.Length, 0);
			}
		}

		private void miCreateOperationGroup_Click(object sender, EventArgs e)
		{
			CreateOrEditOperationGroupForm createOperationGroupForm = new CreateOrEditOperationGroupForm();
			if (createOperationGroupForm.ShowDialog() == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(createOperationGroupForm.GroupName))
				{
					var res = MQueryCommand.CreateOperationGroup(createOperationGroupForm.GroupName, createOperationGroupForm.Description);
					if (!res.IsComplete)
					{
						MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						InitCbOperationGroups();
					}
				}
			}
		}

		private void miEditOperationGroup_Click(object sender, EventArgs e)
		{
			OperationGroupsModel[] operationGroupsModels = cbOperationGroups.DataSource as OperationGroupsModel[];
			if (operationGroupsModels == null)
			{
				MessageBox.Show("Ошибка получения текущей группы.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			OperationGroupsModel operationGroupsModel = operationGroupsModels.FirstOrDefault(f => f.Id == Convert.ToInt32(cbOperationGroups.SelectedValue));
			if (operationGroupsModel == null)
			{
				MessageBox.Show("Ошибка получения текущей группы.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			CreateOrEditOperationGroupForm editOperationGroupForm = new CreateOrEditOperationGroupForm(operationGroupsModel);
			if (editOperationGroupForm.ShowDialog() == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(editOperationGroupForm.GroupName))
				{
					var res = MQueryCommand.EditOperationGroup(Convert.ToInt32(cbOperationGroups.SelectedValue), editOperationGroupForm.GroupName, editOperationGroupForm.Description);
					if (!res.IsComplete)
					{
						MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						InitCbOperationGroups();
					}
				}
			}
		}

		private void miRemoveOperationGroup_Click(object sender, EventArgs e)
		{
			if (cbOperationGroups.SelectedValue != null)
			{
				int selectedGroupId = Convert.ToInt32(cbOperationGroups.SelectedValue);
				if (MessageBox.Show($"Удалить группу [{((OperationGroupsModel)cbOperationGroups.SelectedItem).Name}]?", Application.ProductName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					var res = MQueryCommand.RemoveOperationGroup(selectedGroupId);
					if (!res.IsComplete)
					{
						MessageBox.Show(res.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						InitCbOperationGroups();
					}
				}
			}
		}

		private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
		{

		}

		private void btnAddEditOperationGroup_Click(object sender, EventArgs e)
		{
			cmsOperationGroups.Show(cbOperationGroups, 0, cbOperationGroups.Height);
		}

		private void tsmiExecutionToPlan_Click(object sender, EventArgs e)
		{

		}

		private async void miUpdateAllAgents_Click(object sender, EventArgs e)
		{
			if (_isAllAgentsUpdateFormShowing)
			{
				return;
			}

			_isAllAgentsUpdateFormShowing = true;
			if (sbStatus.InvokeRequired)
			{
				sbStatus.Invoke(new MethodInvoker(() => miUpdateAllAgents.Enabled = false));
			}
			else
			{
				miUpdateAllAgents.Enabled = false;
			}

			try
			{
				ConfirmationWithTableForm cf = new ConfirmationWithTableForm();
				cf.rgvInfo.IsMultiselect = false;
				cf.rgvInfo.KeyField = "ComputerId";
				cf.rgvInfo.MappingColumns = Mapping.ComputersGridColMap;
				cf.rgvInfo.DataSource = await MQueryCommand.SelectShopComputersServerViewGridAsync();
				cf.OperationTypeName = OperationTools.TryGetOperationNameRuByEn("SelfUpdate");
				cf.rgvInfo.Filter = $"MagicUpdaterVersion <> '{LastAgentVersionChecker.GetLatestVersion()}' and IsON = 1";
				cf.rgvInfo.HideColumns("LicStatusBitmap");
				if (cf.ShowDialog() == DialogResult.OK)
				{
					lastOperationDateUtc = DateTime.UtcNow;
					foreach (DataGridViewRow row in cf.rgvInfo.dataGridView.Rows)
					{
						new OperationBase(OperationTools.GetOperationIdByName("SelfUpdate")).SendForComputer(Convert.ToInt32(row.Cells["ComputerId"].Value), lastOperationDateUtc);
					}
					SetOperationsLastFilter(LastOperationDateLocal);
				}
			}
			finally
			{
				_isAllAgentsUpdateFormShowing = false;
				if (sbStatus.InvokeRequired)
				{
					sbStatus.Invoke(new MethodInvoker(() => miUpdateAllAgents.Enabled = true));
				}
				else
				{
					miUpdateAllAgents.Enabled = true;
				}
			}
		}

		private void miShedulerForAgents_Click(object sender, EventArgs e)
		{
			//new ScheduleForm().ShowDialog();
			tsbScheduler.Enabled = false;
			try
			{
				new ShedulerFormNew().ShowDialog();
			}
			finally
			{
				tsbScheduler.Enabled = true;
			}
		}

		private void miShedulerFOrServer_Click(object sender, EventArgs e)
		{
			tsbScheduler.Enabled = false;
			try
			{
				new ShedulerPluginForm().ShowDialog();
			}
			finally
			{
				tsbScheduler.Enabled = true;
			}
		}

		private void miGetLicForAllAgents_Click(object sender, EventArgs e)
		{
			var agents = MQueryCommand.SelectShopComputersServerViewGrid().Where(w => (w.LicStatus ?? -1) == -1 && w.ComputerId > 0 && w.IsClosed == false).ToArray();
			AgentLicForm agentLicForm = new AgentLicForm(agents);
			agentLicForm.ShowDialog();
		}

		private void miGetLicForSelectedAgents_Click(object sender, EventArgs e)
		{
			var selectedAgentsIds = rgvComputers.SelectedValues.Select(s => Convert.ToInt32(s)).ToArray();

			var agents = MQueryCommand.SelectShopComputersServerViewGrid().Where(w => /*(w.LicStatus ?? -1) == -1 &&*/ w.ComputerId > 0 && w.IsClosed == false && selectedAgentsIds.Contains(w.ComputerId)).ToArray();
			if (agents.Length == 0)
			{
				MessageBox.Show("Агенты не выбраны", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			AgentLicForm agentLicForm = new AgentLicForm(agents);
			agentLicForm.ShowDialog();
		}

		private void miAbout_Click(object sender, EventArgs e)
		{
			new AboutForm().ShowDialog();
		}

		private void miCheckUpdates_Click(object sender, EventArgs e)
		{
			new CheckUpdatesForm().ShowDialog();
		}
	}
}