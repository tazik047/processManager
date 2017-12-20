using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TopPower.DataGridViewSortableBinding;
using Timer = System.Windows.Forms.Timer;

namespace ProcessManager.Models
{
	public class ComputerView : UserControl
	{
		private Computer _computer;
		private readonly TabPage _page;
		private readonly SortableBindingList<ProcessModel> _currentProcessModels;
		private Button button1;
		private DataGridView dataGridView1;
		private BindingSource processModelBindingSource;
		private System.ComponentModel.IContainer components;
		private Timer timer1;
		private Button button2;
		private Button button3;
		private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn argumentsDataGridViewTextBoxColumn;
		private bool isUpdated = true;

		public ComputerView(Computer computer, TabPage page)
		{
			_computer = computer;
			_page = page;
			InitializeComponent();
			_currentProcessModels = new SortableBindingList<ProcessModel>();
			processModelBindingSource.DataSource = _currentProcessModels;
			timer1.Interval = _computer.LoadInterval * 1000;
			timer1.Start();
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.processModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.button3 = new System.Windows.Forms.Button();
			this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.argumentsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.processModelBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(4, 4);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(123, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Запустить процесс";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.Location = new System.Drawing.Point(319, 3);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(138, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "Изменить настройки";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.AllowUserToAddRows = false;
			this.dataGridView1.AllowUserToDeleteRows = false;
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.AutoGenerateColumns = false;
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.argumentsDataGridViewTextBoxColumn});
			this.dataGridView1.DataSource = this.processModelBindingSource;
			this.dataGridView1.Location = new System.Drawing.Point(4, 34);
			this.dataGridView1.MultiSelect = false;
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.ReadOnly = true;
			this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridView1.Size = new System.Drawing.Size(453, 344);
			this.dataGridView1.TabIndex = 1;
			// 
			// processModelBindingSource
			// 
			this.processModelBindingSource.DataSource = typeof(ProcessManager.Models.ProcessModel);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(133, 3);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(180, 23);
			this.button3.TabIndex = 2;
			this.button3.Text = "Завершить выбранный процесс";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// idDataGridViewTextBoxColumn
			// 
			this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
			this.idDataGridViewTextBoxColumn.HeaderText = "Id";
			this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
			this.idDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// nameDataGridViewTextBoxColumn
			// 
			this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
			this.nameDataGridViewTextBoxColumn.HeaderText = "Название";
			this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
			this.nameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// pathDataGridViewTextBoxColumn
			// 
			this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
			this.pathDataGridViewTextBoxColumn.HeaderText = "Путь";
			this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
			this.pathDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// argumentsDataGridViewTextBoxColumn
			// 
			this.argumentsDataGridViewTextBoxColumn.DataPropertyName = "Arguments";
			this.argumentsDataGridViewTextBoxColumn.HeaderText = "Аргументы";
			this.argumentsDataGridViewTextBoxColumn.Name = "argumentsDataGridViewTextBoxColumn";
			this.argumentsDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// ComputerView
			// 
			this.Controls.Add(this.button3);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "ComputerView";
			this.Size = new System.Drawing.Size(460, 381);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.processModelBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		private void button2_Click(object sender, EventArgs e)
		{
			var form = new ComputerSettings(_computer);

			if (form.ShowDialog() == DialogResult.OK)
			{
				_computer = form.GetComputer();
				_page.Text = _computer.Name;
				timer1.Stop();
				timer1.Interval = _computer.LoadInterval;
				timer1.Start();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var result = Microsoft.VisualBasic.Interaction.InputBox("Введите путь к файлу которые необходимо запустить");
			if (!string.IsNullOrWhiteSpace(result))
			{
				try
				{
					_computer.RunProcess(result);
					MessageBox.Show("Процесс запущено", "Процесс успешно запущено");
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message, "Произошла ошибка");
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (!isUpdated)
			{
				return;
			}

			isUpdated = false;
			ThreadPool.QueueUserWorkItem(UpdateProcessGrid);
		}

		private void UpdateProcessGrid(object arg)
		{
			try
			{
				var currentProcesses = _computer.GetAllProcesses();
				Invoke((Action) (() =>
				{
					var selectedProcess = (ProcessModel) processModelBindingSource.Current;
					var scrollPosition = dataGridView1.FirstDisplayedScrollingRowIndex;
					var currentSort = new
					{
						processModelBindingSource.SortDirection,
						processModelBindingSource.SortProperty
					};
					_currentProcessModels.Clear();
					foreach (var processModel in currentProcesses)
					{
						if (!string.IsNullOrWhiteSpace(processModel.Path) && _computer.RestrictedProcesses.Contains(processModel.Path.ToLower()))
						{
							_computer.KillProcess(processModel.Id);
						}

						_currentProcessModels.Add(processModel);
					}

					if (currentSort.SortProperty != null)
					{
						processModelBindingSource.ApplySort(currentSort.SortProperty, currentSort.SortDirection);
					}

					processModelBindingSource.Position = _currentProcessModels.IndexOf(selectedProcess);
					if (scrollPosition > 0 && scrollPosition < _currentProcessModels.Count)
					{
						dataGridView1.FirstDisplayedScrollingRowIndex = scrollPosition;
					}

					isUpdated = true;
				}));
			}
			catch(Exception exception)
			{
				MessageBox.Show(exception.Message, "Произошла ошибка - " + _computer.Name);
				isUpdated = true;
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			var selectedProcess = (ProcessModel)processModelBindingSource.Current;
			if (selectedProcess != null)
			{
				try
				{
					_computer.KillProcess(selectedProcess.Id);
					MessageBox.Show("Процесс успешно завершен", "Процесс запущено");
				}
				catch (Exception exception)
				{
					MessageBox.Show(exception.Message, "Произошла ошибка");
				}
				
			}
		}

		protected override void Dispose(bool disposing)
		{
			timer1.Stop();
			base.Dispose(disposing);
		}
	}
}
