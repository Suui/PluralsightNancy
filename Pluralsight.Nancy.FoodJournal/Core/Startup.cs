using Owin;


namespace Pluralsight.Nancy.FoodJournal.Core
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.UseWelcomePage();
		}
	}
}