using Nancy;


namespace Pluralsight.NancyIntro.Course
{
	public class CourseModule : NancyModule
	{
		public CourseModule() : base("/courses")
		{
			Get["/"] = p => View["courses.html"];
		}
	}
}