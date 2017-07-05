using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebInfoCollect
{
    class Cls_Task
    {
        public void add_task1(string username, string pwd, string t1, string t2, string t3, System.Windows.Forms.ListView sender)
        {//任务表
            ListViewItem lvi;


            lvi = new ListViewItem();
            lvi.Text = username;
            lvi.SubItems.Add(pwd);
            lvi.SubItems.Add(t1);
            lvi.SubItems.Add(t2);
            lvi.SubItems.Add(t3);
            sender.Items.Add(lvi);
        }
        public void add_task(string username, string pwd, System.Windows.Forms.ListView sender)
        {//任务表
            ListViewItem lvi;


            lvi = new ListViewItem();
            lvi.Text = username;
            lvi.SubItems.Add(pwd);

            sender.Items.Add(lvi);
        }
        public void add_finishedtask(string username, System.Windows.Forms.ListView sender,string ip="")
        {//任务完成
            if (username.Trim().Length > 0)
            {
                ListViewItem lvi = new ListViewItem();
                lvi.Text = username;
                lvi.SubItems.Add(ip);
                sender.Items.Add(lvi);
               
            }
        }
      

    }
}
