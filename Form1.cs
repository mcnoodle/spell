using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Utilities;
using mshtml;
using System.Configuration;
using System.Threading;
using Model;
using BLL;
using System.Text.RegularExpressions;
using SqliteDAL;
using System.Reflection;
namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            t = new Thread(voteThread);
            t.IsBackground = true;
            Load += new EventHandler(Form1_Load);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Sqlite.connectstring = string.Format("data source={0}/db/AgentIP;pooling=True", Application.StartupPath);

            allIPList = ipBLL.GetAllAgentIP();
        }
        List<AgentIPModel> allIPList = new List<AgentIPModel>();

        //中晟72，对手70，测试65，61
        const int VoteId = 72;

        void Application_ApplicationExit(object sender, EventArgs e)
        {
            voteThreadLive = false;
            voteMainLive = false;
            promaxy1.IEProxy.ReSetProxy("");

            //throw new NotImplementedException();
        }

        void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ipBLL.GetAllAgentIP();
            t.Start();
        }
        string url = "http://gztx.chinappzw.com/pptp.aspx?b=875";
        //javascript:vote(70,'new_vouch1','newvouch1','li','5','directory1','directory1sub5')
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //webBrowser1.silen
            btnVote.Enabled = true;
            webBrowserOK = true;
            label2.Text = "ready";

            var htmlDoc = (IHTMLDocument3)webBrowser1.Document.DomDocument;
            HTMLHeadElement head = htmlDoc.getElementsByTagName("head").Cast<HTMLHeadElement>().First();
            var script = (IHTMLScriptElement)((IHTMLDocument2)htmlDoc).createElement("script");


            /*  function vote() {
                 document.getElementById("ContentPlaceHolder1_UC_pptp_vote1_btn_vote").click();
             }*/
            string scriptline10 = @" var g_msg=""""; ";
            //scriptline10 += @"function vote() {";
            //scriptline10 += @"  document.getElementById(""ContentPlaceHolder1_UC_pptp_vote1_btn_vote"").click();";
            //scriptline10 += @"   }	";

            scriptline10 += @"function enterclick() {";
            scriptline10 += @" document.forms[0].submit();";
            scriptline10 += @"   }	";



            script.text = scriptline10;
            head.appendChild((IHTMLDOMNode)script);


        }

        int getCount { get { return Convert.ToInt32(ConfigurationManager.AppSettings["cc"]); } }
        Thread t;

        int totalSetCount = 0;
        int total1Count = 0;
        int total0Count = 0;
        int totalEmptyCount = 0;
        int totalOther = 0;
        string currentIP = "";

        void LabelRecord(Label label, string info)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() =>
                {
                    label.Text = info;
                }));
            }
            else
            {
                label.Text = info;
            }
        }

        bool voteMainLive = true;
        bool voteThreadLive = false;
        bool webBrowserOK = false;
        int voteState = 0;

        DeviceTimer voteTimer = new DeviceTimer();
        R_TRIG voteTrig = new R_TRIG();

        DateTime lastUpdate = DateTime.MinValue;
        string newip = "";

        void vote(List<AgentIPModel> ipList)
        {
            foreach (AgentIPModel ipm in ipList)
            {
                //是否需要检验？

                //投票
                newip = string.Format("{0}:{1}", ipm.ip, ipm.port);
                promaxy1.IEProxy.SetProxy(newip);
                webBrowserOK = false;
                webBrowser1.Navigate(newip);

                for (int i = 10; i > 0; i--)
                {
                    Thread.Sleep(800);
                    if (webBrowserOK)
                        break;
                }
                if (webBrowserOK)
                {
                    addClick();
                    Thread.Sleep(1000);

                    if (webBrowser1.InvokeRequired)
                    {
                        webBrowser1.Invoke(new Action(() =>
                        {
                            IHTMLDocument2 vDocument = webBrowser1.Document.DomDocument as IHTMLDocument2;
                            IHTMLWindow2 vWindow = (IHTMLWindow2)vDocument.parentWindow;
                            Type vWindowType = vWindow.GetType();
                            object testText = vWindowType.InvokeMember("g_msg", BindingFlags.GetProperty, null, vWindow, new object[] { });
                            if (testText + "" == "1")
                            {
                                //成功
                                totalSetCount++;
                                total1Count++;
                                ipm.LastSuccess = true;
                            }
                            else if (testText + "" == "0")
                            {
                                total0Count++;
                            }
                            else if (testText + "" == "")
                            {
                                totalEmptyCount++;
                            }
                            else
                            {
                                totalOther++;
                            }
                        }));
                    }

                    ipm.LastCheckData = DateTime.Now;
                    ipBLL.Update(ipm);
                    Thread.Sleep(2000);
                }
                Thread.Sleep(100);
            }

        }

        /// <summary>
        ///     //// 读取     Console.WriteLine(testText);  
        //// 设置  
        ///vWindowType.InvokeMember("testText",         BindingFlags.SetProperty, null, vWindow, new object[] { "Zswang 路过" });
        //// 执行方法 
        ///vWindowType.InvokeMember("ShowMessage",         BindingFlags.InvokeMethod, null, vWindow, new object[] { 12345 });
        /// </summary>
        void voteThread()
        {
            voteTimer.Start(3600000.0);
            while (voteMainLive)
            {
                try
                {
                    //try agentip until ok 
                    switch (voteState)
                    {
                        case 20:
                            postSearch();
                            voteState = -1;
                            break; 
                        default:
                            break;
                    }

                   

                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                    //voteState = 2;
                }
                Thread.Sleep(1000);
            }
        }

        void postSearch()
        {
            if (webBrowser1.InvokeRequired)
            {
                webBrowser1.Invoke(
                new Action(() =>
                { 
                    for (int i = 1; i > 0; i--)
                    {
                        object[] objects = new object[1];
                        objects[0] = VoteId;
                        webBrowser1.Document.InvokeScript("enterclick", objects);
                        Thread.Sleep(5);
                    }
                }));
            }
            else
            { 
                for (int i = 1; i > 0; i--)
                {
                    object[] objects = new object[1];
                    objects[0] = VoteId;
                    webBrowser1.Document.InvokeScript("enterclick", objects);

                    Thread.Sleep(5);
                }
            }
        }

        void addClick()
        {

            if (webBrowser1.InvokeRequired)
            {
                webBrowser1.Invoke(
                new Action(() =>
                {
                    //getCount
                    for (int i = 1; i > 0; i--)
                    {
                        object[] objects = new object[1];
                        objects[0] = VoteId;
                        webBrowser1.Document.InvokeScript("vote", objects);
                        Thread.Sleep(5);
                    }
                }));
            }
            else
            {
                //getCount
                for (int i = 1; i > 0; i--)
                {
                    object[] objects = new object[1];
                    objects[0] = VoteId;
                    webBrowser1.Document.InvokeScript("vote", objects);

                    Thread.Sleep(5);
                }
            }
            LabelRecord(label2, string.Format("确定成功投票：{0}票,{4}失败投票:{1}票,{4}可能成功投票:{2}票,{4}可能成功投票:{3}票", total1Count, total0Count, totalEmptyCount, totalOther, Environment.NewLine));
            LabelRecord(label3, "当前IP" + newip);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            voteThreadLive = true;
            // t.Start();
        }

        AgentIPBLL ipBLL = new AgentIPBLL();

        /// <summary>
        /// http://www.goodips.com/?ip=&port=&dengji=&adr=&checktime=&sleep=&cunhuo=&px=&pageid=2;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        List<AgentIPModel> GetAgentIPs(string url)
        {
            List<AgentIPModel> mList = new List<AgentIPModel>();
            string html = Agent.GetHTMLCode(url, Encoding.UTF8);

            #region 解析

            Regex re = new Regex(@"class\=\""ctable_head\""\>(.*?)\<\/table\>", RegexOptions.Multiline);
            MatchCollection mc = re.Matches(html.Replace("\r", "").Replace("\n", ""));
            try
            {
                string tds = mc[0].Groups[1].Value.Replace("\t", "");
                Regex retds = new Regex(@"\<tr\>(.*?)\<td\>(.*?)\<\/td\>(.*?)\<td\>(.*?)\<\/td\>(.*?)\<\/tr\>", RegexOptions.Multiline);
                MatchCollection tdmc = retds.Matches(tds);
                foreach (Match mt in tdmc)
                {
                    AgentIPModel ipModel = new AgentIPModel();
                    ipModel.LastCheckData = DateTime.Now;
                    ipModel.ip = mt.Groups[2].Value;
                    ipModel.port = mt.Groups[4].Value;
                    string newip_temp = string.Format("{0}:{1}", ipModel.ip, ipModel.port);
                    if (promaxy1.Pro2.checkProxyIP(newip_temp, 10000))
                    {
                        //数据库保存ip
                        ipBLL.SetAgent(ipModel, true);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }


            #endregion

            return mList;

        }
       // string navigateurl = @"http://www.ofweek.com/award/2013/led/wangluotoupiao.html";
        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ipBLL.GetAllAgentIP();
            ////voteThread();
            ////Application.DoEvents(); 
            //webBrowser1.Navigate(url);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            promaxy1.IEProxy.ReSetProxy("");
        }

        int pageid = 1;
        private void button4_Click(object sender, EventArgs e)
        {
            string url = @"http://www.goodips.com/?pageid=" + pageid; //@"http://www.goodips.com/?ip=&port=&dengji=&adr=&checktime=&sleep=&cunhuo=&px=&pageid=2";
            voteMainLive = false;
            GetAgentIPs(url);
        }



        private void btnVote_Click(object sender, EventArgs e)
        {

            //获取80个代理ip
            voteThreadLive = true;
            //遍历ip投票
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            // string url = dataGridView1.Rows[e. ;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                string url = dataGridView1.Rows[e.RowIndex].Cells[1].Value + ":" + dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                promaxy1.IEProxy.SetProxy(url);
                lblCurrentIP.Text = "当前IP：" + url;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// return success
        /// </summary>
        /// <param name="agent"></param>
        /// <returns></returns>
        bool singleAgent(AgentIPModel agent)
        {
            newip = string.Format("{0}:{1}", agent.ip, agent.port);
            promaxy1.IEProxy.SetProxy(newip);
            webBrowserOK = false;
            webBrowser1.Navigate(url);

            for (int i = 10; i > 0; i--)
            {
                Thread.Sleep(800);
                if (webBrowserOK)
                    break;
            }
            if (webBrowserOK)
            {
                LabelRecord(label3, "当前IP" + agent.ip);
                return true;
                //addClick();
            }
            return false;

        }

        private int currentStep = 0;
        private void btntest_Click(object sender, EventArgs e)
        {
            voteState = 20;

        }


    }
}
