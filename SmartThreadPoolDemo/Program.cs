using Amib.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "App.config", Watch = true)]
namespace SmartThreadPoolDemo
{
    class Program
    {
        private static IWorkItemsGroup _workItemsGroup;
        private static SmartThreadPool smartThreadPool;
        static void Main(string[] args)
        {
            STPStartInfo stp = new STPStartInfo() { IdleTimeout = 1000, MaxWorkerThreads = 25, MinWorkerThreads = 10, EnableLocalPerformanceCounters = true };

            smartThreadPool = new SmartThreadPool(stp);
            while (true)
            {
                smartThreadPool.Join(DoWork, DoWork, DoWork, DoWork, DoWork, DoWork, DoWork, DoWork);
                Thread.Sleep(1000);
            }
            Console.ReadKey();
        }
        private static void DoWork()
        {
            log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log.Info("hello the world!");
            Console.WriteLine("DoWork " + "......" + smartThreadPool.ActiveThreads.ToString() + "......" + smartThreadPool.InUseThreads + "......");
        }
    }
}
