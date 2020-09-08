using System;

namespace NLogCoreTst
{
	internal class NLogFacade : LogComparisonCommon.ILogger
	{
		private readonly NLog.Logger Logger;

		public NLogFacade(Type type)
		{
			Logger = NLog.LogManager.GetLogger(type.Name);
		}

		public void LogMessage(string msg)
		{
			Logger.Info(msg);
		}
	}
}
