using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProcessManager.Models;

namespace ProcessManager
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			AddComputerTabPage(new LocalComputer());
			
		}

		private void addComputerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var form = new ComputerSettings();
			if (form.ShowDialog() == DialogResult.OK)
			{
				var computer = form.GetComputer();
				AddComputerTabPage(computer);
			}
		}

		private void AddComputerTabPage(Computer computer)
		{
			var page = new TabPage(computer.Name);

			var view = new ComputerView(computer, page);
			view.Dock = DockStyle.Fill;

			page.Controls.Add(view);
			tabControl1.TabPages.Add(page);
		}
	}
}
