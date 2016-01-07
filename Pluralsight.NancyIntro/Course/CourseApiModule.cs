using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;


namespace Pluralsight.NancyIntro.Course
{
	public class CourseApiModule : NancyModule
	{
		public CourseApiModule(Repository repository) : base("/api/courses")
		{
			Before += context =>
			{
				context.Items.Add("start_time", DateTime.UtcNow);
				return context.Request.Headers.UserAgent.ToLower().StartsWith("curl") ? null : new NotFoundResponse();
			};

			After += context =>
			{
				var processTime = (DateTime.UtcNow - (DateTime) context.Items["start_time"]).TotalMilliseconds;
				System.Diagnostics.Debug.WriteLine("Processing Time: " + processTime);

				context.Response.WithHeader("x-processing-time", processTime.ToString(CultureInfo.CurrentCulture));
			};

			Get["/"] = _ => new JsonResponse(repository, new DefaultJsonSerializer());

			Get["/{id}"] = course => Response.AsJson((Course) repository.GetCourse(course.id));

			Post["/"] = _ =>
			{
				var course = this.Bind<Course>();
				repository.AddCourse(course);

				return CourseResponse(course);
			};

//			Post["/"] = _ =>
//			{
//				var name = this.Request.Form.Name;
//				var author = this.Request.Form.Author;
//				var course = Repository.AddCourse(name, author);
//
//				return CourseResponse(course);
//			};
		}

		private dynamic CourseResponse(dynamic course)
		{
			string url = string.Format("{0}/{1}", Context.Request.Url, course.Id);

			return new Response { StatusCode = HttpStatusCode.Accepted }
				.WithHeader("Location", url);
		}
	}
}