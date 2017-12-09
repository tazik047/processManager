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
	public partial class ComputerSettings : Form
	{
		private readonly Computer _initialComputer;

		public ComputerSettings()
		{
			InitializeComponent();
		}

		public ComputerSettings(Computer computer) : this()
		{
			_initialComputer = computer;
			nameTextBox.Text = computer.Name;
			userNameTextBox.Text = computer.UserName;
			passwordTextBox.Text = computer.Password;
			loadIntervalControl.DecimalPlaces = computer.LoadInterval;

			foreach (var restrictedProcess in _initialComputer.RestrictedProcesses)
			{
				dataGridView1.Rows.Add(restrictedProcess);
			}
		}

		public Computer GetComputer()
		{
			if (_initialComputer != null)
			{
				_initialComputer.Name = nameTextBox.Text;
				_initialComputer.Password = passwordTextBox.Text;
				_initialComputer.UserName = userNameTextBox.Text;
				_initialComputer.LoadInterval = loadIntervalControl.DecimalPlaces;
				_initialComputer.RestrictedProcesses = GetRestrictedProcesses();

				return _initialComputer;
			}

			var result = new Computer(nameTextBox.Text, userNameTextBox.Text, passwordTextBox.Text, loadIntervalControl.DecimalPlaces);
			result.RestrictedProcesses = GetRestrictedProcesses();

			return result;
		}

		private string[] GetRestrictedProcesses()
		{
			var result = new string[dataGridView1.RowCount - 1];
			for (int i = 0; i < dataGridView1.RowCount - 1; i++)
			{
				result[i] = dataGridView1.Rows[i].Cells[0].Value.ToString().Trim().ToLower();
			}

			return result;
		}
	}
}
