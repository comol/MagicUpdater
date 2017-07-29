using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;
using MagicUpdater.DL.Helpers;
using MagicUpdater.DL.Common;
using MagicUpdaterMonitor.Helpers;
using System.Drawing;
using MagicUpdaterMonitor.Forms;
using System.ComponentModel;

namespace MagicUpdaterMonitor.Controls
{
	public partial class RefreshingGridView : UserControl
	{
		private object lockObj = new object();
		private bool _contextFilterMenuItemsVisible = true;
		private DataGridViewCell _currentRightClickCell;
		private string _currentColumnName;
		private bool isUpdating = false;
		private bool isFiltering = false;
		private bool _isAddSelectedValue = true;
		private string _filterByCurrentValue = "";
		private Object lockUpdateObj = new Object();
		private Object lockFilterObj = new Object();
		private int selectedRowIndex = 0;
		private bool _stopExportToexcelFlag = false;
		private List<string> hidingColumns = new List<string>();
		private string _keyField = null;
		private object _selectedValue = null;

		public object SelectedValue
		{
			get
			{
				return _selectedValue;
				//if (dataGridView.Rows.Count > 0 && dataGridView.CurrentRow != null && !string.IsNullOrEmpty(_keyField))
				//{
				//	return dataGridView.CurrentRow.Cells[_keyField].Value;
				//}
				//else
				//{
				//	return null;
				//}
			}
		}
		private List<object> _selectedValues = new List<object>();
		public List<object> SelectedValues
		{
			get
			{
				//if (_selectedValues.Count == 0)
				//{
				if (dataView != null && !string.IsNullOrEmpty(_keyField) && dataGridView.Columns["Selected"].Visible)
				{
					DataGridViewRow[] rows = dataGridView.Rows.OfType<DataGridViewRow>().Where(w => Convert.ToBoolean(w.Cells["Selected"].Value)).ToArray();
					return rows.Select(s => s.Cells[_keyField].Value).ToList();
				}
				else
				{
					//return _selectedValues;
					return new List<object>();
				}
				//}
				//else
				//{
				//	return _selectedValues;
				//}
			}
		}
		private bool isMultiselect = true;
		private object[] dataSource = null;
		private DataView dataView = null;
		private string _filter = "";
		private string _baseFilter = "";
		private CellExtensionForm cellExtensionForm = null;
		private List<string> _columnsToHide = new List<string>();
		private List<string> _columnsForTimeZoneCorrection = new List<string>();
		private TimeSpan _utcOffset;
		public delegate void PaintCellsDelegate(DataGridView sender);
		public event PaintCellsDelegate PaintCells;
		public event EventHandler ContextMenuOpening;
		public event EventHandler SelectedValueChanged;
		public event PaintEventHandler PaintGrid;

		public DataGridViewRow SelectedRow { get; private set; }


		public delegate void DetailsDelegate(RefreshingGridView sender);
		public event DetailsDelegate ShowDetailsEvent;
		public RadioButton ResetFilterRadioButton { get; set; } = null;

		private bool isDetailsEnabled = false;

		public Form DetailsForm { get; set; } = null;

		public bool IsDetailsEnabled
		{
			get { return isDetailsEnabled; }
			set
			{
				isDetailsEnabled = value;
				ShowDetailsField(value);
			}
		}

		public bool IsContextMenuVisible { get; set; } = true;

		public bool IsShowCellExtensionFormByDoubleClick { get; set; } = true;

		public DataView DataView
		{
			get
			{
				return dataView;
			}
			set
			{
				dataView = value;
				dataGridView.DataSource = dataView;
			}
		}

		public Dictionary<string, string> MappingColumns { get; set; } = null;

		public object[] DataSource
		{
			get { return dataSource; }
			set
			{
				dataSource = value;
				dataView = ArrayToDataView(value);
				dataGridView.DataSource = dataView;

				if (dataView != null && dataView.Table.Columns.Contains(_keyField))
				{
					DataColumn[] keys = new DataColumn[1];
					keys[0] = dataView.Table.Columns[_keyField];
					dataView.Table.PrimaryKey = keys;

					foreach (DataGridViewColumn col in dataGridView.Columns)
					{
						if (_columnsToHide.IndexOf(col.Name) >= 0)
						{
							col.Visible = false;
						}
					}

					PaintCells?.Invoke(dataGridView);
				}
				//dataGridView.Visible = true;
			}
		}

		public string BaseFilter
		{
			get
			{
				return _baseFilter;
			}
			set
			{
				_baseFilter = value;
				if (dataView != null)
				{
					lock (lockFilterObj)
					{
						SetFilterAsync(string.IsNullOrEmpty(_baseFilter) ? _filter : string.IsNullOrEmpty(_filter) ? _baseFilter : $"{_baseFilter} AND {_filter}");
					}
				}
			}
		}

