using MagicUpdater.DL.DB;
using MagicUpdater.DL.Models;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class AgentLicForm : BaseForm
	{
		public class AgentLicFormModel
		{
			public string ShopName { get; set; }
			public int ComputerId { get; set; }
			public string ComputerName { get; set; }
			public string LicMessage { get; set; }
		}

		public static Dictionary<string, string> LicAgentGridColMap = new Dictionary<string, string>
		{
			{"ShopName","Имя магазина"},
			{"ComputerId","ID компьютера"},
			{"ComputerName"," Имя компьютера"},
			{"LicMessage","Сообщение"}
		};

		private static readonly string LOGIN = "|$login$|";
		private static readonly string PASS = "|$pass$|";
		private static readonly string LICTYPE = "|$lictype$|";
		private static readonly string HWID = "|$hwid$|";
		private static readonly string PCCOUNT = "|$pccount$|";
		private static readonly string COUNT_LINK_PATTERN = "api/Lic/?asdasdrewfge=|$login$|&sdfkngsiweoroi=|$pass$|&slkmdfglksdf=|$lictype$|";
		private static readonly string LIC_LINK_PATTERN = "api/Lic/?ertggb=|$login$|&hifdiug=|$pass$|&ihysbdb=|$hwid$|&sdijfngoien=|$pccount$|&bdefgolro=|$lictype$|";

		private ShopComputersModel[] _shopComputersModel;
		private CommonGlobalSettings _commonGlobalSettings;
		private List<AgentLicFormModel> _agentLicFormModelList = new List<AgentLicFormModel>();
		public AgentLicForm(ShopComputersModel[] shopComputersModel)
		{
			_shopComputersModel = shopComputersModel;
			_commonGlobalSettings = new CommonGlobalSettings();
			_commonGlobalSettings.LoadCommonGlobalSettings();
			InitializeComponent();
			rgvLicAgent.KeyField = "ComputerId";
			rgvLicAgent.dataGridView.RowHeadersVisible = false;
			rgvLicAgent.MappingColumns = LicAgentGridColMap;
		}

		private async void AgentLicForm_Shown(object sender, EventArgs e)
		{
			btnOk.Enabled = false;

			await Task.Factory.StartNew(() =>
			{
				Thread.Sleep(1000);
			});

			try
			{
				string link = _commonGlobalSettings.LicLink;
				if (!string.IsNullOrEmpty(link) && link.Length > 0 && link.Last() != '/')
				{
					link = $"{link}/";
				}
				string login = _commonGlobalSettings.LicLogin;
				string pass = _commonGlobalSettings.LicPassword;

				var resAgentCount = LicGetter.GetLicFromWeb($"{link}{COUNT_LINK_PATTERN.Replace(LOGIN, login).Replace(PASS, pass).Replace(LICTYPE, "2")}");
				if (!resAgentCount.IsComplete)
				{
					MessageBox.Show(resAgentCount.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				int webAgentCount = resAgentCount.Result.PcCount;

				foreach (var agent in _shopComputersModel)
				{
					var resLic = LicGetter.GetLicFromWeb($"{link}{LIC_LINK_PATTERN.Replace(LOGIN, login).Replace(PASS, pass).Replace(HWID, MainForm.HwId).Replace(PCCOUNT, webAgentCount.ToString()).Replace(LICTYPE, "2")}");
					if (resLic.IsComplete)
					{
						switch (resLic.Result.Status)
						{
							case LicResponceStatus.LicIdOk:
								var resUpdateAgentLic = MQueryCommand.TryUpdateAgentLic(agent.ComputerId, resLic.Result);
								if (!resUpdateAgentLic.IsComplete)
								{
									_agentLicFormModelList.Add(new AgentLicFormModel
									{
										ComputerId = agent.ComputerId,
										ComputerName = agent.ComputerName,
										ShopName = agent.ShopName,
										LicMessage = resUpdateAgentLic.Message
									});

									//MessageBox.Show(resUpdateAgentLic.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								}

								_agentLicFormModelList.Add(new AgentLicFormModel
								{
									ComputerId = agent.ComputerId,
									ComputerName = agent.ComputerName,
									ShopName = agent.ShopName,
									LicMessage = "Успешное получение лицензии"
								});

								//MessageBox.Show("Успешное получение лицензии", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
								break;
							case LicResponceStatus.LicDeletingOk:
								break;
							case LicResponceStatus.LicLimitOver:
								_agentLicFormModelList.Add(new AgentLicFormModel
								{
									ComputerId = agent.ComputerId,
									ComputerName = agent.ComputerName,
									ShopName = agent.ShopName,
									LicMessage = "Лимит лицензий исчерпан"
								});

								//MessageBox.Show("Лимит лицензий исчерпан", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								break;
							case LicResponceStatus.ErrorAuth:
								_agentLicFormModelList.Add(new AgentLicFormModel
								{
									ComputerId = agent.ComputerId,
									ComputerName = agent.ComputerName,
									ShopName = agent.ShopName,
									LicMessage = "Ошибка авторизации"
								});

								//MessageBox.Show("Ошибка авторизации", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
								break;
							case LicResponceStatus.ErrorLicDeleting:
								break;
							case LicResponceStatus.ErrorLicIdCreation:
								break;
							case LicResponceStatus.ErrorLicParameters:
								break;
							case LicResponceStatus.ErrorHwId:
								break;
							case LicResponceStatus.ErrorPcCount:
								break;
							case LicResponceStatus.ErrorLicType:
								break;
							default:
								break;
						}
					}
					else
					{
						MessageBox.Show(resLic.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}

					if (rgvLicAgent.DataSource == null)
					{
						rgvLicAgent.DataSource = _agentLicFormModelList.ToArray();
					}
					else
					{
						await rgvLicAgent.RefreshDataSourceAsync(_agentLicFormModelList.ToArray());
					}

					rgvLicAgent.dataGridView.Invalidate();
				}
			}
			finally
			{
				btnOk.Enabled = true;
			}
		}
	}
}
