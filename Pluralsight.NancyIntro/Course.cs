using System.Collections.Generic;
using System.Linq;


namespace Pluralsight.NancyIntro
{
	public class Repository
	{
		public static List<Course> Courses = new List<Course>();

		public static Course AddCourse(string name, string author)
		{
			return AddCourse(name, author, new string[0]);
		}

		private static Course AddCourse(string name, string author, string[] modules)
		{
			var course = new Course(Courses.NextId(), name, author);
			modules.ToList().ForEach(course.AddModule);
			Courses.Add(course);

			return course;
		}

		public static void AddCourse(Course course)
		{
			course.Id = Courses.NextId();
			course.Modules.ToList().ForEach(module => module.Id = course.Modules.NextId());
			Courses.Add(course);
		}

		public static Course GetCourse(int id)
		{
			return Courses.SingleOrDefault(course => course.Id == id);
		}
	}

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
