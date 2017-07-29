using MagicUpdaterMonitor.Base;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Controls
{
	public partial class ConfirmationWithTableForm : BaseForm
	{
		private string infoText = "Компьютеры, которым отправится операция";
		public string OperationTypeName { get; set; }
		public ConfirmationWithTableForm()
		{
			InitializeComponent();
			rgvInfo.LbFilterVisible = false;
		}

		private const int CP_NOCLOSE_BUTTON = 0x200;
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams myCp = base.CreateParams;
				myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
				return myCp;
			}
		}

		public new DialogResult ShowDialog()
		{
			lbOperType.Text = OperationTypeName.ToString();
			lbInfo.Text = $"{infoText}:";
			return base.ShowDialog();
		}
	}
}
