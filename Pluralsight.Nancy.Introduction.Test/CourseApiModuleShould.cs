using System;
using System.IO;
using FluentAssertions;
using Nancy.Testing;
using NUnit.Framework;
using Pluralsight.NancyIntro.Course;


namespace Pluralsight.Nancy.Introduction.Test
{
	[TestFixture]
	public class CourseApiModuleShould
	{
		private Browser _browser;

		private void GivenABrowserForTheCourseApiModule()
		{
			var bootstrapper = new ConfigurableBootstrapper(with => { with.Module<CourseApiModule>(); });
			_browser = new Browser(bootstrapper);
		}

		[Test]
		public void T01_post_a_new_course_from_a_form()
		{
			GivenABrowserForTheCourseApiModule();

			var response = _browser.Post("/api/courses", with =>
			{
				with.Header("User-Agent", "curl");
				with.FormValue("name", "Testing with Nancy");
				with.FormValue("author", "Richard Cirerol");
				with.Header("Content-Type", "application/x-www-form-urlencoded");
			});

			response.Headers["Location"].Should().Contain("/api/courses/0");
		}

		[Test]
		public void T02_post_a_new_course_from_a_json()
		{
			GivenABrowserForTheCourseApiModule();
			var solutionPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
			var jsonCoursePath = solutionPath + @"Pluralsight.NancyIntro\assets\NancyIntroductionCourse.json";
			var jsonCourse = File.ReadAllText(jsonCoursePath);

			var response = _browser.Post("api/courses", with =>
			{
				with.Header("User-Agent", "curl");
				with.Header("Content-Type", "application/json");
				with.Body(jsonCourse);
			});

			response.Headers["Location"].Should().Contain("/api/courses/1");
		}

		[Test]
		public void T03_post_a_new_course_from_a_model()
		{
			GivenABrowserForTheCourseApiModule();
			var course = new Course(1, "A course", "Myself");

			var response = _browser.Post("/api/courses", with =>
			{
				with.Header("User-Agent", "curl");
				with.JsonBody(course);
			});

			response.Headers["Location"].Should().Contain("/api/courses/2");
		}

		[Test]
		public void T04_get_deserialized_course()
		{
			GivenABrowserForTheCourseApiModule();
			const string courseFromJsonUrl = "/api/courses/1";

			var response = _browser.Get(courseFromJsonUrl, with =>
			{
				with.Header("User-Agent", "curl");
			});
			var course = response.Body.DeserializeJson<Course>();

			course.Id.Should().Be(1);
			course.Name.Should().Be("Getting Started with Nancy");
			course.Author.Should().Be("Richard Cirerol");
			course.Modules.Count.Should().Be(3);
		}
	}
}