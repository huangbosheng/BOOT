using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace WindowsFormsApp9
{
    public partial class Form1 : Form
    {
        Thread drawThread = null;
        static Thread thread1, thread2;
        static string fileName;
        static string curFileName;

        static System.Drawing.Bitmap curBitmap;
        static Thread thread3;

        public Form1()
        {
            InitializeComponent();
        }




        public void button10_Click_1(object sender, EventArgs e)
        {
            try
            {
                closeThread();
                drawThread = new Thread(new ParameterizedThreadStart(thread));
                drawThread.SetApartmentState(ApartmentState.STA);
                drawThread.Start(); this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //关闭子线程
        private void closeThread()
        {
            if (drawThread != null)
            {
                if (drawThread.IsAlive)
                {
                    drawThread.Abort();
                }
            }
        }


       
       




        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            closeThread();           
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Title = "保存为";
            saveFile.OverwritePrompt = true;          
            saveFile.ShowHelp = true;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                string curFileName = saveFile.FileName;
                byte[] buff = new byte[1];
                FileStream fileStream = new FileStream(curFileName, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(buff, 0, buff.Length);
                binaryWriter.Close();
                fileStream.Close();
            }
        }

        //加载代码
        static void thread(Object dataobj)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "|*.nc;*.MP4; *.png; *.gif; *.jpeg; *.bmp";
            dialog.ShowDialog();
            try
            {
                fileName = dialog.FileName.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }                 
    }
}