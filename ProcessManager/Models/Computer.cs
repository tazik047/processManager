using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;

namespace ProcessManager.Models
{
	public class LocalComputer : Computer
	{
		public LocalComputer() : base("Мой компьютер") { }

		public override ProcessModel[] GetAllProcesses()
		{
			return Process
				.GetProcesses()
				.Select(ConvertToProcessModel)
				.ToArray();
		}

		public override void KillProcess(int processId)
		{
			Process.GetProcessById(processId).Kill();
		}

		public override void RunProcess(string fullName)
		{
			Process.Start(fullName);
		}
	}

	public class Computer
	{
		public Computer(string name) : this(name, null, null, 5) { }

		public Computer(string name, string userName, string password, int loadInterval)
		{
			Name = name;
			UserName = userName;
			Password = password;
			LoadInterval = loadInterval;
			RestrictedProcesses = new string[0];
		}

		public string Name { get; set; }

		public string UserName { get; set; }

		public string Password { get; set; }

		public string[] RestrictedProcesses { get; set; }

		public int LoadInterval { get; set; }

		public virtual ProcessModel[] GetAllProcesses()
		{
			var result = new List<ProcessModel>();

			var scope = GetManagementScope();
				var query = new ObjectQuery("Select ProcessId, ExecutablePath, Name, CommandLine From Win32_Process");

			using (var searcher = new ManagementObjectSearcher(scope, query))
			{
				foreach (ManagementObject details in searcher.Get())
				{
					result.Add(new ProcessModel
					{
						Id = Convert.ToInt32(details["ProcessId"].ToString()),
						Name = details["Name"].ToString(),
						Arguments = details["CommandLine"]?.ToString(),
						Path = details["ExecutablePath"]?.ToString()
					});
				}
			}

			return result.ToArray();
		}

		public virtual void RunProcess(string fullName)
		{
			object[] theProcessToRun = { fullName };
			var scope = GetManagementScope();
			using (ManagementClass theClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), new ObjectGetOptions()))
			{
				theClass.InvokeMethod("Create", theProcessToRun);
			}
			
		}

		public virtual void KillProcess(int processId)
		{
			var scope = GetManagementScope();
			var query = new ObjectQuery("Select * From Win32_Process where ProcessId = " + processId);

			using (var searcher = new ManagementObjectSearcher(scope, query))
			{
				foreach (ManagementObject details in searcher.Get())
				{
					details.InvokeMethod("Terminate", null);
				}
			}
			

			/*
			TASKKILL [/S <система> [/U <пользователь> [/P [<пароль>]]]] 
			{ [/FI <фильтр>] [/PID <процесс> | /IM <образ>] } [/F] [/T] 

			Описание: 
			Эта команда позволяет завершить один или несколько процессов. 
			Процесс может быть завершен по имени образа или по идентификатору процесса. 

			Список параметров: 
			/S <система> Подключаемый удаленный компьютер. 

			/U [<домен>\]<пользователь> Пользовательский контекст, в котором 
			должна выполняться эта команда. 

			/P <пароль> Пароль для этого пользовательского контекста. 

			Запрашивает пароль, если он не задан. 

			/F Принудительное завершение процесса 


			/FI <фильтр> Отображение задач, отвечающих 
			указанному в фильтре критерию. 

			/PID <процесс> Идентификатор процесса, который требуется 
			завершить. 

			/IM <образ> Имя образа процесса, который требуется 
			завершить. Для указания всех процессов 
			можно использовать символ шаблона '*'. 

			/T Завершение указанного процесса 
			и всех его дочерних процессов. */
		}

		protected ProcessModel ConvertToProcessModel(Process process)
		{
			return new ProcessModel
			{
				Id = process.Id,
				Name = process.ProcessName,
				Path = GetProcessFileName(process),
				Arguments = process.StartInfo.Arguments
			};
		}

		private string GetProcessFileName(Process process)
		{
			try
			{
				return process.MainModule.FileName;
			}
			catch (Exception)
			{
				return string.Empty;
			}
		}

		private ManagementScope GetManagementScope()
		{
			ConnectionOptions connection = new ConnectionOptions();
			connection.Username = UserName;
			connection.Password = Password;
			ManagementScope scope = new ManagementScope("\\\\" + Name + "\\root\\CIMV2", connection);
			scope.Connect();

			return scope;
		}
	}
}
