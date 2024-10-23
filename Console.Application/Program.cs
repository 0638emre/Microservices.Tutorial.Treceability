


using System.Diagnostics;
using NLog;

Logger logger = LogManager.GetCurrentClassLogger();
Trace.CorrelationManager.ActivityId = Guid.NewGuid(); // dbye eklenen guid ve o transaction silsilesinin takibi.

Work1();
Work2();
Work3();

void Work1()
{
    Console.WriteLine("Work 1 triggered !");
    logger.Debug("work1 log triggered ");
} 

void Work2()
{
    Console.WriteLine("Work 2 triggered !");
    logger.Debug("work2 log triggered ");

} 

void Work3()
{
    Console.WriteLine("Work 3 triggered !");
    logger.Debug("work2 log triggered ");
} 

Console.WriteLine("Hello, World!");