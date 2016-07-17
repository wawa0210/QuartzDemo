using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMqService
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {

                var factory = new ConnectionFactory();
                factory.HostName = "192.168.1.106";
                factory.UserName = "test";
                factory.Password = "123456";

                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare("MyBabyWechatQueue", true, false, false, null);

                        var properties = channel.CreateBasicProperties();
                        properties.SetPersistent(true);

                        int loopFlag = 0;
                        while (true)
                        {
                            HttpWebRequest request = WebRequest.Create("http://202.96.204.238:30009/json/reply/QueryWebchatMessageRequest") as HttpWebRequest;

                            request.Method = "Get";
                            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                            if (Equals(response.StatusCode, HttpStatusCode.OK))
                            {
                                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                                {
                                    string responseStr = reader.ReadToEnd();
                                    var body = Encoding.UTF8.GetBytes(responseStr);

                                    channel.BasicPublish("", "MyBabyWechatQueue", null, body);
                                    //Console.WriteLine(responseStr);

                                }
                            }
                        }
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        protected override void OnStop()
        {
        }
    }
}
