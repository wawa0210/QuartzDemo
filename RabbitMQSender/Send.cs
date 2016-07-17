using Amib.Threading;
using DB_Entity;
using DB_Entity.EntityClass;
using Log4NetDemo;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQSender
{
    class Send
    {
        private static IConnection conn { get; set; }
        static void Main(string[] args)
        {

            var factory = new ConnectionFactory();
            factory.HostName = "localhost";
            //factory.HostName = "10.8.11.75";
            factory.UserName = "test1";
            factory.Password = "123456";
            factory.VirtualHost = "wawa";
            factory.AutomaticRecoveryEnabled = true;
            conn = factory.CreateConnection();

            //SmartThreadPool smartThreadPool = new SmartThreadPool();
            //smartThreadPool.MinThreads = 10;
            //smartThreadPool.MaxThreads = 20;//最大线程数量，数据库配置
            //smartThreadPool.QueueWorkItem(new WorkItemCallback(PushMessage), null);
            //for (var count = 0; count < 10; count++)
            //{
            //    PushMessage(connection);
            //}

            //ThreadPool.SetMinThreads(10, 10);
            ThreadPool.SetMaxThreads(50, 50);
            for (var count = 0; count < 1; count++)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(PushMessage), conn);
            }
        }


        private static void SendMessage()
        {
            try
            {

                var factory = new ConnectionFactory();
                factory.HostName = "10.8.17.69";
                //factory.HostName = "10.8.11.75";
                factory.UserName = "test1";
                factory.Password = "123456";
                factory.VirtualHost = "wawa";
                factory.AutomaticRecoveryEnabled = true;

                using (var connection = factory.CreateConnection())
                {
                    //for (var count = 0; count < 10; count++)
                    //{
                    //SendChannelMsg(connection);
                    //Thread receiveThread = new Thread(new ThreadStart(SendChannelMsg));
                    //Thread receiveThread = new Thread(new ParameterizedThreadStart(SendChannelMsg));
                    //receiveThread.Start(connection);
                    //ThreadPool.SetMaxThreads(10, 10);
                    //ThreadPool.SetMinThreads(5, 5);
                    //ThreadPool.QueueUserWorkItem(new WaitCallback(SendChannelMsg), connection);
                    // }

                    SmartThreadPool smartThreadPool = new SmartThreadPool();
                    smartThreadPool.MinThreads = 50;
                    smartThreadPool.MaxThreads = 100;
                    smartThreadPool.QueueWorkItem(new WorkItemCallback(SendChannelMsg), connection);
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private static object SendChannelMsg(object conn)
        {
            IConnection con = (IConnection)conn;
            using (var channel = con.CreateModel())
            {

                channel.QueueDeclare("ha.wawaQueue", true, false, false, null);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2;

                int loopFlag = 0;
                while (true)
                {
                    string responseStr = "1121210|{\"ToUser\":[\"oTbbcjk1iRkj9MpposV8VIoAbEHQ\"],\"TopColor\":\"#000000\",\"Url\":\"http://m.muyingzhijia.com/\",\"TemplateId\":\"72g2RNGAUV5yfnecGKicSNqQ8HLsacGsZIkuJD-cVMc\",\"Data\":{\"keyword1\":{\"color\":\"#000000\",\"value\":\"85001037\"},\"keyword2\":{\"color\":\"#000000\",\"value\":\"TUTUYA14D卡通熊加绒仿皮护耳飞行员帽 蓝 帽围46-52cm\"},\"keyword3\":{\"color\":\"#000000\",\"value\":\"15711111111\"},\"keyword4\":{\"color\":\"#000000\",\"value\":\"2016/4/26 10:15:35\"},\"first\":{\"color\":\"#000000\",\"value\":\"您的订单已确认收货，交易完成，感谢您对母婴之家的支持！\"},\"remark\":{\"color\":\"#000000\",\"value\":\"点击查看订单详情，发表您对商品的评价！\"}}}";
                    var body = Encoding.UTF8.GetBytes(responseStr);
                    Console.WriteLine(responseStr);
                    channel.BasicPublish("", "ha.wawaQueue", properties, body);
                }

                //channel.ExchangeDeclare("toDemo", ExchangeType.Direct, true, false, null);

                //var properties = channel.CreateBasicProperties();
                //properties.DeliveryMode = 2;

                //int loopFlag = 0;
                //while (true)
                //{
                //    //string responseStr = "hello the world! " + DateTime.Now.ToString("yyyyMMddhhmmssffffff");
                //    string responseStr = "1121210|{\"ToUser\":[\"oTbbcjk1iRkj9MpposV8VIoAbEHQ\"],\"TopColor\":\"#000000\",\"Url\":\"http://m.muyingzhijia.com/\",\"TemplateId\":\"72g2RNGAUV5yfnecGKicSNqQ8HLsacGsZIkuJD-cVMc\",\"Data\":{\"keyword1\":{\"color\":\"#000000\",\"value\":\"85001037\"},\"keyword2\":{\"color\":\"#000000\",\"value\":\"TUTUYA14D卡通熊加绒仿皮护耳飞行员帽 蓝 帽围46-52cm\"},\"keyword3\":{\"color\":\"#000000\",\"value\":\"15711111111\"},\"keyword4\":{\"color\":\"#000000\",\"value\":\"2016/4/26 10:15:35\"},\"first\":{\"color\":\"#000000\",\"value\":\"您的订单已确认收货，交易完成，感谢您对母婴之家的支持！\"},\"remark\":{\"color\":\"#000000\",\"value\":\"点击查看订单详情，发表您对商品的评价！\"}}}";
                //    var body = Encoding.UTF8.GetBytes(responseStr);

                //    channel.BasicPublish("toDemo", "helloInfo", properties, body);
                //    Console.WriteLine(responseStr);
                //    //Thread.Sleep(1000);
                //}

            }
            return null;
        }


        private static void PushMessage(object con)
        {

            var channel = conn.CreateModel();
            channel.QueueDeclare("ha.MyBabyWechatQueue", true, false, false, null);
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2;

            int i = 0;
            while (true)
            {
                string responseStr = "hello" + i.ToString();
                var body = Encoding.UTF8.GetBytes(responseStr);
                channel.BasicPublish("", "ha.MyBabyWechatQueue", properties, body);
                LogHelper.WriteLog(responseStr);
                i++;
            }
        }
    }
}
