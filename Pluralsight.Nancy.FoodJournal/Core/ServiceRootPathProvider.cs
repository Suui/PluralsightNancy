using System;
using System.IO;
using System.Reflection;
using Nancy;


namespace Pluralsight.Nancy.FoodJournal.Core
{
	public class ServiceRootPathProvider : IRootPathProvider
	{
		public string GetRootPath()
		{
			var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
			var assemblyPath = Path.GetDirectoryName(assembly.Location) ?? Environment.CurrentDirectory;
			var rootPath = Path.GetFullPath(Path.Combine(assemblyPath, "..", ".."));

			return rootPath;
		}
	}
}