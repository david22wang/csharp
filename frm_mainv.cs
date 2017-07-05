using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;

using AppUtility;
using DotNet4.Utilities;
using System.IO;
namespace WebInfoCollect
{
    public partial class frm_mainv : Form
    {
        public frm_mainv()
        {
            InitializeComponent();
        }

        #region 任务
        private string net_type = "";
        private string net_route_type = "";
        private string net_route_ip = "";
        private string net_route_user = "";
        private string net_route_pwd = "";
        private int judge_ip_used(string ip)
        {//如果用过就返回一个1
            //lv_account.SelectedItems[0].SubItems[1].Text

            for (int i = 0; i < lv_task_finished.Items.Count; i++)
            {
                if (lv_task_finished.Items[i].SubItems[1].Text.Trim().Length == 0)
                {//如果出现空值，退出
                    break;
                }

                if (lv_task_finished.Items[i].SubItems[1].Text.Trim() == ip)
                {
                    return (1);
                    break;

                }

            }
            return (0);

        }
        #endregion

        private string logined = "";
        private string useragent = "";
        int int_account = 0;
        int int_task_bg_pause_min = 10;//任务暂停起始时间
        int int_task_bg_pause_max = 30;//任务暂停起始时间

        private void btn_login_Click(object sender, EventArgs e)
        {
           
            txt_ip.ForeColor = Color.Black;
            if (txt_ip.TextLength == 0)
            {
                MessageBox.Show("IP没有得到");
                return;

            }
            else
            {//判断是不是IP,用过 

                if (judge_ip_used(txt_ip.Text) == 1)
                {
                    txt_ip.ForeColor = Color.Red;
                }
            }


            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            int t = lv_task.SelectedItems.Count;
            string str_us;
            string str_pw;
            if (t == 1)
            {
                str_us = lv_task.SelectedItems[0].SubItems[0].Text.Trim();//用户名

                str_pw = lv_task.SelectedItems[0].SubItems[1].Text.Trim();//秘码
            }
            else
            {
                MessageBox.Show("请选择一个账号");
                return;
            }
            //===================
            int str_1 = task_huan168.task_login("", str_us, str_pw, useragent);
             logined = task_huan168.logined_cookied;
             if (logined.Length > 0)
             {
                 btn_trade.Enabled = true;
             }

        }

        private void btn_save_id_Click(object sender, EventArgs e)
        {//修改个人信息，
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION
            string nickname = "smihthwang";
            string n1 = "smith";
            Random rdm = new Random();
            double randomDouble = rdm.NextDouble();
            string qql = randomDouble.ToString().Replace("0", "").Replace(".", "").Substring(0, 9);
            nickname = "qq" + qql+"wan";
            int int_code = task_huan168.task_info_modify("", useragent, nickname, qql);//提交任务
        }

