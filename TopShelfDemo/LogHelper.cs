//------------------------------------------------------------------------------------------------
//
//
//         All rights reserved
//
//		   filename:LogHelper
//		   desciption:
//
//		   created by 张潇 at 2016/6/20 17:54:56
//
//------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace TopShelfDemo
{
    public class LogHelper
    {
        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="ex"></param>
        #region static void WriteLog(Type t, Exception ex)

        public static void WriteLog(Type t, Exception ex)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(t);
            log.Error("Error", ex);
        }

        #endregion

        /// <summary>
        /// 输出日志到Log4Net
        /// </summary>
        /// <param name="t"></param>
        /// <param name="msg"></param>
        #region static void WriteLog(string msg)

        public static void WriteLog(string msg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("wawa");
            log.Info(msg);
            log.Error(msg);
        }

        #endregion
    }
}
