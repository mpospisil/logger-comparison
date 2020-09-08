# logger-comparison
Comparison of the performance of logging frameworks for dotnet core - Log4Net, Nlog and Serilog

| Logger     | 1000 messages / one thread | 1000 messages / 8 thread  |
| -----------|:--------------------------:| -------------------------:|
| [Log4net](http://logging.apache.org/log4net/)    | 4 s   | 32 s   |
| [NLog](https://nlog-project.org/)       | 4 s   | 28 s   |
| [Serilog](https://serilog.net/)    | 0.01 s| 0.17 s |


