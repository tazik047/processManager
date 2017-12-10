using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;

namespace ProcessManager.Models
{
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
			var scope = GetManagementScope();
			var options = new ObjectGetOptions();
			using (ManagementClass processClass = new ManagementClass(scope, new ManagementPath("Win32_Process"), options))
			{
				var parameters = processClass.GetMethodParameters("Create");

				var taskName = Guid.NewGuid().ToString();
				var prepareCommand = $"schtasks /Create /sc daily /tn {taskName} /tr \"{fullName}\" /it";
				var executeCommand = $"schtasks /run /tn {taskName} /i";
				var deleteCommand = $"schtasks /delete /tn {taskName} /f";


				parameters["CommandLine"] = prepareCommand;
				processClass.InvokeMethod("Create", parameters, null);

				parameters["CommandLine"] = executeCommand;
				processClass.InvokeMethod("Create", parameters, null);

				parameters["CommandLine"] = deleteCommand;
				processClass.InvokeMethod("Create", parameters, null);
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
			if (!string.IsNullOrWhiteSpace(UserName) || !string.IsNullOrWhiteSpace(Password))
			{
				connection.Username = UserName;
				connection.Password = Password;
			}

			ManagementScope scope = new ManagementScope("\\\\" + Name + "\\root\\CIMV2", connection);
			scope.Connect();

			return scope;
		}
	}
}
