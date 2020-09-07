using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LogComparisonCommon
{
	public class WorkLoad
	{
		readonly LogComparisonCommon.ILogger logger;
		public WorkLoad(LogComparisonCommon.ILogger logger)
		{
			this.logger = logger;
		}
		public TimeSpan DoOneTask(bool doLog)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			for (int i = 0; i < 1000; i++)
			{
				string msg = $"Log test {i}";
				if (doLog)
				{
					logger.LogMessage(msg);
				}
			}


			stopWatch.Stop();
			return stopWatch.Elapsed;
		}

		public async Task<TimeSpan> DoManyTasksAsync(int numberOfTasks, bool doLog)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();

			Task[] tasks = new Task[numberOfTasks];
			for(int i = 0; i < numberOfTasks; i++)
			{
				var task = DoOneTaskAsync(doLog);
				tasks[i] = task;
			}

			await Task.WhenAll(tasks);


			stopWatch.Stop();
			return stopWatch.Elapsed;
		}


		public async Task<TimeSpan> DoOneTaskAsync(bool doLog)
		{
			TimeSpan res = await Task.Factory.StartNew(() =>
			{
				 return DoOneTask(doLog);
			});
			return res;
		}
	}
}
