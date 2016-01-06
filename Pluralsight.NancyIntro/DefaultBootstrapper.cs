using Nancy;
using Nancy.Conventions;


namespace Pluralsight.NancyIntro
{
	public class DefaultBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ConfigureConventions(NancyConventions nancyConventions)
		{
			base.ConfigureConventions(nancyConventions);

			nancyConventions.ViewLocationConventions.Add(
				(viewName, model, context) => string.Concat(context.ModuleName, "/views/", viewName));
		}
	}
}