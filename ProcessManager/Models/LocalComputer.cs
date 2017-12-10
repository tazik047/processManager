using System.Diagnostics;
using System.Linq;

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
}