using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartIdenifyImage
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        List<string> listRptPath = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string strName = "";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.ShowNewFolderButton = true;
                fbd.RootFolder = Environment.SpecialFolder.MyComputer;
                DialogResult result = fbd.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {

                    DirectoryInfo TheFolder = new DirectoryInfo(fbd.SelectedPath);
                    ImageHelper imgHelper = new ImageHelper();
                    string rPath = "";
                    for (var count = 0; count < TheFolder.GetFiles().Length; count++)
                    {
                        for (var count2 = count + 1; count2 < TheFolder.GetFiles().Length; count2++)
                        {
                            string strPath1 = fbd.SelectedPath + "\\" + TheFolder.GetFiles()[count];
                            string strPath2 = fbd.SelectedPath + "\\" + TheFolder.GetFiles()[count2];
                            int[] countNew = imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(strPath1));
                            int[] countNew1 = imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(strPath2));
                            if (imgHelper.GetResult(countNew, countNew1) == 1)
                            {
                                if (!listRptPath.Contains(strPath2))
                                {
                                    listRptPath.Add(strPath2);
                                    LogHelper.WriteLog(strPath2);
                                }
                            }
                        }
                    }
                    label_Localhost.Content = "get succeed" + listRptPath.Count;
                }
            }
            GC.Collect();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (listRptPath != null && listRptPath.Count >= 1)
            {
                foreach (var item in listRptPath)
                {
                    File.Delete(item);
                }
            }
            label_Localhost.Content = "delete succeed";
        }
    }
}
