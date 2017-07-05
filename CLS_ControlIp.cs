using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using DotNet4.Utilities;


namespace WebInfoCollect
{
    class CLS_ControlIp
    {//这个类主要是换IP的
        #region 路由器控制部分 先掉线
        private bool route_adsl_changeip(string routetype="FAST",string ip="192.168.1.1",string user="admin",string pwd="3938390")
        {

            if (routetype == "fast")
            {

                try
                {
                    HttpHelper http = new HttpHelper();
                    string cookie = get_cookie(user, pwd);
                    string url = "http://" + ip + "/userRpm/PPPoECfgRpm.htm?wan=0&wantype=2&acc=55267896369&psw=Hello123World&confirm=Hello123World&specialDial=100&SecType=0&sta_ip=0.0.0.0&sta_mask=0.0.0.0&linktype=1&waittime=15&Disconnect=%B6%CF+%CF%DF";
                    string Referers = "http://" + ip + "/userRpm/PPPoECfgRpm.htm";
                    HttpItem item = new HttpItem()
                    {
                        URL = url,//URL     必需项
                        Method = "GET",//URL     可选项 默认为Get
                        //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                        Timeout = 100000,//连接超时时间     可选项默认为100000
                        ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                        IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                        Cookie = cookie,//字符串Cookie     可选项
                        UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值

                        Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                        //ContentType = "text/html",//返回类型    可选项有默认值
                        ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                        Referer = Referers,//来源URL     可选项
                        Allowautoredirect = false,//是否根据３０１跳转     可选项

                        //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                        //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                        //Postdata = "",//Post要发送的数据
                        //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                        //ProxyPwd = "123456",//代理服务器密码     可选项
                        //ProxyUserName = "administrator",//代理服务器账户名     可选项
                        //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                    };
                    // item.Header.Add("Authorization", "Basic " + base64str);
                    HttpResult result = http.GetHtml(item);
                    string html = result.Html;

                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }

            }



            return (true);
        }
        private string get_cookie(string user = "admin", string passwd = "admin")
        {
            string scookie = "";
            byte[] bytes = Encoding.Default.GetBytes(user + ":" + passwd);
            string base64str = Convert.ToBase64String(bytes);
            string auth = "Basic " + base64str;
            scookie = "Authorization=" + auth + ";path=/";
            return (scookie);
        }
       public string checkstatus(string nettype = "routeadsl", string routetype = "FAST", string ip = "192.168.1.1", string user = "admin", string pwd = "")
        {
            string html = "";
           
            if (nettype == "routeadsl")
            {//通过路ROUTE控制adsl
                html = checkadslrouteststus(routetype, ip, user, pwd);

            }

      

            return (html);
        }

     private string checkadslrouteststus(string routetype="FAST",string ip="192.168.1.1",string user="admin",string pwd="3938390")
     {//route control adsl 控制ADSL

           string html = "";
           HttpHelper http = new HttpHelper();
           HttpItem item;
           if (routetype == "fast")
           {
               try
               {

                   string cookie = get_cookie(user, pwd);
                  // string url = "http://" + ip + "/userRpm/StatusRpm.htm";
                   //string Referers = "http://" + ip + "/userRpm/PPPoECfgRpm.htm";
                   string url = "http://" + ip + "/userRpm/PPPoECfgRpm.htm?wan=0&wantype=2&acc=55267896369&psw=Hello123World&confirm=Hello123World&specialDial=100&SecType=0&sta_ip=0.0.0.0&sta_mask=0.0.0.0&linktype=1&waittime=15&Disconnect=%B6%CF+%CF%DF";

                   string Referers = "http://" + ip + "/userRpm/PPPoECfgRpm.htm";
              


                   item = new HttpItem()
                   {
                       URL = url,//URL     必需项
                       Method = "GET",//URL     可选项 默认为Get
                       //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                       Timeout = 100000,//连接超时时间     可选项默认为100000
                       ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                       IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                       Cookie = cookie,//字符串Cookie     可选项
                       UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)",//用户的浏览器类型，版本，操作系统     可选项有默认值

                       Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                       //ContentType = "text/html",//返回类型    可选项有默认值
                       ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                       Referer = Referers,//来源URL     可选项
                       Allowautoredirect = false,//是否根据３０１跳转     可选项

                       //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                       //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

                       //Postdata = "",//Post要发送的数据
                       //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                       //ProxyPwd = "123456",//代理服务器密码     可选项
                       //ProxyUserName = "administrator",//代理服务器账户名     可选项
                       //ResultType = ResultType.Byte,//返回数据类型，是Byte还是String
                   };
                   // item.Header.Add("Authorization", "Basic " + base64str);
                   HttpResult result = http.GetHtml(item);
                   html = result.Html;


               }
               catch (Exception ex)
               {
                   Console.Write(ex.Message);
               }
           }
           return (html);
       
       }


        #endregion
        public bool change_ip(string nettype = "routeadsl", string routetype = "FAST", string ip = "192.168.1.1", string user = "admin", string pwd = "")
        {
            if (nettype == "routeadsl")
            {//通过路ROUTE控制adsl
                route_adsl_changeip(routetype,ip,user,pwd);
            
            }
            return (true);
        }

        #region 通过whatismyip来得IP
        private string format_ip_by_www_whatismyip_com_tw(string html)
        {//通过http://www.whatismyip.com.tw/得到IP
            if (html.Length < 200)
            {
                return ("");
            }
            html = html.Substring(html.IndexOf("h2"), 30);
            html = html.Substring(html.IndexOf("h2") + 3, 20);
            html = html.Substring(0, html.IndexOf("<"));

            return (html);
        }

        public string get_ip_by_www_whatismyip_com_tw()
        {
            string html = "";
            string ip = "";

            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "http://www.whatismyip.com.tw/",//URL     必需项
                Method = "Get",//URL     可选项 默认为Get
                //Encoding = "System.Text.UTF8Encoding",//编码格式（utf-8,gb2312,gbk）     可选项 默认类会自动识别
                Timeout = 100000,//连接超时时间     可选项默认为100000
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写
                Cookie = ";",//字符串Cookie     可选项
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值
                //ContentType = "text/html",//返回类型    可选项有默认值
                ContentType = "application/x-www-form-urlencoded; charset=UTF-8",

                Referer = "http://www.qq.com",//来源URL     可选项
                Allowautoredirect = false,//是否根据３０１跳转     可选项

                //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数
                //Connectionlimit = 1024,//最大连接数     可选项 默认为1024

               // Postdata = "a=123&c=456&d=789",//Post要发送的数据
                //ProxyIp = proxyip,//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数
                //ProxyPwd = "123456",//代理服务器密码     可选项
                //ProxyUserName = "administrator",//代理服务器账户名     可选项
                ResultType = ResultType.String,//返回数据类型，是Byte还是String
            };
            try
            {
                HttpResult result = http.GetHtml(item);
                html = result.Html;
                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //解析IP
                    ip = format_ip_by_www_whatismyip_com_tw(html);

                }
              

            }
            catch (Exception ex)
            {
               
            }
            return (ip);


        }
        #endregion
    }
}
