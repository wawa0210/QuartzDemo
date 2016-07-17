using Amib.Threading;
using DB_Entity;
using DB_Entity.EntityClass;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQReceiver
{
    class Receive
    {

        static void Main(string[] args)
        {
            ReceiveMessage();
        }

        public static void ReceiveMessage()
        {
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                factory.UserName = "test1";
                factory.Password = "123456";
                factory.VirtualHost = "wawa";
                factory.AutomaticRecoveryEnabled = true;

                using (var connection = factory.CreateConnection())
                {
                    STPStartInfo stp = new STPStartInfo();
                    stp.DisposeOfStateObjects = true;
                    stp.CallToPostExecute = CallToPostExecute.Always;
                    stp.ThreadPriority = ThreadPriority.Highest;
                    stp.UseCallerCallContext = true;
                    stp.MinWorkerThreads = 5;
                    stp.MaxWorkerThreads = 10;

                    SmartThreadPool smartThreadPool = new SmartThreadPool(stp);

                    smartThreadPool.QueueWorkItem(new WorkItemCallback(ReceiveMessageDetails), connection);
                    Console.ReadLine();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        public static object ReceiveMessageDetails(object conn)
        {
            IConnection con = (IConnection)conn;
            using (var channel = con.CreateModel())
            {
                //在MQ上定义一个持久化队列，如果名称相同不会重复创建
                channel.QueueDeclare("MyBabyWechatQueue", true, false, false, null);

                //所有负载均衡到每一个工作consumer 中 输入1，那如果接收一个消息，但是没有应答，则客户端不会收到下一个消息
                channel.BasicQos(0, 1, false);

                //在队列上定义一个消费者
                QueueingBasicConsumer consumer = new QueueingBasicConsumer(channel);

                //消费队列，并设置应答模式为程序主动应答
                channel.BasicConsume("MyBabyWechatQueue", false, consumer);

                while (true)
                {
                    //阻塞函数，获取队列中的消息
                    BasicDeliverEventArgs ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();
                    byte[] bytes = ea.Body;
                    string postData = Encoding.UTF8.GetString(bytes);

                    Console.WriteLine(postData);

                    //回复确认
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            }
            Console.ReadLine();

            return null;
        }
    }
}
