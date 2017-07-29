using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using MagicUpdaterMonitor.Helpers;
using MagicUpdater.DL.Helpers;
using MagicUpdaterCommon.Abstract;

namespace MagicUpdaterMonitor.Controls
{
	public partial class OperationAttributes : UserControl
	{
		public class ControlRow
		{
			public string ControlName { get; set; }
			public object ControlValue { get; set; }
			public string PropertyName { get; set; }
			public Type PropertyType { get; set; }
		}

		private bool _isParameterSaved = false;
		private Type _modelType;
		private List<ControlRow> _controlsValues = new List<ControlRow>();
		//private Dictionary<string, string> _controlsValues = new Dictionary<string, string>();
		public event EventHandler<OperationAttributesEventArgs> OnValueChanged;
		private int _operTypeId;

		public OperationAttributes()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Строим таблицу по модели
		/// </summary>
		public void InitializeControl(object model, int operTypeId = 0)
		{
			tlpPropertyControls.Controls.Clear();
			_operTypeId = operTypeId;
			tlpPropertyControls.RowCount = 1;
			_controlsValues.Clear();
			if (model == null)
			{
				return;
			}

			_modelType = model.GetType();

			foreach (var prop in model.GetType().GetProperties())
			{
				string displayName = null;
				bool isHidden = false;

				foreach (var attr in prop.GetCustomAttributes(true))
				{
					try
					{
						Attribute attribute = attr as Attribute;
						if(((Type)attribute.TypeId) == typeof(OperationAttributeHidden))
						{
							isHidden = true;
							break;
						}

						if (((Type)attribute.TypeId) == typeof(OperationAttributeDisplayName))
						{
							object operationAttributeDisplayName = attr;
							displayName = Convert.ToString(operationAttributeDisplayName.GetType().GetProperty("Name").GetValue(operationAttributeDisplayName));
						}
					}
					catch
					{
						continue;
					}
				}

				string rusPropTypeName = ExtTools.GetRussianTypeDisplayValue(prop.PropertyType);

				if (!isHidden)
				{
					CreateControlRow(prop.Name, prop.PropertyType, rusPropTypeName, displayName, prop.GetValue(model));
				}
			}
		}

		private object GetModel()
		{
			try
			{
				object model = Activator.CreateInstance(_modelType);

				foreach (var controlRow in _controlsValues)
				{
					object value = ExtTools.ConvertStringToType(controlRow.ControlValue.ToString(), controlRow.PropertyType);
					model.GetType().GetProperty(controlRow.PropertyName).SetValue(model, value);
				}

				return model;
			}
			catch (Exception ex)
			{
				MLogger.Error(ex.ToString());
				MessageBox.Show(ex.Message, "Ошибка атрибутов", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return null;
			}
		}

		private void CreateControlRow(string propertyName, Type propertyType, string rusPropName = "", string displayName = "", object tbDefValue = null)
		{
			string lbName = $"lb{propertyName}";
			string lbText = string.IsNullOrEmpty(displayName) ? propertyName : displayName;
			string tbName = $"tb{propertyName}";

			if (!string.IsNullOrEmpty(rusPropName))
			{
				lbText = $"{lbText}({rusPropName})";
			}

			tlpPropertyControls.Controls.Add(new Label
			{
				Name = lbName,
				Text = lbText,
				Dock = DockStyle.Fill,
				TextAlign = System.Drawing.ContentAlignment.MiddleLeft,
				AutoSize = false,
			}, 0, tlpPropertyControls.RowCount - 1);

			Control control;

			if (propertyType == typeof(bool))
			{
				control = new CheckBox
				{
					Name = tbName,
					Checked = (bool)tbDefValue,
					Dock = DockStyle.Fill,
				};

				CheckBox cb = (CheckBox)control;
				cb.CheckedChanged += Cb_CheckedChanged;
			}
			else
			{
				control = new TextBox
				{
					Name = tbName,
					Text = tbDefValue.ToString(),
					Dock = DockStyle.Fill,
				};

				TextBox tb = (TextBox)control;
				tb.LostFocus += Tb_LostFocus;
				tb.KeyDown += Tb_KeyDown;
				tb.TextChanged += Tb_TextChanged;

				if (propertyType == typeof(int))
				{
					tb.KeyPress += ((sender, e) =>
					{
						if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
						{
							e.Handled = true;
						}
					});
				}
				else if (propertyType == typeof(float) || propertyType == typeof(double))
				{
					tb.KeyPress += ((sender, e) =>
					{
						if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
						{
							e.Handled = true;
						}
						// only allow one decimal point
						if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
						{
							e.Handled = true;
						}
					});
				}
			}

			_controlsValues.Add(new ControlRow
			{
				ControlName = tbName,
				ControlValue = tbDefValue,
				PropertyName = propertyName,
				PropertyType = propertyType
			});

			tlpPropertyControls.Controls.Add(control, 1, tlpPropertyControls.RowCount - 1);

			tlpPropertyControls.RowCount++;
		}

