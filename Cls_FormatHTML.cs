using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HtmlAgilityPack;
namespace WebInfoCollect
{
    class Cls_FormatHTML
    {
        public string format_Trade_hidden(string html)
        {
            string string_task_hidden = "";
            try
            {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

              

                HtmlNodeCollection CNodes = doc.DocumentNode.SelectNodes("//input");

                foreach (HtmlNode item in CNodes)
                {

                    string b_class = item.GetAttributeValue("name", "");


                    if (b_class == "__H__")
                    {
                        string_task_hidden = item.GetAttributeValue("value", "");
                        break;

                    }


                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            return (string_task_hidden);

        }
        public string format_Task_URL(string html)
        {   string url_task = "";
            try
            {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);

                HtmlNodeCollection CNodes = doc.DocumentNode.SelectNodes("//li[@data-url]");
                
               foreach (HtmlNode item in CNodes)
               {
                   
                   string b_class=    item.GetAttributeValue("class","");
                  

                   if (b_class.Length>0)
                   {      url_task = url_task + item.Attributes["data-url"].Value.ToString();
                          
                    }

                   
               }


            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return (url_task);
        }

        public string format_TaskDetailPage_URL(string html)
        {
            string url_task = "";
            try
            {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNodeCollection CNodes = doc.DocumentNode.SelectNodes("//a[@target='_blank']");

                foreach (HtmlNode item in CNodes)
                {
                    string b_url= item.GetAttributeValue("href", "");
                    if (b_url.Length > 0)
                    {
                        return (b_url);

                    }
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return (url_task);
        }
        public string format_TaskDetail_URL(string html)
        {
            string url_task = "";
            try
            {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
               HtmlNode hn= doc.GetElementbyId("btnSubmit");
                url_task = hn.GetAttributeValue("data-url", "");
                url_task = hn.GetAttributeValue("data-id", "") + "@" + url_task;
                url_task = hn.GetAttributeValue("data-val", "") + "@" + url_task;
               if (url_task.Length > 0)
               {
                   return (url_task);
               }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return (url_task);
        }
        public string format_TaskDetailFormata_URL(string html)
        {
            string url_task = "";
            try
            {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                HtmlNode hn = doc.GetElementbyId("btnSubmit");
                url_task = hn.GetAttributeValue("data-url", "");
                if (url_task.Length > 0)
                {
                    return (url_task);
                }

            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }

            return (url_task);
        }
        public string format_route_html(string html)
        {

            try {
                //实例化HtmlAgilityPack.HtmlDocument对象
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);


                
            
            } 
               catch (Exception ex)
               {
                   Console.Write(ex.Message);
               }

           


            return ("");
        
        }
    }
}
