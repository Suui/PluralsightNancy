using Nancy;


namespace Pluralsight.Nancy.FoodJournal.Core
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override IRootPathProvider RootPathProvider => new ServiceRootPathProvider();
	}
}