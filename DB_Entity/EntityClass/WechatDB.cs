using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Entity.EntityClass
{
    public static class WechatDB
    {
        /// <summary>
        /// 插入消息信息到消息队列中
        /// </summary>
        /// <param name="cloudPushPool"></param>
        public static void InsertWechatMsg(Marketing_WechatQueue taskQueue)
        {
            try
            {
                using (DataBaseDataContext db = DbFactory.CreateContext())
                {
                    db.Marketing_WechatQueue.InsertOnSubmit(taskQueue);
                    db.SubmitChanges();
                }
            }
            catch (Exception ex)
            {
                //Utility.RecordException(ex);
            }
        }
    }
}
