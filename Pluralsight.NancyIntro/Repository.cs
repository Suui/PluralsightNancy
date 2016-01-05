using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;


namespace Pluralsight.NancyIntro
{
	public class Repository
	{
		static List<Course> _courses = new List<Course>();
		public ReadOnlyCollection<Course> Courses
		{
			get
			{
				if (_courses == null) { _courses = new List<Course>(); }
				return new ReadOnlyCollection<Course>(_courses);
			}
		}

		public Course AddCourse(string name, string author)
		{
			return AddCourse(name, author, new string[0]);
		}

		public Course AddCourse(string name, string author, string[] modules)
		{
			var course = new Course(_courses.NextId(), name, author);
			modules.ToList().ForEach(course.AddModule);
			_courses.Add(course);

			return course;
		}

		public void AddCourse(Course course)
		{
			course.Id = _courses.NextId();
			course.Modules.ToList().ForEach(module => module.Id = course.Modules.NextId());
			_courses.Add(course);
		}

		public Course GetCourse(int id)
		{
			return _courses.SingleOrDefault(course => course.Id == id);
		}
	}
}