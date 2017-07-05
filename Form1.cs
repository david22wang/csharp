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
    public partial class Form1 : Form
    {

        #region 系统初始化
        public Form1()
        {
            InitializeComponent();
        }
        private string useragent = "";
        int int_account = 0;
        int int_task_bg_pause_min = 10;//任务暂停起始时间
        int int_task_bg_pause_max = 30;//任务暂停起始时间

        private void Form1_Shown(object sender, EventArgs e)
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
                str_app_file = str_app_file + "configfilepath=c:\\config.ini\n";
                str_app_file = str_app_file + "int_task_bg_pause_min=2\n";
                str_app_file = str_app_file + "int_task_bg_pause_max=5\n";

                System.IO.File.AppendAllText(str_app_run,str_app_file);//保存
            
            }

            if (f1.Length == 0)
            {//如果f1为空，DEFAULT为C盘
                f1 = "c:\\config.ini";
            }
          
          
            string pa1, pa2,  pa3;
            
            Cls_Task obj_task = new Cls_Task();
            DateTime now = DateTime.Now;
            

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

                for (int j = 1; j < int_account + 1; j++)
                {//加载帐户 
                    pa2 = ini.ReadValue("account" + j.ToString(), "user");
                    pa3 = ini.ReadValue("account" + j.ToString(), "pwd");
                    obj_task.add_task(pa2, pa3, lv_task);
                }

            }
            else
            {
                MessageBox.Show("系统找不到配置文件,把配置文件放到C盘根目录下");
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
        #endregion 


        #region IP换
        private string net_type = "";
        private string net_route_type = "";
        private string net_route_ip = "";
        private string net_route_user = "";
        private string net_route_pwd = "";

        private void btn_step1_Click(object sender, EventArgs e)
        {
            txt_ip.Text = "";
            txt_ip.ForeColor = Color.Black;

            CLS_ControlIp obj_ip = new CLS_ControlIp();
            bool blchange=obj_ip.change_ip(net_type, net_route_ip, net_route_user, net_route_pwd);//如果成功的话,激活
            if (blchange)
            {
                timer_check_route_status.Enabled = true;//启动ROUTE状态检查
            }
            
        }
        private void timer_check_route_status_Tick(object sender, EventArgs e)
        {//ROUTE状态检查
            CLS_ControlIp obj_ip = new CLS_ControlIp();
            string html = obj_ip.checkstatus(net_type, net_route_type, net_route_ip, net_route_user, net_route_pwd);//如果成功的话,激活
            if (html == "无法连接到远程服务")
            {
                DateTime now = DateTime.Now;


                txt_log.Text = "正在切换IP，请等待。。。" + now.ToString("yyyy年MM月dd HH时mm分ss秒");
            }
            else
            {
                txt_log.Text = html;

                Cls_FormatHTML cfh = new Cls_FormatHTML();

                string str_ip = cfh.format_route_html(html);

                timer_Get_ip.Enabled = true;//起动得到IP计时器

                timer_check_route_status.Enabled = false;
            }

        }
        private void timer_Get_ip_Tick(object sender, EventArgs e)
        {//这个函数是得到IP
            timer_Get_ip.Interval = 10000;//切换为10秒

            CLS_ControlIp cci = new CLS_ControlIp();
            string c_ip=cci.get_ip_by_www_whatismyip_com_tw();//得到当前IP
            if (c_ip.Length > 0)
            {
                txt_ip.Text = c_ip;
                timer_Get_ip.Enabled = false;//停止得到IP计时器
            }
             cci = null;

        }
        #endregion

       

        #region 任务
        private string logined = "";
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
        private void btn_step2_Click(object sender, EventArgs e)
        {
            txt_code.Text = "";
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
            task_huan168.pic = pic;
           int str_1= task_huan168.task_step_one("",str_us, str_pw,useragent);
           showMessage(str_1);
           task_huan168.showCode(pic, pic2);
            logined=task_huan168.logined_cookied;
            txt_code.Focus();

        }
        private void btn_reloadimage_Click(object sender, EventArgs e)
        {
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            if(logined.Length>0)
            {
                task_huan168.logined_cookied=logined;
                task_huan168.hiddenCode(pic, pic2);
                task_huan168.show_image(pic);
                task_huan168.showCode(pic, pic2);
            }
            
        }
        private void showMessage(int errcode)
        {//出错
            string tip_8888 = "任务完成";
            string tip_9999 = "验证码出错";
            string tip_777 = "没有新任务";
            string tip_666 = "登陆出错";
            if (errcode == 777)
            {
              

                MessageBox.Show(tip_777);
            }
            if (errcode == 666)
            {
                MessageBox.Show(tip_666);
            }
            if (errcode == 8888)
            {
               
                MessageBox.Show(tip_8888);//提示任务完成 
                Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
                task_huan168.hiddenCode(pic, pic2);//关闭验证码
           
                

            }
            if (errcode == 9999)
            {
                MessageBox.Show(tip_9999);
            }
        
        }
        private void btn_step3_Click(object sender, EventArgs e)
        {   
            if (txt_code.TextLength > 0)
            {
                Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
                task_huan168.logined_cookied = logined;//增加LOGIN SESSION
                int int_code = task_huan168.task_step_two("", useragent, txt_code.Text.Trim(), int_task_bg_pause_min, int_task_bg_pause_max);//提交任务
                if (int_code == 8888)
                { //任务完成-更新
                    Cls_Task obj_task = new Cls_Task();
                    obj_task.add_finishedtask(txt_user.Text.Trim(), lv_task_finished,txt_ip.Text);
                    
                    //保存到配置文件中
                    string ph = System.IO.Directory.GetCurrentDirectory();
                    DateTime now = DateTime.Now;
                    string str_fn = ph + "\\" + now.ToString("yyMMdd") + ".ini";//完成任务
                    IniHelper ini;
                    ini = new IniHelper(str_fn);
                    
                    for (int j = 1; j < int_account + 1; j++)
                    {//加载帐户 
                       string pa2 = ini.ReadValue("taskfinished", "account" + j.ToString());
                       if (pa2.Length == 0)
                       {
                           ini.WriteValue("taskfinished", "account" + j.ToString(), txt_user.Text.Trim());
                           ini.WriteValue("taskfinished", "ip" + j.ToString(), txt_ip.Text.Trim());

                           break;
                       }
                        
                    }
                    //=================
                    txt_user.Text = "";//清空


                   //


                }
                showMessage(int_code);
                
                
            }
        }
        #endregion
        #region 界面
        private void lv_task_Click(object sender, EventArgs e)
        {
            string str_us = lv_task.SelectedItems[0].SubItems[0].Text.Trim();//用户名
            txt_user.Text = str_us;
            
        }
        #endregion
        private void button1_Click(object sender, EventArgs e)
        {

            string s = "{\"info\":\"\\u4efb\\u52a1\\u5956\\u52b1 268 \\u94f6\\u5b50\\uff0c\\u6263\\u9664\\u7a0e\\u6536\\uff1a24\\u94f6\\u5b50\\uff0c\\u5b9e\\u9645\\u6536\\u5165\\uff1a244\\u94f6\\u5b50\\u3002\\u7531\\u4e8e\\u60a8\\u76ee\\u524d  \\u57fa\\u672c\\u7a0e\\u6536\\uff089%\\uff09\\u76841\\u500d\\uff0c\\u53739%\\uff0c\\u4e3a\\u4e86\\u63d0\\u9ad8\\u60a8\\u7684\\u4efb\\u52a1\\u6536\\u76ca\\uff0c\\u8bf7\\u5c3d\\u5feb\\u5b8c\\u6210\\u4ee5\\u4e0a\\u4efb\\u52a1\\u3002\",\"status\":1,\"url\":\"\\/index.php?m=Home&c=Task&a=index\"}";
            string[] strs = s.Split(',');
            string str_status = strs[1];
            string[] arr_status = str_status.Split(':');
            if (arr_status[1] == "1")
            {
                MessageBox.Show(arr_status[1]);
            }
            else
            { 
            
            }

            

            

        }

        private void btn_config_save_Click(object sender, EventArgs e)
        {
            frm_app_config frm = new frm_app_config();
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("iexplore.exe", "http://www.paitangtang.com");
        }

        private void btn_pending_Click(object sender, EventArgs e)
        {
            }

        private void button2_Click(object sender, EventArgs e)
        {
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION
            int int_code = task_huan168.task_trade_sell("", useragent, txt_trading_pro.Text.Trim(), txt_trade_price.Text.Trim(), txt_amount.Text.Trim());//提交任务
       
        }

        private void btn_changepersonINFO_Click(object sender, EventArgs e)
        {
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION
            string nickname = "smihthwang";
            string n1 = "smith";
       
            



            Random rdm = new Random();
            double randomDouble = rdm.NextDouble();
            string qql = randomDouble.ToString().Replace("0","").Replace(".","").Substring(0,9) ;
            nickname = "qq" + qql;
            int int_code = task_huan168.task_info_modify("", useragent, nickname, qql);//提交任务
        }

        private void btn_save_id_Click(object sender, EventArgs e)
        {
            Cls_Task_Huan168 task_huan168 = new Cls_Task_Huan168();
            task_huan168.logined_cookied = logined;//增加LOGIN SESSION

            string nickname =t_0.Text.Trim();
            string idcards =t_1.Text.Trim();
            string homeaddresss = t_2.Text.Trim();

            int int_code = task_huan168.task_info_realname("", useragent, nickname, idcards, homeaddresss);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        

       

       

      



    }
}
