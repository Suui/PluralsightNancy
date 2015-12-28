using System;
using Nancy;


namespace Pluralsight.NancyIntro
{
	public class HomeModule : NancyModule
	{
		public HomeModule()
		{
			Func<Request, bool> isNotAnApiClient = request => !request.Headers.UserAgent.ToLower().StartsWith("curl");

			Get["/", context => isNotAnApiClient.Invoke(context.Request)] = _ => View["index"];
			Get["/"] = _ => "Welcome to the Pluralsight API";
		}
	}
}