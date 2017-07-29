using MagicUpdater.DL;
using MagicUpdater.DL.DB;
using MagicUpdater.DL.Models;
using MagicUpdater.DL.Tools;
using MagicUpdaterCommon.Abstract;
using MagicUpdaterCommon.Helpers;
using MagicUpdaterCommon.SettingsTools;
using MagicUpdaterMonitor.Abstract;
using MagicUpdaterMonitor.Base;
using MagicUpdaterMonitor.Helpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Forms
{
	public partial class NewEditShedulerStepForm : BaseForm
	{
		private int _stepId;
		private ViewShedulerStepModel _viewShedulerStepModel;
		private List<ViewShedulerStepModel> _viewShedulerStepModelList;

		public ViewShedulerStepModel OutViewShedulerStepModel { get; private set; } = null;

		public NewEditShedulerStepForm(List<ViewShedulerStepModel> viewShedulerStepModelList, ViewShedulerStepModel viewShedulerStepModel = null)
		{
			_viewShedulerStepModel = viewShedulerStepModel;
			_viewShedulerStepModelList = viewShedulerStepModelList;
			InitializeComponent();
			if (_viewShedulerStepModel != null)
			{
				_stepId = _viewShedulerStepModel.Id;
			}
			else
			{
				_viewShedulerStepModel = new ViewShedulerStepModel();
			}
			InitControls();
		}

		private void InitControls()
		{
			tabControl1.Appearance = TabAppearance.FlatButtons;
			tabControl1.ItemSize = new Size(0, 1);
			tabControl1.SizeMode = TabSizeMode.Fixed;

			cbOperation.DisplayMember = "DisplayGridName";
			cbOperation.ValueMember = "Id";
			List<OperationTypeModel> operationTypeModels = new List<OperationTypeModel>();
			operationTypeModels.Add(new OperationTypeModel
			{
				Id = 0,
				DisplayGridName = "(не выбрано)"
			});
			operationTypeModels.AddRange(MQueryCommand.GetOperationTypes());
			cbOperation.DataSource = operationTypeModels.Where(w => w.Visible.HasValue ? w.Visible.Value : false).ToList();

			if (_stepId != 0)
			{
				cbOperation.SelectedValue = _viewShedulerStepModel.OperationType;
				//cbOperation.Enabled = false;
			}

			operationAttributes1.OnValueChanged += (sender, e) =>
			{
				int operTypeId = Convert.ToInt32(cbOperation.SelectedValue);
				if (operTypeId <= 0)
				{
					return;
				}

				if (!e.IsTypeError)
				{
					string jsonModel = NewtonJson.GetJsonFromModel(e.Model);

					if (string.IsNullOrEmpty(jsonModel))
					{
						MessageBox.Show("Ошибка формирования json из модели", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

					_viewShedulerStepModel.OperationAttributes = jsonModel;
				}
				else
				{
					MessageBox.Show(e.TypeErrorText, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			};

			cbSuccess.DisplayMember = "nameVisCount";
			cbSuccess.ValueMember = "Id";
			var cbSuccessList = _viewShedulerStepModelList?.Where(w => w.Step > _viewShedulerStepModel.Step).ToList();
			if (cbSuccessList == null)
			{
				cbSuccessList = new List<ViewShedulerStepModel>();
			}
			cbSuccessList.Add(new ViewShedulerStepModel
			{
				Id = 0,
				nameVisCount = "Завершить"
			});
			cbSuccess.DataSource = cbSuccessList;

			cbFailure.DisplayMember = "nameVisCount";
			cbFailure.ValueMember = "Id";
			var cbFailureList = _viewShedulerStepModelList?.Where(w => w.Step > _viewShedulerStepModel.Step).ToList();
			if (cbFailureList == null)
			{
				cbFailureList = new List<ViewShedulerStepModel>();
			}
			cbFailureList.Add(new ViewShedulerStepModel
			{
				Id = 0,
				nameVisCount = "Завершить"
			});
			cbFailure.DataSource = cbFailureList;

			if (_stepId != 0)
			{
				cbSuccess.SelectedValue = _viewShedulerStepModel.OnOperationCompleteStep;
				cbFailure.SelectedValue = _viewShedulerStepModel.OnOperationErrorStep;
				nudCheckOperationResultInterval.Value = _viewShedulerStepModel.OperationCheckIntervalMs / 1000 / 60;
				nudError1CRepeatCount.Value = Convert.ToDecimal(_viewShedulerStepModel.RepeatCount);
				nudError1CCheckInterval.Value = (_viewShedulerStepModel.RepeatTimeout.HasValue ? _viewShedulerStepModel.RepeatTimeout.Value : 0) / 1000 / 60;
			}
		}

		private void lvPages_SelectedIndexChanged(object sender, EventArgs e)
		{
			var selectedItem = lvPages.Items.OfType<ListViewItem>().FirstOrDefault(f => f.Selected);
			if (selectedItem == null)
			{
				return;
			}
			switch (selectedItem.Text)
			{
				case "Общие":
					tabControl1.SelectedTab = tabPageGeneral;
					break;
				case "Цепочка":
					tabControl1.SelectedTab = tabPageChain;
					break;
			}
		}

		private void tableLayoutPanel5_Paint(object sender, PaintEventArgs e)
		{

		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			#region Редактируемый шаг
			_viewShedulerStepModel.OperationType = Convert.ToInt32(cbOperation.SelectedValue);
			_viewShedulerStepModel.nameVis = Convert.ToString(cbOperation.Text);
			_viewShedulerStepModel.nameVisCount = $"[{_viewShedulerStepModel.Step}] {_viewShedulerStepModel.nameVis}";
			if (cbSuccess.SelectedValue != null)
			{
				_viewShedulerStepModel.OnOperationCompleteStep = (int)cbSuccess.SelectedValue;
				_viewShedulerStepModel.nameVisCountCompleteStep = cbSuccess.Text;
			}
			if (cbFailure.SelectedValue != null)
			{
				_viewShedulerStepModel.OnOperationErrorStep = (int)cbFailure.SelectedValue;
				_viewShedulerStepModel.nameVisCountErrorStep = cbFailure.Text;
			}
			_viewShedulerStepModel.OperationCheckIntervalMs = Convert.ToInt32(nudCheckOperationResultInterval.Value) * 1000 * 60;
			_viewShedulerStepModel.RepeatCount = Convert.ToInt32(nudError1CRepeatCount.Value);
			_viewShedulerStepModel.RepeatTimeout = Convert.ToInt32(nudError1CCheckInterval.Value) * 1000 * 60;

			string jsonModel = NewtonJson.GetJsonFromModel(operationAttributes1.GetCurrentModel());

			if (!string.IsNullOrEmpty(jsonModel))
			{
				_viewShedulerStepModel.OperationAttributes = jsonModel;
			}

			OutViewShedulerStepModel = _viewShedulerStepModel;
			#endregion
		}

		private void NewEditShedulerStepForm_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		private void InitOperationAttributes(int operationId)
		{
			if (operationId == 0)
			{
				operationAttributes1.InitializeControl(null);
				return;
			}

			_viewShedulerStepModel.nameVis = cbOperation.Text;

			string fileName = OperationTools.GetOperationFileNameById(operationId);
			if (string.IsNullOrEmpty(fileName))
			{
				operationAttributes1.InitializeControl(null);
				return;
			}

			Type attrType = null;
			string operName = OperationTools.GetOperationNameEnById(operationId);
			string operFileName = OperationTools.GetOperationFileNameById(operationId);
			string operFileMd5 = OperationTools.GetOperationFileMd5ById(operationId);

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
				JObject savedModel = NewtonJson.GetModelFromJson(_viewShedulerStepModel.OperationAttributes) as JObject;
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

				operationAttributes1.InitializeControl(operationAttributesInstance, operationId);
			}
			else
			{
				operationAttributes1.InitializeControl(null);
			}
		}

		private void cbOperation_SelectedValueChanged(object sender, EventArgs e)
		{
			int id = Convert.ToInt32(cbOperation.SelectedValue);
			//_viewShedulerStepModel.OperationType = id;

			InitOperationAttributes(id);
		}
	}
}
