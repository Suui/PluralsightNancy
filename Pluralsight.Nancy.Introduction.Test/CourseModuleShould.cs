using System;
using System.IO;
using Nancy.Testing;
using Nancy.Testing.Fakes;
using NUnit.Framework;
using Pluralsight.NancyIntro.Course;


namespace Pluralsight.Nancy.Introduction.Test
{
	[TestFixture]
	public class CourseModuleShould
	{
		private Browser _browser;
		private readonly string _solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));

		private void GivenABrowserForTheCourseModule()
		{
			FakeRootPathProvider.RootPath = _solutionPath + @"Pluralsight.NancyIntro\Course\Views";

			var bootstrapper = new ConfigurableBootstrapper(with =>
			{
				with.RootPathProvider(new FakeRootPathProvider());
				with.Module<CourseModule>();
			});
			_browser = new Browser(bootstrapper);
		}

		[Test]
		public void get_the_courses_view()
		{
			GivenABrowserForTheCourseModule();

			var response = _browser.Get("/courses");

			response.Body["body"].ShouldExist();
		}
	}
}