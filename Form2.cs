using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppUtility;

using DotNet4.Utilities;
using System.IO;
namespace WebInfoCollect
{
    public partial class frm_app_config : Form
    {
        public frm_app_config()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
        private void frm_app_config_Load(object sender, EventArgs e)
        {
            string ph = System.IO.Directory.GetCurrentDirectory();
            IniHelper ini;
            string str_app_run = ph + "\\app.ini";//程序运行
            string str_file = "";
            if (System.IO.File.Exists(str_app_run))
            {

                ini = new IniHelper(str_app_run);
                str_file= ini.ReadValue("app", "configfilepath");
                load_config(str_file);
            }



            
        }
        private void load_config(string fp = "c:\\config.ini")
        {
            IniHelper ini = new IniHelper(fp);
            cb_useragent.Text= ini.ReadValue("browser", "useragent");
            if(ini.ReadValue("net", "type")=="adsl")
            {
                txt_1_1.Text = ini.ReadValue("net", "user");
                txt_1_2.Text = ini.ReadValue("net", "pwd");

            }
            if (ini.ReadValue("net", "type") == "routeadsl")
            {
                txt_2_1.Text = ini.ReadValue("net", "user");
                txt_2_2.Text = ini.ReadValue("net", "pwd");
                txt_2_3.Text = ini.ReadValue("net", "ip");
                txt_2_4.Text = ini.ReadValue("net", "routetype");

            }
            if (ini.ReadValue("net", "type") == "vpn")
            {
                txt_3_1.Text = ini.ReadValue("net", "user");
                txt_3_2.Text = ini.ReadValue("net", "pwd");
                txt_3_3.Text = ini.ReadValue("net", "ip");
               
            }
            if (ini.ReadValue("net", "type") == "agent")
            {
                txt_4_1.Text = ini.ReadValue("net", "user");
                txt_4_2.Text = ini.ReadValue("net", "pwd");
                txt_4_3.Text = ini.ReadValue("net", "ip");

            }


        }
        private void btn_config_save_Click(object sender, EventArgs e)
        {
             string ph = System.IO.Directory.GetCurrentDirectory();
            IniHelper ini;
            string str_app_run = ph + "\\app.ini";//程序运行
            if (System.IO.File.Exists(str_app_run))
            {

                ini = new IniHelper(str_app_run);
                ini.WriteValue("app", "configfilepath", txt_filepath.Text.Trim());
                ini.WriteValue("app", "int_task_bg_pause_min", txt_min.Text.Trim());
                ini.WriteValue("app", "int_task_bg_pause_max", txt_max.Text.Trim());


            }
            if (System.IO.File.Exists(txt_filepath.Text.Trim()))
            {//文件配置目录 
                ini = new IniHelper(txt_filepath.Text.Trim());
                if (radioButton1.Checked)
                { //adsl
                    ini.WriteValue("net", "type", "adsl");
                    ini.WriteValue("net", "user", txt_1_1.Text.Trim());
                    ini.WriteValue("net", "pwd", txt_1_2.Text.Trim());

                }
                if (radioButton2.Checked)
                {//route
                    ini.WriteValue("net", "type", "routeadsl");

                    ini.WriteValue("net", "user", txt_2_1.Text.Trim());
                    ini.WriteValue("net", "pwd", txt_2_2.Text.Trim());
                    ini.WriteValue("net", "ip", txt_2_3.Text.Trim());
                    ini.WriteValue("net", "routetype", txt_2_4.Text.Trim());
                }
                if (radioButton3.Checked)
                {//vpn
                    ini.WriteValue("net", "type", "vpn");
                    ini.WriteValue("net", "user", txt_3_1.Text.Trim());
                    ini.WriteValue("net", "pwd", txt_3_2.Text.Trim());
                    ini.WriteValue("net", "ip", txt_3_3.Text.Trim());
                }
                if (radioButton4.Checked)
                {//agent
                    ini.WriteValue("net", "type", "agent");
                    ini.WriteValue("net", "user", txt_4_1.Text.Trim());
                    ini.WriteValue("net", "pwd", txt_4_2.Text.Trim());
                    ini.WriteValue("net", "ip", txt_4_3.Text.Trim());
                }
                ini.WriteValue("browser", "useragent", cb_useragent.Text.Trim());




            }
         
           
            //下面是保存
      

          


            MessageBox.Show("保存成功!");
        }

        
    }
}
