//------------------------------------------------------------------------------------------------
//
//
//         All rights reserved
//
//		   filename:PrintDateTime
//		   desciption:
//
//		   created by 张潇 at 2016/7/14 23:24:50
//
//------------------------------------------------------------------------------------------------

using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Log_BLL
{
    [Description("打印当前日期")]
    public class PrintDateTime : IJob, IBase
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
}
