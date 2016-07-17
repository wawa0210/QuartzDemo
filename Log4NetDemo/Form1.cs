using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log4NetDemo
{
    public partial class Form1 : Form
    {
        List<string> listRptPath = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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

                        string strPath1 = fbd.SelectedPath + "\\" + TheFolder.GetFiles()[count];
                        //判断当前文件是否已经判断为重复
                        if (listRptPath.Contains(strPath1))
                        {
                            continue;
                        }
                        else
                        {
                            for (var count2 = count + 1; count2 < TheFolder.GetFiles().Length; count2++)
                            {
                                string strPath2 = fbd.SelectedPath + "\\" + TheFolder.GetFiles()[count2];

                                //判断当前文件是否已经判断为重复
                                if (listRptPath.Contains(strPath2))
                                {
                                    continue;
                                }
                                else
                                {
                                    int[] countNew = imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(strPath1));
                                    int[] countNew1 = imgHelper.GetHisogram((Bitmap)System.Drawing.Image.FromFile(strPath2));
                                    if (imgHelper.GetResult(countNew, countNew1) == 1)
                                    {
                                        if (!listRptPath.Contains(strPath2))
                                        {
                                            listRptPath.Add(strPath2);
                                            LogHelper.WriteLog("File.Delete(\"" + strPath2 + "\");");
                                        }
                                    }
                                }
                            }
                        }
                    }
                    // label_Localhost.Content = "get succeed" + listRptPath.Count;
                }
            }
            MessageBox.Show("scan succeed!");
            GC.Collect();
            // MessageBox.Show("deleted succeed!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            while (true)
            {
                LogHelper.WriteLog("hello_" + DateTime.Now);
                Thread.Sleep(1000);
            }
        }
    }
}
