using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotNet4.Utilities;
using System.IO;
using System.Web;
namespace WebInfoCollect
{
    class Cls_Task_Huan168
    {
        public string logined_cookied = "";

        #region ToUrlEncode
        public static string ToUrlEncode(string strCode)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str)
            System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
            for (int i = 0; i < byStr.Length; i++)
            {
                string strBy = Convert.ToChar(byStr[i]).ToString();
                if (regKey.IsMatch(strBy))
                {
                    //是字母或者数字则不进行转换  
                    sb.Append(strBy);
                }
                else
                {
                    sb.Append(@"%" + Convert.ToString(byStr[i], 16));
                }
            }
            return (sb.ToString());
        }
        #endregion
        public int task_info_realname(string str_ProxyIp, string useragent, string nickname, string idcards, string homeaddresss)
        {//第二步,得到任务URL,做任务
            string strdomain = "http://www.huan168.com";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result;
            string html;
            //实名认证
            string old_url = "http://www.huan168.com/index.php?m=Home&c=Member&a=certification";

            string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Member&a=smsc";
            string realname = ToUrlEncode(nickname);
            string sex = "1";
            string credentialstype = "1";
            string idcard = ToUrlEncode(idcards);
            string homeaddress = ToUrlEncode(homeaddresss);

            item = new HttpItem()
            {
                URL = str_form_step1,//URL     必需项
                Method = "Post",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "credentialstype=" + credentialstype + "&realname=" + realname + "&sex=" + sex + "&idcard=" + idcard + "&homeaddress=" + homeaddress,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };
            item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。


            result = http.GetHtml(item);
            html = result.Html;
                return (0);
        }
        public int task_info_modify(string str_ProxyIp, string useragent, string nickname, string qq)
        {//第二步,得到任务URL,做任务


            string strdomain = "http://www.huan168.com";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result;
            string html;
            string old_url = "http://www.huan168.com/index.php?m=Home&c=Index&a=index";

            string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Member&a=index" ;
            nickname =ToUrlEncode( nickname);
            string job = ToUrlEncode("服装鞋帽");
            item = new HttpItem()
            {
                URL = str_form_step1,//URL     必需项
                Method = "Post",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "nickname=" + nickname + "&qq=" + qq + "&email=" + qq + "@qq.com&job="+job,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };
            item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。
          
      
            result = http.GetHtml(item);
            html = result.Html;
            //MessageBox.Show(html);
            //===================
            
            return (0);

        }


        public int task_trade_sell(string str_ProxyIp, string useragent, string product_id, string product_price, string amount = "1")
        {//第二步,得到任务URL,做任务


            string strdomain = "http://www.huan168.com";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result;
            string html;
            string old_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=trade";

            string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Token&a=sell&id=" + product_id;
            item = new HttpItem()
            {
                URL = str_form_step1,//URL     必需项
                Method = "Get",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                //Postdata = "id=" + form_id + "&verify=" + verify_code,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };

            result = http.GetHtml(item);
            html = result.Html;

            Cls_FormatHTML xfh = new Cls_FormatHTML();
            string str_hidden = xfh.format_Trade_hidden(html);//得到——H——的值
            //==================================

            string form_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=sell";

            item = new HttpItem()
            {
                URL = form_url,//URL     必需项
                Method = "Post",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "school_id=" + product_id + "&amount=" + amount + "&__H__=" + str_hidden + "&price=" + product_price,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };
            item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。
            result = http.GetHtml(item);
            html = result.Html;
            MessageBox.Show(html);



            return (0);

        }
        public string trade_code(string str_ProxyIp, string useragent, string url="")
        { //得到交易码
            HttpHelper http = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项
                Method = "Get",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

               // Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                //Postdata = "id=" + form_id + "&verify=" + verify_code,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };

            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            return (html);

            
            
        }
        public int task_trade_buy(string str_ProxyIp, string useragent, string product_id, string product_price, string amount="1",string strhidden="")
        {//交易买入
            
          
                string strdomain = "http://www.huan168.com";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem();
                HttpResult result;
                string html;
                string old_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=trade";

                string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Token&a=buy";
                item = new HttpItem()
                {
                    URL = str_form_step1,//URL     必需项
                    Method = "Post",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = old_url,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                    Postdata = "__H__=" + strhidden.Trim()+ "&amount=" + amount + "&price=" + product_price + "&school_id=" + product_id,//Post要发送的数据a=123&c=456&d=789verify:verify
                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。
              
                result = http.GetHtml(item);
                html = result.Html;
                if (html.IndexOf(":0") > 0)
                {
                    MessageBox.Show("交易失败"+html);
                }
                

               
            return (0);

        }
        public int task_trade_sell_v2(string str_ProxyIp, string useragent, string product_id, string product_price, string amount = "1", string strhidden = "")
        {//交易买入


            string strdomain = "http://www.huan168.com";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result;
            string html;
            string old_url = "http://www.huan168.com/index.php?m=Home&c=Token&a=trade";

            string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Token&a=sell";
            item = new HttpItem()
            {
                URL = str_form_step1,//URL     必需项
                Method = "Post",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "__H__=" + strhidden.Trim() + "&amount=" + amount + "&price=" + product_price + "&school_id=" + product_id,//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };
            item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

            result = http.GetHtml(item);
            html = result.Html;
            if (html.IndexOf(":0") > 0)
            {
                MessageBox.Show("交易失败" + html);
            }



            return (0);

        }
        public int task_applyforjob(string str_ProxyIp, string useragent, string jobid)
        {//入职

            Random rdm = new Random();
            double randomDouble = rdm.NextDouble();
            string strdomain = "http://www.huan168.com";
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem();
            HttpResult result;
            string html;
            string old_url = "http://www.huan168.com/index.php?m=Home&c=Jianghu&a=zhaopin&p=3";

            string str_form_step1 = "http://www.huan168.com/index.php?m=Home&c=Jianghu&a=zhaopin_show";
            item = new HttpItem()
            {
                URL = str_form_step1,//URL     必需项
                Method = "Post",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = logined_cookied,//字符串Cookie     可选项
                // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = old_url,//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                Postdata = "id=" + jobid.Trim() + "&t=" + randomDouble.ToString(),//Post要发送的数据a=123&c=456&d=789verify:verify
                //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
            };
            item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

            result = http.GetHtml(item);
            html = result.Html;
            if (html.IndexOf(":0") > 0)
            {
                MessageBox.Show("交易失败" + html);
            }



            return (0);

        }
    
        public int task_login(string str_ProxyIp, string userid, string pwd, string useragent)
        { //第一步,登陆
            try
            {
                string strdomain = "http://www.huan168.com";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = "http://www.huan168.com/index.php?s=/Home/User/login.html",//URL     必需项
                    Method = "Post",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "",//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = strdomain,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    Postdata = "username=" + userid + "&password=" + pwd,//Post要发送的数据a=123&c=456&d=789

                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                HttpResult result = http.GetHtml(item);
                string html = result.Html;
                //MessageBox.Show(result.Cookie);
                logined_cookied = result.Cookie;
                if (logined_cookied == string.Empty)
                {//退出不执行相关操作
                    return (666);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return (0);
            }
            return (0);
        
        }

        public int task_step_one(string str_ProxyIp, string userid, string pwd,string useragent)
        {//第一步,登陆,第二步,签到,第三步,得验证码
            try
            {
                string strdomain = "http://www.huan168.com";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = "http://www.huan168.com/index.php?s=/Home/User/login.html",//URL     必需项
                    Method = "Post",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = "",//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = strdomain,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    Postdata = "username=" + userid + "&password=" + pwd,//Post要发送的数据a=123&c=456&d=789

                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                HttpResult result = http.GetHtml(item);
                string html = result.Html;
                //MessageBox.Show(result.Cookie);
                logined_cookied = result.Cookie;
                if (logined_cookied == string.Empty)
                {//退出不执行相关操作
                    return (666);
                 }
                //签到
                Random rdm = new Random();
                double randomDouble = rdm.NextDouble();
                item = new HttpItem()
                {
                    URL = "http://www.huan168.com/index.php?s=/Home/Index/qiandao.html",//URL     必需项
                    Method = "Post",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = "http://www.huan168.com",//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    Postdata = "t=" + randomDouble.ToString(),//Post要发送的数据a=123&c=456&d=789

                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                result = http.GetHtml(item);
                
                //=========显示CODE
                show_image(pic);

            
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return (0);
            }
            return(0);
        }
        public int task_step_two(string str_ProxyIp, string useragent, string verify_code, int int_task_bg_pause_min = 10, int int_task_bg_pause_max=30)
        {//第二步,得到任务URL,做任务
            Random ran = new Random();
            int RandKey = ran.Next(int_task_bg_pause_min, int_task_bg_pause_max);
            
            try
            {
                string strdomain = "http://www.huan168.com";
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem();
                HttpResult result;
                string html;
                string vurl = "http://www.huan168.com/index.php?s=/Home/Task/index.html";
                item = new HttpItem()
                {
                    URL = vurl,//URL     必需项
                    Method = "Get",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
               
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = strdomain,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    //Postdata = "",//Post要发送的数据a=123&c=456&d=789

                    // ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                //item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                result = http.GetHtml(item);
                html = result.Html;

                Cls_FormatHTML xfh = new Cls_FormatHTML();
                string ad_url = xfh.format_Task_URL(html);
                if (ad_url.Length == 0)
                { 
                    return(777);
                }
                string url = "";
                if (ad_url.IndexOf(strdomain) > 0)
                {
                    url = ad_url;
                }
                else
                {
                    url = strdomain + ad_url;//数据提交
                }

                //下面解析广告页
                item = new HttpItem()
                {
                    URL = url,//URL     必需项
                    Method = "Get",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
               
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = vurl,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    Postdata = "",//Post要发送的数据a=123&c=456&d=789

                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                //item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                result = http.GetHtml(item);
                html = result.Html;
                //再次得到具体的调查页地址

                string ad_page_url = xfh.format_TaskDetailPage_URL(html);
                if (ad_page_url.IndexOf(strdomain) > 0)
                {
                    
                }
                else
                {
                    ad_page_url = strdomain + ad_page_url;//数据提交
                }
                item = new HttpItem()
                {
                    URL = ad_page_url,//URL     必需项
                    Method = "Get",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值

                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = url,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                    Postdata = "",//Post要发送的数据a=123&c=456&d=789

                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                //item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                result = http.GetHtml(item);
                html = result.Html;
                //下面是提交数据
                string form_ac_url = xfh.format_TaskDetail_URL(html);
                string[] strs = form_ac_url.Split('@');
                string form_value = strs[0];
                string form_id = strs[1];
                string form_url = strs[2];

                if (form_url.IndexOf(strdomain) > 0)
                {
                    
                }
                else
                {
                    form_url = strdomain + form_url;//数据提交
                }

                delayTime(RandKey);//任务停止时间

                //===================================
                item = new HttpItem()
                {
                    URL = form_url,//URL     必需项
                    Method = "Post",//URL     可选项 默认为Get
                    //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                    Timeout = 100000,//连接超时时间     可选项默认为100000
                    ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                    IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    // UserAgent = "Mozilla/5.0 (Linux; U; Android 2.3.6; zh-cn; GT-S5660 Build/GINGERBREAD) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1 MicroMessenger/4.5.255",//用户的浏览器类型，版本，操作系统     可选项有默认值
                    UserAgent = useragent,//用户的浏览器类型，版本，操作系统     可选项有默认值
               
                    Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                    //ContentType = "text/html",//返回类型    可选项有默认值
                    ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                    Referer = url,//来源URL     可选项
                    Allowautoredirect = false,//是否根据３０１跳转     可选项

                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024
                    Postdata = "id=" + form_id + "&verify=" + verify_code,//Post要发送的数据a=123&c=456&d=789verify:verify
                    //ProxyIp = str_ProxyIp,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                    // ProxyPwd = str_Proxy_password,//代理服务器密码     可选项
                    // ProxyUserName = str_Proxy_user,//代理服务器账户名     可选项
                    ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                };
                item.Header.Add("x-requested-with", "XMLHttpRequest");  //主要就是这一句，只接受ajax请求。

                result = http.GetHtml(item);
                html = result.Html;
                //"{\"info\":\"\\u4efb\\u52a1\\u5956\\u52b1 268 \\u94f6\\u5b50\\uff0c\\u6263\\u9664
                //\\u7a0e\\u6536\\uff1a24\\u94f6\\u5b50\\uff0c\\u5b9e\\u9645\\u6536\\u5165\\uff1a244\\u94f6
                //\\u5b50\\u3002\\u7531\\u4e8e\\u60a8\\u76ee\\u524d  \\u57fa\\u672c\\u7a0e\\u6536\\uff089%
                //\\uff09\\u76841\\u500d\\uff0c\\u53739%\\uff0c\\u4e3a\\u4e86\\u63d0\\u9ad8\\u60a8\\u7684\\
                //u4efb\\u52a1\\u6536\\u76ca\\uff0c\\u8bf7\\u5c3d\\u5feb\\u5b8c\\u6210\\u4ee5\\u4e0a\\u4efb\\u52a1
                //\\u3002\",\"status\":1,\"url\":\"\\/index.php?m=Home&c=Task&a=index\"}"

                string[] arr_result = html.Split(',');
                string str_taskstatu1="";
                if (arr_result.Length > 1)
                { 
                     string str_status = arr_result[1];
                    string[] arr_status = str_status.Split(':');
                    str_taskstatu1 = arr_status[1];
                }

                
                if ( str_taskstatu1== "1")
                {//任务完成 
                    return (8888);
                }
                else
                {//验证码
                    return (9999);
                }

            
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return (1);
            }

            return (0);
        }



        public System.Windows.Forms.PictureBox pic;
        public void showCode(System.Windows.Forms.PictureBox pic, System.Windows.Forms.PictureBox pic1)
        {
                 pic.Visible = true;
                 pic1.Visible = false;
            
         }
        public void hiddenCode(System.Windows.Forms.PictureBox pic, System.Windows.Forms.PictureBox pic1)
        {

            pic.Visible =false;
            pic1.Visible = true;

        }
        public void show_image(System.Windows.Forms.PictureBox pic, string url = "http://www.huan168.com/index.php?s=/Home/Task/verify.html")
        {
            try
            {
                HttpHelper http = new HttpHelper();
                HttpItem item = new HttpItem()
                {
                    URL = url,
                    Method = "get",//URL     可选项 默认为Get  
                    Cookie = logined_cookied,//字符串Cookie     可选项
                    ResultType = ResultType.Byte
                };
                HttpResult result = http.GetHtml(item);
                Image img = byteArrayToImage(result.ResultByte);
                pic.Image = img;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            

        }

        /// <summary>
        /// 字节数组生成图片
        /// </summary>
        /// <param name="Bytes">字节数组</param>
        /// <returns>图片</returns>
        private Image byteArrayToImage(byte[] Bytes)
        {
            MemoryStream ms = new MemoryStream(Bytes);
            return Bitmap.FromStream(ms, true);
        }

        private void delayTime(double secend)
        {
            DateTime tempTime = DateTime.Now;
            while (tempTime.AddSeconds(secend).CompareTo(DateTime.Now) > 0)
                Application.DoEvents();
        }


    }
}
