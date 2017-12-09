namespace ProcessManager.Models
{
	public class ProcessModel
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string Path { get; set; }

		public string Arguments { get; set; }

		public override bool Equals(object obj)
		{
			if (obj is ProcessModel)
			{
				return Equals((ProcessModel)obj);
			}

			return false;
		}

		protected bool Equals(ProcessModel other)
		{
			return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Path, other.Path) && string.Equals(Arguments, other.Arguments);
		}

		public override int GetHashCode()
		{
			return Id;
		}
	}
}
