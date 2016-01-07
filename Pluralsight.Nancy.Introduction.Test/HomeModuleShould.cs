using Nancy.Testing;
using NUnit.Framework;
using Pluralsight.NancyIntro;


namespace Pluralsight.Nancy.Introduction.Test
{
	[TestFixture]
	public class HomeModuleShould
	{
		private Browser _browser;

		[Test]
		public void redirect_to_courses_when_accessing_root()
		{
			GivenABrowserForTheHomeModule();

			var response = _browser.Get("/");

			response.ShouldHaveRedirectedTo("/courses");
		}

		[Test]
		public void redirect_to_api_courses_when_accesing_root_from_curl()
		{
			GivenABrowserForTheHomeModule();

			var response = _browser.Get("/", with => with.Header("User-Agent", "curl"));

			response.ShouldHaveRedirectedTo("/api/courses");
		}

		private void GivenABrowserForTheHomeModule()
		{
			var bootstrapper = new ConfigurableBootstrapper(with => with.Module<HomeModule>());
			_browser = new Browser(bootstrapper);
		}
	}
}