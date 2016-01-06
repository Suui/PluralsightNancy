using log4net;
using log4net.Config;
using Nancy.Bootstrapper;


namespace Pluralsight.NancyIntro
{
	public class LoggingStartup : IApplicationStartup
	{
		private readonly ILog _logger;

		public LoggingStartup()
		{
			_logger = LogManager.GetLogger(typeof (LoggingStartup));
		}

		public void Initialize(IPipelines pipelines)
		{
			XmlConfigurator.Configure();

			pipelines.BeforeRequest.AddItemToStartOfPipeline(context =>
			{
				_logger.DebugFormat("Starting request for {0}", context.Request.Url);
				return null;
			});

			pipelines.AfterRequest += context => _logger.DebugFormat("Ending request for {0}", context.Request.Url);

			pipelines.OnError += (context, error) =>
			{
				_logger.ErrorFormat("Error on request({0}): {1}", context.Request.Url, error.Message);
				return context.Response;
			};
		}
	}
}