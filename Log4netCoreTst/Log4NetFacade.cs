using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;

namespace Log4netCoreTst
{
	internal class Log4NetFacade : LogComparisonCommon.ILogger
	{
		private log4net.ILog Logger { get; set; }

		static Log4NetFacade()
		{
			XmlDocument log4netConfig = new XmlDocument();
			log4netConfig.Load(File.OpenRead("log4net.config"));
			var repo = log4net.LogManager.CreateRepository(Assembly.GetEntryAssembly(),
								 typeof(log4net.Repository.Hierarchy.Hierarchy));
			log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
		}

		internal Log4NetFacade(Type type)
		{
			Logger = log4net.LogManager.GetLogger(type);
		}

		public void LogMessage(string msg)
		{
			Logger.Info(msg);
		}
	}
}
