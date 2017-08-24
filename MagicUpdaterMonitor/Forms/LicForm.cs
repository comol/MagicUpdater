using MagicUpdater.DL.DB;
using MagicUpdaterCommon.Data;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class LicForm : BaseForm
	{
		private static readonly string LOGIN = "|$login$|";
		private static readonly string PASS = "|$pass$|";
		private static readonly string LICTYPE = "|$lictype$|";
		private static readonly string HWID = "|$hwid$|";
		private static readonly string PCCOUNT = "|$pccount$|";
		private static readonly string COUNT_LINK_PATTERN = "api/Lic/?asdasdrewfge=|$login$|&sdfkngsiweoroi=|$pass$|&slkmdfglksdf=|$lictype$|";
		private static readonly string LIC_LINK_PATTERN = "api/Lic/?ertggb=|$login$|&hifdiug=|$pass$|&ihysbdb=|$hwid$|&sdijfngoien=|$pccount$|&bdefgolro=|$lictype$|";

		private CommonGlobalSettings _commonGlobalSettings;

		public LicForm()
		{
			InitializeComponent();
			_commonGlobalSettings = new CommonGlobalSettings();
			var resCommonGlobalSettings = _commonGlobalSettings.LoadCommonGlobalSettings();
			if (!resCommonGlobalSettings.IsComplete)
			{
				MessageBox.Show(resCommonGlobalSettings.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			tbLogin.Text = _commonGlobalSettings.LicLogin;
			tbPassword.Text = _commonGlobalSettings.LicPassword;
			tbLicLink.Text = _commonGlobalSettings.LicLink;
		}

		private bool IsChangeExists()
		{
			return !(tbLogin.Text == (_commonGlobalSettings.LicLogin ?? "")
			&& tbPassword.Text == (_commonGlobalSettings.LicPassword ?? "")
			&& tbLicLink.Text == (_commonGlobalSettings.LicLink.ToString() ?? ""));
		}

		private void LicForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			btnOk.Enabled = false;
			try
			{
				switch (this.DialogResult)
				{
					case DialogResult.OK:
						string login = tbLogin.Text;

						MD5 sec = new MD5CryptoServiceProvider();
						byte[] bt = Encoding.ASCII.GetBytes(tbPassword.Text);
						byte[] hash = sec.ComputeHash(bt);
						StringBuilder sb = new StringBuilder();
						for (int i = 0; i < hash.Length; i++)
						{
							sb.Append(hash[i].ToString("X2"));
						}

						string pass = sb.ToString();
						string link = tbLicLink.Text;
						if (!string.IsNullOrEmpty(link) && link.Length > 0 && link.Last() != '/')
						{
							link = $"{link}/";
						}

						var resMonitorCount = LicGetter.GetLicFromWeb($"{link}{COUNT_LINK_PATTERN.Replace(LOGIN, login).Replace(PASS, pass).Replace(LICTYPE, "1")}");
						if (!resMonitorCount.IsComplete)
						{
							MessageBox.Show(resMonitorCount.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}

						if (resMonitorCount.Result.Status != LicResponceStatus.LicIdOk)
						{
							switch (resMonitorCount.Result.Status)
							{
								case LicResponceStatus.LicLimitOver:
									MessageBox.Show("Лимит лицензий исчерпан", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								case LicResponceStatus.ErrorAuth:
									MessageBox.Show("Ошибка авторизации", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								default:
									MessageBox.Show(resMonitorCount.Result.Status.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
							}
							
							e.Cancel = true;
							return;
						}

						var resAgentCount = LicGetter.GetLicFromWeb($"{link}{COUNT_LINK_PATTERN.Replace(LOGIN, login).Replace(PASS, pass).Replace(LICTYPE, "2")}");
						if (!resAgentCount.IsComplete)
						{
							MessageBox.Show(resAgentCount.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}

						if (resAgentCount.Result.Status != LicResponceStatus.LicIdOk)
						{
							switch (resAgentCount.Result.Status)
							{
								case LicResponceStatus.LicLimitOver:
									MessageBox.Show("Лимит лицензий исчерпан", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								case LicResponceStatus.ErrorAuth:
									MessageBox.Show("Ошибка авторизации", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								default:
									MessageBox.Show(resMonitorCount.Result.Status.ToString(), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
							}

							e.Cancel = true;
							return;
						}

						var resPcCount = MQueryCommand.TryUpdatePcCounts(resMonitorCount.Result.PcCount, resAgentCount.Result.PcCount);
						if (!resPcCount.IsComplete)
						{
							MessageBox.Show(resPcCount.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}

						CommonGlobalSettings commonGlobalSettings = new CommonGlobalSettings();
						var resCommonGlobalSettings = commonGlobalSettings.LoadCommonGlobalSettings();
						if (!resCommonGlobalSettings.IsComplete)
						{
							MessageBox.Show(resCommonGlobalSettings.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
							e.Cancel = true;
							return;
						}

						var resLic = LicGetter.GetLicFromWeb($"{link}{LIC_LINK_PATTERN.Replace(LOGIN, login).Replace(PASS, pass).Replace(HWID, MainForm.HwId).Replace(PCCOUNT, commonGlobalSettings.LicMonitorCount).Replace(LICTYPE, "1")}");
						if (resLic.IsComplete)
						{
							switch (resLic.Result.Status)
							{
								case LicResponceStatus.LicIdOk:
									var resUpdateMonitorLic = MQueryCommand.TryUpdateMonitorLic(MainForm.UserId, resLic.Result);
									if (!resUpdateMonitorLic.IsComplete)
									{
										MessageBox.Show(resUpdateMonitorLic.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
										e.Cancel = true;
										return;
									}

									_commonGlobalSettings.LicLink = tbLicLink.Text;
									_commonGlobalSettings.LicLogin = login;
									_commonGlobalSettings.LicPassword = pass;
									_commonGlobalSettings.LicMonitorCount = resMonitorCount.Result.PcCount.ToString();
									_commonGlobalSettings.LicAgentsCount = resAgentCount.Result.PcCount.ToString();

									if (!SqlWorks.SaveCommonGlobalSettingsToBase(_commonGlobalSettings))
									{
										MessageBox.Show("Ошибка сохранения CommonGlobalSettings", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									}

									MessageBox.Show("Успешное получение лицензии", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
									break;
								case LicResponceStatus.LicDeletingOk:
									break;
								case LicResponceStatus.LicLimitOver:
									MessageBox.Show("Лимит лицензий исчерпан", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
									break;
								case LicResponceStatus.ErrorAuth:
									MessageBox.Show("Ошибка авторизации", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
							e.Cancel = true;
							return;
						}
						break;
					case DialogResult.Cancel:
						break;
				}
			}
			finally
			{
				btnOk.Enabled = true;
			}
		}

		private void TbAll_TextChanged(object sender, EventArgs e)
		{
			//btnOk.Enabled = IsChangeExists();
		}
	}
}
