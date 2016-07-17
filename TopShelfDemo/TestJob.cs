//------------------------------------------------------------------------------------------------
//
//
//         All rights reserved
//
//		   filename:TestJob
//		   desciption:
//
//		   created by 张潇 at 2016/6/20 17:56:28
//
//------------------------------------------------------------------------------------------------

using Common.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopShelfDemo
{
    public sealed class TestJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LogHelper.WriteLog("TestJob测试");
        }
    }
}
