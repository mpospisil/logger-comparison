using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Threading.Tasks;

namespace SerilogTst
{
	class Program
	{
		static void Main(string[] args)
		{
			MainAsync().GetAwaiter().GetResult();
		}

		private static async Task MainAsync()
		{
			Console.WriteLine("Serilog performance test");

			LogComparisonCommon.ILogger mainLogger = new SerilogFacade(typeof(Program));

			//LogComparisonCommon.ILogger workLoadLogger = new SerilogFacade(typeof(LogComparisonCommon.WorkLoad));
			var workLoad = new LogComparisonCommon.WorkLoad(mainLogger);

			TimeSpan oneTaskTotalTime;

			{
				var msg = "Starting no log workload - synchronous";
				Console.WriteLine(msg);
				mainLogger.LogMessage(msg);
				oneTaskTotalTime = await workLoad.DoOneTaskAsync(false);
				msg = $"Serilog performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);
			}

			{
				var msg = "Starting log workload - synchronous";
				Console.WriteLine(msg);
				mainLogger.LogMessage(msg);
				oneTaskTotalTime = await workLoad.DoOneTaskAsync(true);
				msg = $"Serilog performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);
			}

			{
				int processorCount = Environment.ProcessorCount;
				var msg = $"Starting asynchronous NLog performance test for {processorCount} running in parallel";
				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);

				oneTaskTotalTime = await workLoad.DoManyTasksAsync(processorCount, false);
				msg = $"NLog performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);
			}

			{
				int processorCount = Environment.ProcessorCount;
				var msg = $"Starting asynchronous NLog performance test for {processorCount} running in parallel";
				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);

				oneTaskTotalTime = await workLoad.DoManyTasksAsync(processorCount, true);
				msg = $"NLog performance test is finished - time : {oneTaskTotalTime.TotalSeconds} s ({oneTaskTotalTime.TotalMilliseconds} ms)";

				mainLogger.LogMessage(msg);
				Console.WriteLine(msg);
			}
		}
	}
}
