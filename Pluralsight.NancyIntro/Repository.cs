using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Pluralsight.NancyIntro.Course;


namespace Pluralsight.NancyIntro
{
	public class Repository
	{
		private static List<Course.Course> _courses = new List<Course.Course>();
		public ReadOnlyCollection<Course.Course> Courses
		{
			get
			{
				if (_courses == null) { _courses = new List<Course.Course>(); }
				return new ReadOnlyCollection<Course.Course>(_courses);
			}
		}

		public Course.Course AddCourse(string name, string author)
		{
			return AddCourse(name, author, new string[0]);
		}

		public Course.Course AddCourse(string name, string author, string[] modules)
		{
			var course = new Course.Course(_courses.NextId(), name, author);
			modules.ToList().ForEach(course.AddModule);
			_courses.Add(course);

			return course;
		}

		public void AddCourse(Course.Course course)
		{
			course.Id = _courses.NextId();
			course.Modules.ToList().ForEach(module => module.Id = course.Modules.NextId());
			_courses.Add(course);
		}

		public Course.Course GetCourse(int id)
		{
			return _courses.SingleOrDefault(course => course.Id == id);
		}
	}
}