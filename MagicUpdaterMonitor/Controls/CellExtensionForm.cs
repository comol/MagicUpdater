using MagicUpdaterMonitor.Base;
using System;

namespace MagicUpdaterMonitor.Controls
{
	public partial class CellExtensionForm : BaseForm
	{
		public bool IsShowing { get; set; } = false;
		public CellExtensionForm()
		{
			InitializeComponent();
		}

		public new void Show()
		{
			IsShowing = true;
			base.Show();
		}

		public new void Hide()
		{
			IsShowing = false;
			base.Hide();
		}

		public new void Close()
		{
			IsShowing = false;
			base.Close();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
