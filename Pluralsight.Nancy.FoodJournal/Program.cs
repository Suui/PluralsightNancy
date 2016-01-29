using System;
using System.Threading;
using Microsoft.Owin.Hosting;
using Pluralsight.Nancy.FoodJournal.Core;


namespace Pluralsight.Nancy.FoodJournal
{
	class Program
	{
		private static readonly ManualResetEvent _quitEvent = new ManualResetEvent(false);

		public static void Main(string[] args)
		{
			var port = 59338;
			if (args.Length > 0)
			{
				int.TryParse(args[0], out port);
			}

			Console.CancelKeyPress += (IChannelSender, eArgs) =>
			{
				_quitEvent.Set();
				eArgs.Cancel = true;
			};

			using (WebApp.Start<Startup>(string.Format("http://*:{0}", port)))
			{
				Console.WriteLine("Started");
				_quitEvent.WaitOne();
			}
		}
	}
}