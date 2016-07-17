using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Entity.EntityClass
{
    public static class CloudPushDB
    {
        public static List<Marketing_MsgPool> GetListReadyCloudPushList()
        {

            List<Marketing_MsgPool> listMsgPool = new List<Marketing_MsgPool>();
            try
            {
                using (DataBaseDataContext db = DbFactory.CreateContext())
                {
                    db.CommandTimeout = 600;
                    listMsgPool = db.ExecuteQuery<Marketing_MsgPool>(" exec P_GetBatchMarketingMsg ").ToList();
                }
            }
            catch (Exception ex)
            {
                //Utility.RecordException(ex);
            }
            return listMsgPool;
        }

        /// <summary>
        /// 插入消息信息到消息队列中
        /// </summary>
        /// <param name="cloudPushPool"></param>
        public static void InsertMarketingMsg(Marketing_MsgPool msgpool)
        {
            try
            {
                using (DataBaseDataContext db = DbFactory.CreateContext())
                {
                    Marketing_MsgQueue msgQueue = db.Marketing_MsgQueue.FirstOrDefault(f => f.Id == msgpool.Id.ToString());
                    if (msgQueue == null)
                    {
                        msgQueue = new Marketing_MsgQueue();
                        msgQueue.Id = msgpool.Id.ToString();
                        msgQueue.CreateTime = msgpool.CreateTime;
                        msgQueue.Email = msgpool.Email;
                        msgQueue.ExpectedTime = msgpool.ExpectedTime;
                        msgQueue.LastSendTime = DateTime.Now;
                        msgQueue.Mobile = msgpool.Mobile;
                        msgQueue.MsgBatchCode = msgpool.MsgBatchCode;
                        msgQueue.MsgChannel = msgpool.MsgChannel;
                        msgQueue.MsgContent = msgpool.MsgContent;
                        msgQueue.MsgTitle = msgpool.MsgTitle;
                        msgQueue.Priority = msgpool.Priority;
                        msgQueue.SendCount = msgpool.SendCount;
                        msgQueue.Status = msgpool.Status;
                        msgQueue.TemplateCode = msgpool.TemplateCode;
                        msgQueue.RefCode = msgpool.RefCode;
                        msgQueue.RefType = msgpool.RefType;
                        msgQueue.UserCode = msgpool.UserCode;
                        msgQueue.Memo = msgpool.Memo;
                        db.Marketing_MsgQueue.InsertOnSubmit(msgQueue);
                        db.SubmitChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.RecordException(ex);
            }
        }
    }
}
