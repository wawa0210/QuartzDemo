using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmartIdenifyImage
{
    /// <summary>
    /// ImageIdentify.xaml 的交互逻辑
    /// </summary>
    public partial class ImageIdentify : Window
    {
        public ImageIdentify()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            lb_Img1Url.Content = GetImgPath();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            lb_Img2Url.Content = GetImgPath();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ImageHelper imgHelper = new ImageHelper();

            lb_result.Content = imgHelper.GetResult(imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(lb_Img1Url.Content.ToString())), imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(lb_Img2Url.Content.ToString()))).ToString();
        }

        private string GetImgPath()
        {
            string strPath = "";
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                strPath = ofd.FileName;
            }
            return strPath;
        }
    }
}
