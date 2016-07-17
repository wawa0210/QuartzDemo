using PetaPoco;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PetaPocoDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string connectionStr = ConfigurationManager.ConnectionStrings["SunyuCRM"].ConnectionString.ToString();

            //根据配置文件实例数据库上下文
            var db = new Database("SunyuCRM");

            //long count = db.ExecuteScalar<long>("SELECT Count(*) FROM Base_Service");

            var listServices = db.Page<Base_Service>(1, 10, "SELECT * FROM Base_Service");

            foreach (var item in listServices.Items)
            {
                txt_Content.AppendText(item.ServiceCode + "_______" + item.ServiceName + "\r\n");
                Thread.Sleep(100);
            }
        }
    }
}
