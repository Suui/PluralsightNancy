using System.Collections.Generic;
using log4net;
using log4net.Config;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;


namespace Pluralsight.NancyIntro
{
	public class DefaultPluralsightBootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);
			XmlConfigurator.Configure();
		}

		protected override void RegisterInstances(TinyIoCContainer container, IEnumerable<InstanceRegistration> instanceRegistrations)
		{
			base.RegisterInstances(container, instanceRegistrations);
			container.Register(typeof (ILog), (_container, _overload) => LogManager.GetLogger(typeof (DefaultNancyBootstrapper)));
		}

		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{
			base.RequestStartup(container, pipelines, context);
			var logger = container.Resolve<ILog>();

			pipelines.BeforeRequest += _context =>
			{
				logger.DebugFormat("Starting request for {0}", _context.Request.Url);
				return null;
			};

			pipelines.AfterRequest += _context => logger.DebugFormat("Ending request for {0}", _context.Request.Url);

			pipelines.OnError += (_context, _error) =>
			{
				logger.ErrorFormat("Error on request({0}): {1}", _context.Request.Url, _error.Message);
				return _context.Response;
			};
		}
	}
}