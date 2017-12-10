using System;
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

		private void deleteSelectedComputer_Click(object sender, EventArgs e)
		{
			if (tabControl1.TabPages.Count > 0)
			{
				var tab = tabControl1.SelectedTab;
				tabControl1.TabPages.Remove(tab);
				tab.Dispose();
			}
		}
	}
}
