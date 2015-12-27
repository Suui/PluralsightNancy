using System;
using System.Linq;
using Nancy;
using Nancy.Responses;


namespace Pluralsight.NancyIntro
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Func<Request, bool> isNotAnApiClient = request => !request.Headers.UserAgent.ToLower().StartsWith("curl");

			Get["/", context => isNotAnApiClient.Invoke(context.Request)] = _ => View["index"];
			Get["/"] = _ => "Welcome to the Pluralsight API";


			/**
			Course routes:
			- GET list of courses
			- GET single course
			- POST single course
			- Return responses as JSON
			*/

			Get["/courses"] = _ => new JsonResponse(Course.List, new DefaultJsonSerializer());

			Get["/courses/{id}"] = parameter => Response.AsJson(Course.List.SingleOrDefault(x => x.Id == parameter.id));

			Post["/courses"] = _ =>
			{
				var name = Request.Form.Name;
				var author = Request.Form.Author;
				var id = Course.AddCourse(name, author);

				string url = string.Format("{0}/{1}", Context.Request.Url, id);

				return new Response { StatusCode = HttpStatusCode.Accepted }
					.WithHeader("Location", url);
			};
		} 
	}
}