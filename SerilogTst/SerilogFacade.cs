using Microsoft.Extensions.Configuration;
using Serilog;
using System;

namespace SerilogTst
{
	internal class SerilogFacade : LogComparisonCommon.ILogger
	{
		private readonly Serilog.Core.Logger Logger;

		public SerilogFacade(Type type)
		{
			Logger = new LoggerConfiguration()
					//.WriteTo.Console()
					.WriteTo.File("c:\\temp\\serilog-comparison",
				outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
					.Enrich.WithThreadId()
					.CreateLogger();

		}

		public void LogMessage(string msg)
		{
			Logger.Information(msg);
		}
	}
}