		private void Cb_CheckedChanged(object sender, EventArgs e)
		{
			SendModel(sender);
		}

		private void Tb_TextChanged(object sender, EventArgs e)
		{
			_isParameterSaved = false;
		}

		private bool IsTypeValid(string value, Type type)
		{
			if (type == typeof(int))
			{
				int outVal;
				return int.TryParse(value, out outVal);
			}

			if (type == typeof(double))
			{
				double outVal;
				return double.TryParse(value, out outVal);
			}

			if (type == typeof(float))
			{
				float outVal;
				return float.TryParse(value, out outVal);
			}

			if (type == typeof(string))
			{
				return true;
			}

			return false;
		}

		private void SendModel(object sender)
		{
			Control control = sender as Control;
			if (control != null)
			{
				var controlRow = _controlsValues.OfType<ControlRow>().FirstOrDefault(f => f.ControlName == control.Name);
				if (controlRow != null)
				{
					if (controlRow.PropertyType == typeof(bool))
					{
						CheckBox cb = (CheckBox)control;
						controlRow.ControlValue = cb.Checked;
						Model = GetModel();
						OnValueChanged?.Invoke(this, new OperationAttributesEventArgs
						{
							OperTypeId = _operTypeId,
							Model = Model
						});
						_isParameterSaved = true;
					}
					else if (IsTypeValid(control.Text, controlRow.PropertyType))
					{
						TextBox tb = (TextBox)control;
						controlRow.ControlValue = tb.Text;
						Model = GetModel();
						OnValueChanged?.Invoke(this, new OperationAttributesEventArgs
						{
							OperTypeId = _operTypeId,
							Model = Model
						});
						_isParameterSaved = true;
					}
					else
					{
						OnValueChanged?.Invoke(this, new OperationAttributesEventArgs
						{
							OperTypeId = 0,
							Model = null,
							IsTypeError = true,
							TypeErrorText = "Ошибочный тип параметра."
						});
					}
				}
			}
		}

		private void Tb_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter && !_isParameterSaved)
			{
				SendModel(sender);
			}
		}

		private void Tb_LostFocus(object sender, EventArgs e)
		{
			if (!_isParameterSaved)
			{
				SendModel(sender);
			}
		}

		private void tlpPropertyControls_SizeChanged(object sender, EventArgs e)
		{
			tlpHeader.Width = tlpPropertyControls.Width;
		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void pnScroll_Scroll(object sender, ScrollEventArgs e)
		{
		}

		public bool IsParameterSaved => _isParameterSaved;
		public List<ControlRow> Properties => _controlsValues;

		public object Model { get; private set; }

		public object GetCurrentModel()
		{
			if (_modelType != null)
			{
				return GetModel();
			}
			else
			{
				return null;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			CreateControlRow("Test", typeof(float), "Тест", "Тест", 0);
		}
	}

	public class OperationAttributesEventArgs
	{
		public bool IsTypeError { get; set; } = false;
		public string TypeErrorText { get; set; }
		public int OperTypeId { get; set; }
		public object Model { get; set; }
	}
}
