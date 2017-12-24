using System;
using System.Diagnostics;
using System.Linq;

namespace ProcessManager.Models
{
	public class LocalComputer : Computer
	{
		public LocalComputer() : base("Мой компьютер", null, null, 5) { }

		public override ProcessModel[] GetAllProcesses()
		{
			return Process
				.GetProcesses()
				.Select(p=> new ProcessModel
					{
						Id = p.Id,
						Name = p.ProcessName,
						Path = GetProcessFileName(p),
						Arguments = p.StartInfo.Arguments
					})
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
	}
}