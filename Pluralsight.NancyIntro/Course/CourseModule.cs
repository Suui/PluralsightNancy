using Nancy;


namespace Pluralsight.NancyIntro.Course
{
	public class CourseModule : NancyModule
	{
		public CourseModule(Repository repository) : base("/courses")
		{
			Get["/"] = p => View["courses.html", repository.Courses];

			Get["/{id}"] = p => View[repository.GetCourse(p.Id)];
		}
	}
}