		public string Filter
		{
			get { return _filter; }
			set
			{
				_filter = value;
				lbFilter.Text = $"Применен фильтр: {_filter}";
				if (string.IsNullOrEmpty(_filter))
				{
					lbFilter.Text = "";
				}
				if (dataView != null)
				{
					//dataView.RowFilter = filter;
					//lbCount.Text = $"Количество строк: {dataGridView.RowCount}";
					lock (lockFilterObj)
					{
						SetFilterAsync(string.IsNullOrEmpty(_baseFilter) ? _filter : string.IsNullOrEmpty(_filter) ? _baseFilter : $"{_baseFilter} AND {_filter}");
					}
					//CorrectSelectedValuesAfterFiltering();
					//ShowSelectedField(isMultiselect);
					//PaintCells?.Invoke(dataGridView);					
				}
			}
		}

		private async void SetFilterAsync(string filter)
		{
			Action act1 = new Action(() =>
			{
				do
				{
					Thread.Sleep(100);
				}
				while (isFiltering);
			});
			await Task.Run(act1);

			isFiltering = true;
			dataGridView.Enabled = false;
			try
			{
				Action act = new Action(() =>
				{
					do
					{
						Thread.Sleep(100);
					}
					while (isUpdating);
				});
				await Task.Run(act);
				btnClearFilters.Enabled = !string.IsNullOrEmpty(filter);
				dataView.RowFilter = filter;
				lbCount.Text = $"Количество строк: {dataGridView.RowCount}";
			}
			finally
			{
				isFiltering = false;
				dataGridView.Enabled = true;
			}

			//await Task.Factory.StartNew(() =>
			//{
			CorrectSelectedValues();
			ShowSelectedField(isMultiselect);
			PaintCells?.Invoke(dataGridView);
			//GC.Collect();
			//});
		}

		public void Repaint()
		{
			PaintCells?.Invoke(dataGridView);
		}

		public bool IsMultiselect
		{
			get { return isMultiselect; }
			set
			{
				isMultiselect = value;
				ShowSelectedField(value);
			}
		}

		public bool IsColumnFilteringEnabled { get; set; } = true;

		public string KeyField
		{
			get { return _keyField; }
			set
			{
				if (value == null)
					return;
				_keyField = value;

				if (dataView != null && dataView.Table.Columns.Contains(_keyField))
				{
					DataColumn[] keys = new DataColumn[1];
					keys[0] = dataView.Table.Columns[_keyField];
					dataView.Table.PrimaryKey = keys;
				}

				ShowSelectedField(isMultiselect);
			}
		}

		public RefreshingGridView()
		{
			InitializeComponent();
			ShowSelectedField(isMultiselect);
			dataGridView.Columns["Details"].Visible = false;
			lbLoading.Left = (pnLoading.Width - lbLoading.Width) / 2;
			lbLoading.Top = (pnLoading.Height - lbLoading.Height) / 2;
			_utcOffset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now);
		}

