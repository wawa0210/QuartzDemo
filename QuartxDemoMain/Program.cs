using Log_BLL;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartxDemoMain
{
    class Program
    {
        static void Main(string[] args)
        {
            //从工厂中获取一个调度器实例化
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            //Student student = new Student();
            //student.Name = "wawa";
            //student.Sno = "2011021045";
            //IDictionary<string, object> mapData = new Dictionary<string, object>();
            //mapData.Add("student", student);

            ////==========例子2 (执行时 作业数据传递，时间表达式使用)===========

            //IJobDetail job2 = JobBuilder.Create<PrintDateTime>()
            //                            .WithIdentity("myJob", "group1")
            //                            .UsingJobData("jobSays", "Hello").UsingJobData(new JobDataMap(mapData))
            //                            .Build();


            //ITrigger trigger2 = TriggerBuilder.Create()
            //                            .WithIdentity("mytrigger2", "group1")
            //                            .StartNow()
            //                            .WithCronSchedule("/5 * * ? * *")    //时间表达式，5秒一次     
            //                            .Build();

            ITrigger trigger1 = TriggerBuilder.Create()
                                      .WithIdentity("mytrigger", "group1")
                                      .StartNow()
                                      .WithCronSchedule("/1 * * ? * *")    //时间表达式，5秒一次     
                                      .Build();

            string dllPath = AppDomain.CurrentDomain.BaseDirectory + "Log_BLL.dll";
            var asm = Assembly.LoadFrom(dllPath);

            Type[] arrayType = asm.GetTypes();

            JobDetail job1 = new JobDetail("job1", "group1", typeof(PrintDateTime));

            scheduler.ScheduleJob(job1, trigger1);
        }
    }

    /// <summary>
    /// 作业
    /// </summary>
    public class HelloJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("task excute，jobSays:hello the world!");
            Thread t = new Thread(new ThreadStart(Hello));
            t.Start();
        }
        public void Hello()
        {
            Console.WriteLine("task excute");
        }
    }
    public class DumbJob : IJob
    {
        /// <summary>
        ///  context 可以获取当前Job的各种状态。
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            //Student student = dataMap.GetString("student") as Student;

            //Console.WriteLine(student.Name + "____" + student.Sno);
            Console.WriteLine("task excute");
        }
    }


    public class Student
    {
        public string Name { get; set; }

        public string Sno { get; set; }
    }
}
