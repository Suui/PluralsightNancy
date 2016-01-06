using System;
using Nancy;


namespace Pluralsight.NancyIntro
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Func<Request, bool> isNotAnApiClient = request => !request.Headers.UserAgent.ToLower().StartsWith("curl");

			Get["/"] = _ => Response.AsRedirect("/api/courses");

			Get["/", context => isNotAnApiClient.Invoke(context.Request)] = _ => Response.AsRedirect("/courses");
		}
	}
}