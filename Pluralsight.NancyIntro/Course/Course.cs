using System.Collections.Generic;
using System.Linq;


namespace Pluralsight.NancyIntro.Course
{
	public class Course : IEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public List<Module> Modules { get; set; }

		public Course()
		{
			Modules = new List<Module>();
		}

		public Course(int id, string name, string author)
		{
			Id = id;
			Name = name;
			Author = author;
			Modules = new List<Module>();
		}

		public void AddModule(string topic)
		{
			Modules.Add(new Module(Modules.NextId(), topic));
		}
	}

	public class Module : IEntity
	{
		public int Id { get; set; }
		public string Topic { get; set; }

		public Module() {}

		public Module(int id, string topic)
		{
			Id = id;
			Topic = topic;
		}
	}

	public interface IEntity
	{
		
	}

	static class EntityListExtensions
	{
		public static int NextId(this IEnumerable<IEntity> entities)
		{
			return entities.Count();
		}
	}
}