		public async Task RefreshDataSourceAsync<T>(T[] dsArr)
		{
			if (isUpdating)
				return;
			isUpdating = true;

			try
			{
				Action act = new Action(() =>
				{
					//lock (lockObj)
					//{
					if (isFiltering)
						return;

					DataView dgv = ArrayToDataView(dsArr);
					if (dgv == null || dataView == null)
						return;

					if (dgv.Table.Columns.Contains(_keyField))
					{
						DataColumn[] keys = new DataColumn[1];
						keys[0] = dgv.Table.Columns[_keyField];
						dgv.Table.PrimaryKey = keys;
					}

					//List<DataRow> tempListInsert = new List<DataRow>();

					//foreach (DataRow dr in dgv.Table.Rows)
					//{
					//	if (!dataView.Table.Rows.Contains(dr[keyField]))
					//	{
					//		tempListInsert.Add(dr);
					//	}
					//}

					//foreach (DataRow dr in tempListInsert)
					//{
					//	dataView.Table.ImportRow(dr);
					//}

					for (int i = dataView.Table.Rows.Count - 1; i >= 0; i--)
					{
						if (!dgv.Table.Rows.Contains(dataView.Table.Rows[i][_keyField]))
						{
							dataView.Table.Rows.Remove(dataView.Table.Rows[i]);
						}
					}

					//List<DataRow> tempListDelete = new List<DataRow>();
					//foreach (DataRow dr in dataView.Table.Rows)
					//{
					//	if (!dgv.Table.Rows.Contains(dr[keyField]))
					//	{
					//		tempListDelete.Add(dr);
					//	}
					//}

					//for (int i = tempListDelete.Count - 1; i >= 0; i--)
					//{
					//	dataView.Table.Rows.Remove(tempListDelete[i]);
					//}

					var rowsInsertUpdate = dgv.Table.AsEnumerable().Except(dataView.Table.AsEnumerable(), DataRowComparer.Default);
					List<DataRow> tempListInsert = new List<DataRow>();
					foreach (DataRow dr in rowsInsertUpdate)
					{
						if (dataView.Table.Rows.Contains(dr[_keyField]))
						{
							DataRow findRow = dataView.Table.Rows.OfType<DataRow>().FirstOrDefault(d => d[_keyField].ToString() == dr[_keyField].ToString());

							if (findRow != null)
							{
								for (int i = 0; i < dataView.Table.Columns.Count; i++)
								{
									if (findRow[i] != dr[i])
									{
										if (dataGridView.InvokeRequired)
										{
											this.Invoke(new MethodInvoker(() => findRow[i] = dr[i]));
										}
										else
										{
											findRow[i] = dr[i];
										}
										//findRow[i] = dr[i];
									}
								}
							}
						}
						else
						{
							tempListInsert.Add(dr);
							//dataView.Table.ImportRow(dr);
						}
					}

					foreach (DataRow dr in tempListInsert)
					{
						if (dataGridView.InvokeRequired)
						{
							this.Invoke(new MethodInvoker(() => dataView.Table.ImportRow(dr)));
						}
						else
						{
							dataView.Table.ImportRow(dr);
						}
					}
					//foreach (DataRow dr in dgv.Table.Rows)
					//{
					//	if (dataView.Table.Rows.Contains(dr[keyField]))
					//	{
					//		DataRow findRow = dataView.Table.Rows.OfType<DataRow>().FirstOrDefault(d => d[keyField].ToString() == dr[keyField].ToString());

					//		if (findRow != null)
					//		{
					//			for (int i = 0; i < dataView.Table.Columns.Count; i++)
					//			{
					//				if (findRow[i] != dr[i])
					//				{
					//					findRow[i] = dr[i];
					//				}
					//			}
					//		}
					//	}
					//}
					//});

					//await Task.Run(actUpdate);
					//if (this.InvokeRequired)
					//{
					//	this.Invoke(new MethodInvoker(() => PaintCells?.Invoke()));
					//	this.Invoke(new MethodInvoker(() => lbCount.Text = $"Количество строк: {dataGridView.RowCount}"));
					//}

					//}
					dgv.Dispose();
				});

				await Task.Run(act);
				PaintCells?.Invoke(dataGridView);
				if (lbCount.InvokeRequired)
				{
					this.Invoke(new MethodInvoker(() => lbCount.Text = $"Количество строк: {dataGridView.RowCount}"));
				}
				else
				{
					lbCount.Text = $"Количество строк: {dataGridView.RowCount}";
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Ошибка в методе RefreshDataSourceAsync Original: {ex.ToString()}");
				MessageBox.Show($"Ошибка в методе RefreshDataSourceAsync Original: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				try
				{
					DataGridViewRow selectedRow = null;
					if (dataGridView.SelectedRows != null && dataGridView.SelectedRows.Count > 0)
					{
						selectedRow = dataGridView.SelectedRows[0];
					}

					if (selectedRow != null && selectedRow.Cells[_keyField].Value.ToString() != _selectedValue.ToString())
					{
						_selectedValue = selectedRow.Cells[_keyField].Value;
						SelectedRow = selectedRow;
						//SelectedValueChanged?.Invoke(this, new EventArgs());
					}

					CorrectSelectedValues();
					//GC.Collect();
					isUpdating = false;
				}
				catch (Exception ex)
				{
					MLogger.Error($"Ошибка в методе RefreshDataSourceAsync блок finally. Original: {ex.ToString()}");
					MessageBox.Show($"Ошибка в методе RefreshDataSourceAsync блок finally. Original: {ex.ToString()}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void UpdateRow(DataRow updatedRow, DataRow sourceRow)
		{
			foreach (DataColumn col in updatedRow.Table.Columns)
			{
				updatedRow[col.ColumnName] = sourceRow[col.ColumnName];
			}
		}

		public void SelectAll()
		{
			if (dataGridView.Columns["Selected"].Visible)
			{
				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					if (Convert.ToInt32(row.Cells[KeyField].Value) > 0)
					{
						row.Cells["Selected"].Value = true;
					}
				}
			}
		}

		public void ClearSelection()
		{
			if (dataGridView.Columns["Selected"].Visible)
			{
				foreach (DataGridViewRow row in dataGridView.Rows)
				{
					row.Cells["Selected"].Value = false;
				}
			}
		}

		private void ShowSelectedField(bool val)
		{
			if (dataGridView.ColumnCount > 0)
			{
				try
				{
					if (!string.IsNullOrEmpty(_keyField))
					{
						dataGridView.Columns["Selected"].Visible = val;
						//cbSelectAll.Visible = false;
						dataGridView.Columns["Selected"].ReadOnly = !val;

					}
					else
					{
						//dataGridView.Columns["Selected"].Visible = cbSelectAll.Visible = false;
						dataGridView.Columns["Selected"].Visible = false;
					}
				}
				catch (Exception ex)
				{
					MLogger.Error($"Method: ShowSelectedField. {ex.ToString()}");
					MessageBox.Show($"Method: ShowSelectedField. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void ShowDetailsField(bool val)
		{
			if (dataGridView.ColumnCount > 0)
			{
				try
				{
					if (!string.IsNullOrEmpty(_keyField))
						dataGridView.Columns["Details"].Visible = val;
					else
						dataGridView.Columns["Details"].Visible = false;
				}
				catch (Exception ex)
				{
					MLogger.Error($"Method: ShowDetailsField. {ex.ToString()}");
					MessageBox.Show($"Method: ShowDetailsField. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private DataView ArrayToDataView<T>(T[] arr)
		{
			if (arr == null || arr.Length == 0 || arr[0] == null)
				return null;

			DataTable dataTable = new DataTable();
			PropertyInfo[] propsInfo = arr[0].GetType().GetProperties();

			foreach (PropertyInfo pi in propsInfo)
			{
				if (Attribute.IsDefined(pi, typeof(NotMapRefreshingGridView)))
				{
					if (_columnsToHide.IndexOf(pi.Name) < 0)
						_columnsToHide.Add(pi.Name);
				}

				if (Attribute.IsDefined(pi, typeof(SetCurrentTimeZome)))
				{
					if (_columnsForTimeZoneCorrection.IndexOf(pi.Name) < 0)
						_columnsForTimeZoneCorrection.Add(pi.Name);
				}

				Type type;
				DataColumn dc = new DataColumn();
				dc.AllowDBNull = true;
				dc.ColumnName = pi.Name;
				if (Nullable.GetUnderlyingType(pi.PropertyType) != null)
				{
					type = Nullable.GetUnderlyingType(pi.PropertyType);
				}
				else
				{
					type = pi.PropertyType;
				}

				dc.DataType = type;
				dataTable.Columns.Add(dc);
			}

			foreach (T obj in arr)
			{
				DataRow row = dataTable.NewRow();
				int i = 0;
				foreach (PropertyInfo pi in obj.GetType().GetProperties())
				{
					//if (Attribute.IsDefined(pi, typeof(NotMapRefreshingGridView)))
					//{
					//	if (_columnsToHide.IndexOf(pi.Name) < 0)
					//		_columnsToHide.Add(pi.Name);
					//}



					row[i] = pi.GetValue(obj) ?? DBNull.Value;
					i++;
				}
				dataTable.Rows.Add(row);
			}

			foreach (string colName in _columnsForTimeZoneCorrection)
			{
				if (dataTable.Columns[colName].DataType == typeof(DateTime))
				{
					foreach (DataRow row in dataTable.Rows)
					{
						DateTime? dateTime = row[colName] as DateTime?;

						if (dateTime.HasValue)
						{
							row[colName] = dateTime.Value.Add(_utcOffset);
						}
					}
				}
			}

			return dataTable.DefaultView;
		}

		private async Task<DataView> ArrayToDataViewAsync<T>(T[] arr)
		{
			if (arr == null || arr.Length == 0 || arr[0] == null)
				return null;

			DataTable dataTable = new DataTable();
			PropertyInfo[] propsInfo = arr[0].GetType().GetProperties();
			Action action = new Action(() =>
			{
				foreach (PropertyInfo pi in propsInfo)
				{
					if (Attribute.IsDefined(pi, typeof(NotMapRefreshingGridView)))
					{
						continue;
					}

					Type type;
					DataColumn dc = new DataColumn();
					dc.AllowDBNull = true;
					dc.ColumnName = pi.Name;
					if (Nullable.GetUnderlyingType(pi.PropertyType) != null)
					{
						type = Nullable.GetUnderlyingType(pi.PropertyType);
					}
					else
					{
						type = pi.PropertyType;
					}
					dc.DataType = type;
					dataTable.Columns.Add(dc);
				}

				foreach (T obj in arr)
				{
					DataRow row = dataTable.NewRow();
					int i = 0;
					foreach (PropertyInfo pi in obj.GetType().GetProperties())
					{
						if (Attribute.IsDefined(pi, typeof(NotMapRefreshingGridView)))
						{
							continue;
						}

						row[i] = pi.GetValue(obj) ?? DBNull.Value;
						i++;
					}
					dataTable.Rows.Add(row);
				}
			});
			await Task.Run(action);
			return dataTable.DefaultView;
		}


		public void HideColumns(params string[] names)
		{
			foreach (string name in names)
			{
				if (dataGridView.Columns.Contains(name) && !hidingColumns.Contains(name))
				{
					dataGridView.Columns[name].Visible = false;
					hidingColumns.Add(name);
				}
			}
		}
		public void ShowColumns(params string[] names)
		{
			foreach (string name in names)
			{
				if (dataGridView.Columns.Contains(name) && hidingColumns.Contains(name))
				{
					dataGridView.Columns[name].Visible = true;
					hidingColumns.Remove(name);
				}
			}
		}
		public void ShowAllColumns()
		{
			hidingColumns.Clear();
			foreach (DataGridViewColumn col in dataGridView.Columns.OfType<DataGridViewColumn>().Where(c => c.Name != "Selected" && c.Name != "Details"))
			{
				col.Visible = true;
			}
		}

		private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;
			if (dataGridView.Rows.Count > 0 && dataGridView.CurrentRow != null && !string.IsNullOrEmpty(_keyField))
			{
				SelectedRow = dataGridView.CurrentRow;
				_selectedValue = dataGridView.CurrentRow.Cells[_keyField].Value;
				SelectedValueChanged?.Invoke(this, new EventArgs());
			}
		}

		private void dataGridView_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{

			//SelectedValueChanged?.Invoke(this, new EventArgs());
			//DataGridView dgv = sender as DataGridView;

			//if (dgv.CurrentRow == null)
			//	return;

			//selectedRowIndex = dgv.CurrentRow.Index;
			//if (!string.IsNullOrEmpty(keyField))
			//{
			//	try
			//	{
			//		_selectedValue = dgv.CurrentRow.Cells[keyField].Value;
			//		SelectedValueChanged?.Invoke(this, new EventArgs());
			//	}
			//	catch { }
			//}
		}

		private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			cbSelectAll.CheckState = CheckState.Indeterminate;
			DataGridView dgv = sender as DataGridView;
			if (dgv.IsCurrentCellDirty)
			{
				dgv.CommitEdit(DataGridViewDataErrorContexts.Commit);
			}
		}

		private bool AddSelectedValue(object val)
		{
			if (!_selectedValues.Contains(val))
			{
				_selectedValues.Add(val);
				return true;
			}
			return false;
		}

		private bool RemoveSelectedValue(object val)
		{
			if (_selectedValues.Contains(val))
			{
				_selectedValues.Remove(val);
				return true;
			}
			return false;
		}

		private void CorrectSelectedValuesAfterFiltering()
		{
			if (dataView != null && !string.IsNullOrEmpty(_keyField) && dataGridView.Columns["Selected"].Visible)
			{
				try
				{
					_isAddSelectedValue = false;
					foreach (DataGridViewRow dgr in dataGridView.Rows)
					{
						dgr.Cells["Selected"].Value = false;
					}

					foreach (object val in _selectedValues)
					{
						DataGridViewRow dr = dataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(r => r.Cells[_keyField].Value.ToString() == val.ToString());
						if (dr != null)
						{
							dr.Cells["Selected"].Value = true;
						}
					}
				}
				finally
				{
					_isAddSelectedValue = true;
				}
			}
		}

		private void CorrectSelectedValues()
		{
			if (dataView != null && !string.IsNullOrEmpty(_keyField) && dataGridView.Columns["Selected"].Visible)
			{
				List<object> tempSelectedValues = new List<object>();

				foreach (var item in _selectedValues)
				{
					tempSelectedValues.Add(item);
				}

				_selectedValues.Clear();

				try
				{
					_isAddSelectedValue = false;
					foreach (DataGridViewRow dgr in dataGridView.Rows)
					{
						dgr.Cells["Selected"].Value = false;
					}
				}
				finally
				{
					_isAddSelectedValue = true;
				}

				foreach (object val in tempSelectedValues)
				{
					DataGridViewRow dr = dataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(r => r.Cells[_keyField].Value.ToString() == val.ToString());
					if (dr != null)
					{
						dr.Cells["Selected"].Value = true;
					}
				}

				tempSelectedValues.Clear();
			}
		}

		private void SetSelectedValuesAfterSorting()
		{
			if (dataView != null && !string.IsNullOrEmpty(_keyField) && dataGridView.Columns["Selected"].Visible)
			{
				foreach (object val in _selectedValues)
				{
					DataGridViewRow dr = dataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault(r => r.Cells[_keyField].Value.ToString() == val.ToString());
					if (dr != null)
					{
						dr.Cells["Selected"].Value = true;
					}
				}
			}

		}

		private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (!isMultiselect || string.IsNullOrEmpty(_keyField) || e.RowIndex == -1)
				return;

			DataGridView dgv = sender as DataGridView;
			DataGridViewColumn colSelected = null;
			DataGridViewColumn colKey = null;
			try
			{
				colSelected = dgv.Columns[e.ColumnIndex];
				if (!string.IsNullOrEmpty(_keyField))
				{
					colKey = dgv.Columns[_keyField];
				}
			}
			catch (Exception ex)
			{
				MLogger.Error($"Method: dataGridView_CellValueChanged. {ex.ToString()}");
				MessageBox.Show($"Method: dataGridView_CellValueChanged. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				colSelected = null;
				colKey = null;
			}

			if (_isAddSelectedValue)
			{
				if (colKey != null && colSelected != null && colSelected.Name == "Selected")
				{
					if (Convert.ToBoolean(dgv.Rows[e.RowIndex].Cells[colSelected.Name].Value))
					{
						AddSelectedValue(dgv.Rows[e.RowIndex].Cells[colKey.Name].Value);
					}
					else
					{
						RemoveSelectedValue(dgv.Rows[e.RowIndex].Cells[colKey.Name].Value);
					}
				}
			}

			dataGridView.RefreshEdit();
		}

		private void cbSelectAll_CheckStateChanged(object sender, EventArgs e)
		{
			CheckBox cb = sender as CheckBox;
			switch (cb.CheckState)
			{
				case CheckState.Checked:
					SelectAll();
					break;
				case CheckState.Indeterminate:
					break;
				case CheckState.Unchecked:
					ClearSelection();
					break;
			}
		}

		private void dataGridView_Sorted(object sender, EventArgs e)
		{
			SetSelectedValuesAfterSorting();
			PaintCells?.Invoke(dataGridView);
			dataGridView.RefreshEdit();
		}

		private void MapColumns()
		{
			if (dataGridView.Columns.Count == 0)
				return;

			DataGridViewColumn[] allCols = dataGridView.Columns.OfType<DataGridViewColumn>().ToArray();

			if (MappingColumns != null && MappingColumns.Count > 0)
			{
				foreach (var col in allCols)
				{
					if (MappingColumns.ContainsKey(col.Name))
					{
						col.HeaderText = MappingColumns[col.Name];
					}
				}
			}
		}

		private void dataGridView_DataSourceChanged(object sender, EventArgs e)
		{
			MapColumns();

			DataGridViewColumn[] cols = dataGridView.Columns.OfType<DataGridViewColumn>().Where(c => c.Name != "Selected").ToArray();
			foreach (DataGridViewColumn col in cols)
			{
				col.ReadOnly = true;
			}
			lbCount.Text = $"Количество строк: {dataGridView.RowCount}";



		}

		private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
				return;
			DataGridView dgv = sender as DataGridView;
			if (cellExtensionForm != null && cellExtensionForm.IsShowing)
				cellExtensionForm.Close();

			if (dgv.Columns[e.ColumnIndex].Name == "Details")
			{
				if (dgv.Columns.Contains(KeyField) &&
					(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null ? "" :
					dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) == "Double click")
					ShowDetailsEvent?.Invoke(this);
			}
			else
			{
				if (IsShowCellExtensionFormByDoubleClick)
				{
					ShowCellExtensionForm(dgv.Rows[e.RowIndex].Cells[e.ColumnIndex]); 
				}

				//if (dgv.Columns[e.ColumnIndex].Name != "Selected")
				//{
				//	cellExtensionForm = new CellExtensionForm();
				//	DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
				//	cellExtensionForm.lbInfo.Text = cell.OwningColumn.Name;
				//	cellExtensionForm.rtbInfo.Text = cell.Value == null ? "" : cell.Value.ToString();
				//	cellExtensionForm.Show();
				//}
			}
		}

		public void ShowCellExtensionForm(DataGridViewCell cell)
		{
			if (cell != null && cell.OwningColumn.Name != "Selected")
			{
				cellExtensionForm = new CellExtensionForm();
				cellExtensionForm.lbInfo.Text = cell.OwningColumn.HeaderText;
				cellExtensionForm.rtbInfo.Text = cell.Value == null ? "" : cell.Value.ToString();
				cellExtensionForm.Show();
			}
		}

		private void dataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				DataGridView dgv = sender as DataGridView;

				if (dgv != null)
				{
					if (IsColumnFilteringEnabled)
					{
						ColumnFindingForm cff = new ColumnFindingForm(dgv, dgv.Columns[e.ColumnIndex], dgv.Columns[e.ColumnIndex].ValueType);
						if (cff?.ShowDialog() == DialogResult.OK)
						{
							if (string.IsNullOrEmpty(Filter))
							{
								Filter = cff.FilterString;
							}
							else
							{
								Filter = $"{Filter} AND {cff.FilterString}";
							}
						} 
					}
				}
			}
		}

		private void btnClearFilters_Click(object sender, EventArgs e)
		{
			if (ResetFilterRadioButton != null)
			{
				ResetFilterRadioButton.Checked = false;
				ResetFilterRadioButton.Checked = true;
			}
			else
			{
				Filter = "";
			}
		}

		private void dataGridView_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			//dataGridView.EnableHeadersVisualStyles = true;
		}

		private void dataGridView_Paint(object sender, PaintEventArgs e)
		{
			PaintGrid?.Invoke(sender, e);
		}

		public void AddMenuItem(ToolStripMenuItem tsmi)
		{
			contextMenuStripGrid.Items.Add(tsmi);
		}

		private void miFilterByCurrentValue_Click(object sender, EventArgs e)
		{
			Filter = _filterByCurrentValue;
		}

		private void miResetFilter_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(Filter))
			{
				Filter = "";
			}
		}

		private void dataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			//if (e.ColumnIndex > -1 && e.RowIndex > -1 && e.Button == MouseButtons.Right)
			//{


			//	DataGridView dgv = sender as DataGridView;

			//	if (dgv.CurrentRow == null)
			//		return;

			//	DataGridViewCell cell = null;
			//	if (e.ColumnIndex > -1 && e.RowIndex > -1)
			//	{
			//		cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			//	}

			//	if (cell.ValueType == null || cell.Value == null)
			//	{
			//		_filterByCurrentValue = null;
			//		return;
			//	}

			//	_currentRightClickCell = cell;

			//	_currentColumnName = cell.OwningColumn.Name;

			//	switch (ExtTools.FindType(cell.ValueType))
			//	{
			//		case TypesFind._bool:
			//			_filterByCurrentValue = $"{cell.OwningColumn.Name} = {cell.Value.ToString()}";
			//			break;
			//		case TypesFind._int:
			//			_filterByCurrentValue = $"{cell.OwningColumn.Name} = {cell.Value.ToString()}";
			//			break;
			//		case TypesFind._string:
			//			_filterByCurrentValue = $"{cell.OwningColumn.Name} = '{cell.Value.ToString()}'";
			//			break;
			//		case TypesFind._DateTime:
			//			_filterByCurrentValue = $"{cell.OwningColumn.Name} = '{cell.Value.ToString()}'";
			//			break;
			//		case TypesFind._unnone:
			//			_filterByCurrentValue = "";
			//			break;
			//		default:
			//			break;
			//	}
			//}
		}

		private void ShowContextFilterMenuItems()
		{
			miFilterByCurrentValue.Visible = miResetFilter.Visible = true;
		}

		private void HideContextFilterMenuItems()
		{
			miFilterByCurrentValue.Visible = miResetFilter.Visible = false;
		}

		#region Flags
		public bool CbSelectAllVisible
		{
			get { return cbSelectAll.Visible; }
			set { cbSelectAll.Visible = value; }
		}

		public bool BtnClearFiltersVisible
		{
			get { return btnClearFilters.Visible; }
			set { btnClearFilters.Visible = value; }
		}

		public bool ExportToexcelVisible
		{
			get { return btnExportToexcel.Visible; }
			set { btnExportToexcel.Visible = value; }
		}

		public bool LbFilterVisible
		{
			get { return lbFilter.Visible; }
			set { lbFilter.Visible = value; }
		}

		public bool ContextFilterMenuItemsVisible
		{
			get
			{
				return _contextFilterMenuItemsVisible;
			}
			set
			{
				if (_contextFilterMenuItemsVisible)
				{
					ShowContextFilterMenuItems();
				}
				else
				{
					HideContextFilterMenuItems();
				}
				_contextFilterMenuItemsVisible = value;
			}
		}

		#endregion Flags

		public DataGridViewCell CurrentRightClickCell => _currentRightClickCell;

		private void RefreshingGridView_Load(object sender, EventArgs e)
		{
			if (_contextFilterMenuItemsVisible)
			{
				ShowContextFilterMenuItems();
			}
			else
			{
				HideContextFilterMenuItems();
			}
		}

		private void dataGridView_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			DataGridView dgv = sender as DataGridView;

			if (dgv.CurrentRow == null)
				return;
			if (e.RowIndex < 0)
			{
				return;
			}

			selectedRowIndex = e.RowIndex;
			if (!string.IsNullOrEmpty(_keyField))
			{
				try
				{
					SelectedRow = dgv.Rows[selectedRowIndex];
					_selectedValue = dgv.Rows[selectedRowIndex].Cells[_keyField].Value;
					//SelectedValueChanged?.Invoke(this, new EventArgs());
				}
				catch (Exception ex)
				{
					MLogger.Error($"Method: dataGridView_CellMouseDown. {ex.ToString()}");
					MessageBox.Show($"Method: dataGridView_CellMouseDown. {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{

		}

		private void ChangeExportToExcelProgressAsync(int val)
		{
			if (dataGridView.InvokeRequired)
			{
				pbExportToExcel.Invoke(new MethodInvoker(() => pbExportToExcel.Value = val));
			}
			else
			{
				pbExportToExcel.Value = val;
			}
		}

		private void ExportToExcelProgressVisible(bool val)
		{
			if (dataGridView.InvokeRequired)
			{
				pbExportToExcel.Invoke(new MethodInvoker(() => pbExportToExcel.Visible = val));
			}
			else
			{
				pbExportToExcel.Visible = val;
			}
		}

		private async Task ExportToExcelAsync()
		{
			await Task.Factory.StartNew(() =>
			{
				DataGridView dgv = new DataGridView();

				foreach (DataGridViewColumn col in this.dataGridView.Columns)
				{
					dgv.Columns.Add((DataGridViewColumn)col.Clone());
				}

				foreach (DataGridViewRow row in this.dataGridView.Rows)
				{
					DataGridViewRow cloneRow = (DataGridViewRow)row.Clone();
					foreach (DataGridViewCell cell in row.Cells)
					{
						cloneRow.Cells[cell.ColumnIndex].Value = cell.Value;
						cloneRow.Cells[cell.ColumnIndex].Style = cell.Style;
					}

					dgv.Rows.Add(cloneRow);
				}

				ChangeExportToExcelProgressAsync(0);
				ExportToExcelProgressVisible(true);

				// Creating a Excel object.
				Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
				Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
				Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

				try
				{
					worksheet = workbook.ActiveSheet;

					worksheet.Name = "ExportedFromDatGrid";

					int cellColumnIndex = 1;

					int step = 100000 / dgv.Columns.Count;

					int inc = 0;

					for (int j = 0; j < dgv.Columns.Count; j++)
					{
						if (dgv.Columns[j].Visible)
						{
							worksheet.Cells[1, cellColumnIndex] = dgv.Columns[j]?.HeaderText;
							worksheet.Cells[1, cellColumnIndex].EntireRow.Font.Bold = true;

							Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
							Microsoft.Office.Interop.Excel.Range cell = range.Cells[1, cellColumnIndex];
							Microsoft.Office.Interop.Excel.Borders border = cell.Borders;

							border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
							border.Weight = 2d;

							cellColumnIndex++;
							if (_stopExportToexcelFlag)
							{
								return;
							}
						}

						inc += step;
						ChangeExportToExcelProgressAsync(inc);
					}

					int cellRowIndex = 2;
					cellColumnIndex = 1;

					int mult = dgv.Rows.Count * dgv.Columns.Count;

					step = 900000 / mult;

					//Loop through each row and read value from each column.
					for (int i = 0; i < dgv.Rows.Count; i++)
					{
						for (int j = 0; j < dgv.Columns.Count; j++)
						{
							if (dgv.Rows[i].Cells[j].Visible)
							{
								if (!dgv.Rows[i].Cells[j].Style.BackColor.IsEmpty)
								{
									worksheet.Cells[cellRowIndex, cellColumnIndex].Interior.Color = System.Drawing.ColorTranslator.ToOle(dgv.Rows[i].Cells[j].Style.BackColor);
								}
								worksheet.Cells[cellRowIndex, cellColumnIndex] = dgv.Rows[i].Cells[j].Value?.ToString();

								Microsoft.Office.Interop.Excel.Range range = worksheet.UsedRange;
								Microsoft.Office.Interop.Excel.Range cell = range.Cells[cellRowIndex, cellColumnIndex];
								Microsoft.Office.Interop.Excel.Borders border = cell.Borders;

								border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
								border.Weight = 2d;

								cellColumnIndex++;
								if (_stopExportToexcelFlag)
								{
									return;
								}
							}

							inc += step;
							ChangeExportToExcelProgressAsync(inc);
						}
						cellColumnIndex = 1;
						cellRowIndex++;
					}

					worksheet.Columns.AutoFit();

					if (dataGridView.InvokeRequired)
					{
						dataGridView.Invoke(new MethodInvoker(() =>
						{
							SaveFileDialog saveDialog = new SaveFileDialog();
							saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
							saveDialog.FilterIndex = 2;
							saveDialog.FileName = $"{this.Name}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";

							if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
							{
								workbook.SaveAs(saveDialog.FileName);
								//MessageBox.Show("Export Successful");
							}
						}));
					}
					else
					{
						SaveFileDialog saveDialog = new SaveFileDialog();
						saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
						saveDialog.FilterIndex = 2;
						saveDialog.FileName = $"{this.Name}_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";

						if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
						{
							workbook.SaveAs(saveDialog.FileName);
							//MessageBox.Show("Export Successful");
						}
					}

				}
				catch (System.Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				finally
				{
					workbook.Saved = true;
					excel.Quit();
					workbook = null;
					excel = null;
					dgv = null;
					ExportToExcelProgressVisible(false);
					GC.Collect();
				}
			});
		}

		private async void btnExportToexcel_Click(object sender, EventArgs e)
		{
			//btnExportToexcel.Enabled = false;

			try
			{
				if (Convert.ToInt32(btnExportToexcel.Tag) == 1)
				{
					_stopExportToexcelFlag = true;
				}
				else
				{
					_stopExportToexcelFlag = false;
					btnExportToexcel.Text = "Отмена";
					btnExportToexcel.Tag = 1;

					await ExportToExcelAsync();
				}
			}
			finally
			{
				btnExportToexcel.Text = "В Excel";
				btnExportToexcel.Tag = 0;
				//btnExportToexcel.Enabled = true;
			}
		}

		private void dataGridView_MouseDown(object sender, MouseEventArgs e)
		{
			var hti = dataGridView.HitTest(e.X, e.Y);
			if (hti.RowIndex > -1 && hti.ColumnIndex > -1)
			{
				

				DataGridViewCell cell = dataGridView.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
				if (cell.OwningColumn.Name != "Selected")
				{
					switch (ExtTools.FindType(cell.ValueType))
					{
						case TypesFind._bool:
							_filterByCurrentValue = $"{cell.OwningColumn.Name} = {cell.Value.ToString()}";
							break;
						case TypesFind._int:
							_filterByCurrentValue = $"{cell.OwningColumn.Name} = {cell.Value.ToString()}";
							break;
						case TypesFind._string:
							_filterByCurrentValue = $"{cell.OwningColumn.Name} = '{cell.Value.ToString()}'";
							break;
						case TypesFind._DateTime:
							_filterByCurrentValue = $"{cell.OwningColumn.Name} = '{cell.Value.ToString()}'";
							break;
						case TypesFind._unnone:
							_filterByCurrentValue = "";
							break;
						default:
							break;
					}

					if (e.Button == MouseButtons.Right)
					{
						dataGridView.ClearSelection();
						dataGridView.Rows[hti.RowIndex].Selected = true;
						SelectedRow = dataGridView.Rows[hti.RowIndex];
						_selectedValue = dataGridView.Rows[hti.RowIndex].Cells[_keyField].Value;
						SelectedValueChanged?.Invoke(this, new EventArgs());
						int currentMouseOverRow = hti.RowIndex;
						if (IsContextMenuVisible)
						{
							contextMenuStripGrid.Show(dataGridView, new Point(e.X, e.Y)); 
						}
					}
				}
			}
		}

		public async Task ShowLoading(bool val)
		{
			await Task.Factory.StartNew(() =>
			{
				if (dataGridView.InvokeRequired)
				{
					dataGridView.Invoke(new MethodInvoker(() =>
					{
						dataGridView.Visible = !val;
					}));
				}
				else
				{
					dataGridView.Visible = !val;
				}
			});
		}

		private void dataGridView_SelectionChanged(object sender, EventArgs e)
		{
			if (dataGridView.CurrentRow != null)
			{
				SelectedRow = dataGridView.CurrentRow;
				_selectedValue = dataGridView.CurrentRow.Cells[KeyField].Value;
				SelectedValueChanged?.Invoke(this, new EventArgs());
			}
		}

		private void contextMenuStripGrid_Opening(object sender, CancelEventArgs e)
		{
			ContextMenuOpening?.Invoke(sender, e);
		}
	}
}
