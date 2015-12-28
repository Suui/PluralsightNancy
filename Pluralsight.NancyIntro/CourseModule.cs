using System;
using System.Globalization;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Responses;


namespace Pluralsight.NancyIntro
{
	public class CourseModule : NancyModule {

		public CourseModule() : base("/courses")
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

			Get["/"] = _ => new JsonResponse(Repository.Courses, new DefaultJsonSerializer());

			Get["/{id}"] = parameter => Response.AsJson((Course)Repository.GetCourse(parameter.id));

			Post["/", context => context.Request.Headers.ContentType != "application/x-www-urlencoded"] = _ =>
			{
				var course = this.Bind<Course>();
				Repository.AddCourse(course);

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