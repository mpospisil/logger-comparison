using log4net;
using log4net.Config;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;

namespace Log4netCoreTst
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync().GetAwaiter().GetResult();
		}


		private static async Task MainAsync()
		{

			Console.WriteLine("Log4Net performance test");

			LogComparisonCommon.ILogger mainLogger = new Log4NetFacade(typeof(Program));

			LogComparisonCommon.ILogger workLoadLogger = new Log4NetFacade(typeof(LogComparisonCommon.WorkLoad));
			var workLoad = new LogComparisonCommon.WorkLoad(workLoadLogger);

			TimeSpan oneTaskTotalTime;

			using (ThreadContext.Stacks["NDC"].Push("my context message"))
			{
				{
					var msg = "Starting no log workload - synchronous";
					Console.WriteLine(msg);
					mainLogger.LogMessage(msg);
					oneTaskTotalTime = await workLoad.DoOneTaskAsync(false);
					msg = $"Log4Net performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);
				}

				{
					var msg = "Starting log workload - synchronous";
					Console.WriteLine(msg);
					mainLogger.LogMessage(msg);
					oneTaskTotalTime = await workLoad.DoOneTaskAsync(true);
					msg = $"Log4Net performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);
				}

				{
					int processorCount = Environment.ProcessorCount;
					var msg = $"Starting asynchronous Log4Net performance test for {processorCount} running in parallel";
					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);

					oneTaskTotalTime = await workLoad.DoManyTasksAsync(processorCount, false);
					msg = $"Log4Net performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);
				}


				{
					int processorCount = Environment.ProcessorCount;
					var msg = $"Starting asynchronous Log4Net performance test for {processorCount} running in parallel";
					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);

					oneTaskTotalTime = await workLoad.DoManyTasksAsync(processorCount, true);
					msg = $"Log4Net performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

					mainLogger.LogMessage(msg);
					Console.WriteLine(msg);
				}

			}
		}
	}
}
