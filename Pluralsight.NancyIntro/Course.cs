using System.Collections.Generic;


namespace Pluralsight.NancyIntro
{
	public class Course
	{
		public int Id { get; set; } 
		public string Name { get; set; }
		public string Author { get; set; }

		public Course(int id, string name, string author)
		{
			Id = id;
			Name = name;
			Author = author;
		}

		public static List<Course> List = new List<Course>
		{
			new Course(0, "Getting Started with Nancy", "Richard Cirerol"),
			new Course(1, "HTTP Fundamentals", "Scott Allen")
		};

		public static int AddCourse(string name, string author)
		{
			var id = List.Count;
			List.Add(new Course(id, name, author));

			return id;
		}
	}
}