        private void frm_mainv_Shown(object sender, EventArgs e)
        {

            string ph = System.IO.Directory.GetCurrentDirectory();
            IniHelper ini;
            string str_app_run = ph + "\\app.ini";//程序运行
            string f1 = "";
            if (System.IO.File.Exists(str_app_run))
            {
                ini = new IniHelper(str_app_run);
                //上网参数
                f1 = ini.ReadValue("app", "configfilepath").ToLower();
                int_task_bg_pause_min =Int16.Parse(ini.ReadValue("app", "int_task_bg_pause_min").ToLower());
                int_task_bg_pause_max = Int16.Parse(ini.ReadValue("app", "int_task_bg_pause_max").ToLower());

            }
            else
            {
                string str_app_file = "[app]\n";
                str_app_file = str_app_file + "configfilepath=d:\\config.ini\n";
                str_app_file = str_app_file + "int_task_bg_pause_min=2\n";
                str_app_file = str_app_file + "int_task_bg_pause_max=5\n";

                System.IO.File.AppendAllText(str_app_run,str_app_file);//保存
            
            }

            if (f1.Length == 0)
            {//如果f1为空，DEFAULT为C盘
                f1 = "d:\\config.ini";
            }
          
          
            string pa1, pa2,  pa3;
            
            Cls_Task obj_task = new Cls_Task();
            DateTime now = DateTime.Now;

            string t1, t2, t3;
            if (System.IO.File.Exists(f1))
            {
                ini = new IniHelper(f1);
                //上网参数
                net_type = ini.ReadValue("net", "type").ToLower();
                net_route_type = ini.ReadValue("net", "routetype").ToLower();
                net_route_ip = ini.ReadValue("net", "ip").ToLower();
                net_route_user = ini.ReadValue("net", "user").ToLower();
                net_route_pwd = ini.ReadValue("net", "pwd").ToLower();
                //加载浏览器
                useragent = ini.ReadValue("browser", "useragent").Trim();
                //加载任务
                pa1 = ini.ReadValue("account", "size").ToLower();
                int_account = Int16.Parse(pa1);
                int s = 0;
                for (int j = 1; j < int_account + 1; j++)
                {//加载帐户 
                    pa2 = ini.ReadValue("account" + j.ToString(), "user");
                    pa3 = ini.ReadValue("account" + j.ToString(), "pwd");
                    if(j>5)
                    {
                        s=s+1;
                         t1 = ini.ReadValue("personinfo" + s.ToString(), "t1");
                         t2 = ini.ReadValue("personinfo" + s.ToString(), "t2");
                         t3 = ini.ReadValue("personinfo" + s.ToString(), "t3");
                    
                    }
                    else
                    {
                        t1="";
                        t2="";
                        t3="";

                    }

                   
                    obj_task.add_task1(pa2, pa3,t1,t2,t3,lv_task);
                }

            }
            else
            {
                MessageBox.Show("系统找不到配置文件,把配置文件放到D盘根目录下");
            }
            //加载完成任务
            string str_fn = ph + "\\" + now.ToString("yyMMdd") + ".ini";//完成任务

            if (System.IO.File.Exists(str_fn))
            {
                ini = new IniHelper(str_fn);//加载任务完成
                for (int j = 1; j < int_account + 1; j++)
                {//加载帐户 
                    pa2 = ini.ReadValue("taskfinished" ,"account"+ j.ToString());
                    pa3= ini.ReadValue("taskfinished", "ip" + j.ToString());
                    obj_task.add_finishedtask(pa2, lv_task_finished,pa3);
                }

            }
            else
            {
                string str_taskfind = "[taskfinished]\n";
                for (int j = 1; j < int_account + 1; j++)
                {//加载帐户 
                    str_taskfind=str_taskfind+"account"+j.ToString()+"=\n";
                    str_taskfind = str_taskfind + "ip" + j.ToString() + "=\n";

                }

                System.IO.File.AppendAllText(str_fn, str_taskfind);
            
            }
            //启动得到当前IP的信息
            timer_Get_ip.Enabled = true;
           
        }

        private void timer_Get_ip_Tick(object sender, EventArgs e)
        {
            //这个函数是得到IP
            timer_Get_ip.Interval = 10000;//切换为10秒

            CLS_ControlIp cci = new CLS_ControlIp();
            string c_ip = cci.get_ip_by_www_whatismyip_com_tw();//得到当前IP
            if (c_ip.Length > 0)
            {
                txt_ip.Text = c_ip;
                timer_Get_ip.Enabled = false;//停止得到IP计时器
            }
            cci = null;
        }

     

        private void lv_task_Click(object sender, EventArgs e)
        {
            string str_us = lv_task.SelectedItems[0].SubItems[0].Text.Trim();//用户名
            txt_user.Text = str_us;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

          
        }

        private void frm_mainv_Load(object sender, EventArgs e)
        {

        }

        private void btn_trade_Click(object sender, EventArgs e)
        {//买入操作
            if (logined.Length == 0)
            {
                MessageBox.Show("登陆！");
            }
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION

            string pro_id = t_0.Text.Trim();
            string pro_price = t_1.Text.Trim();
            string pro_number = t_2.Text.Trim();
            string str_code_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=buy&id=" + pro_id;
            
            string str_html = task_huan168.trade_code("", useragent, str_code_url);//得到码
            Cls_FormatHTML xfh = new Cls_FormatHTML();
            string str_hidden = xfh.format_Trade_hidden(str_html);//得到——H——的值
       


            int int_code = task_huan168.task_trade_buy("", useragent, pro_id, pro_price, pro_number,str_hidden);


        }

        private void btn_trade_sell_Click(object sender, EventArgs e)
        {
            //SELL操作
            if (logined.Length == 0)
            {
                MessageBox.Show("登陆！");
            }
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION

            string pro_id = t_0.Text.Trim();
            string pro_price = t_1.Text.Trim();
            string pro_number = t_2.Text.Trim();
            string str_code_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=sell&id=" + pro_id;

            string str_html = task_huan168.trade_code("", useragent, str_code_url);//得到码
            Cls_FormatHTML xfh = new Cls_FormatHTML();
            string str_hidden = xfh.format_Trade_hidden(str_html);//得到——H——的值



            int int_code = task_huan168.task_trade_sell_v2("", useragent, pro_id, pro_price, pro_number, str_hidden);


        }

        private void btn_apply_forjob_Click(object sender, EventArgs e)
        {//申请入府
            if (logined.Length == 0)
            {
                MessageBox.Show("登陆！");
            }
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION

            string pro_id = t_0.Text.Trim();//府号
            int int_code = task_huan168.task_applyforjob("", useragent, pro_id);




        }





    }
}
