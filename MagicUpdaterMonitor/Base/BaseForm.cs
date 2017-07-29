using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace MagicUpdaterMonitor.Base
{
	public partial class BaseForm : Form
	{
		public BaseForm()
		{
			InitializeComponent();
		}

		private void BaseForm_Load(object sender, EventArgs e)
		{
			this.Icon = new Icon(Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location), 40, 40);
		}
	}
}
