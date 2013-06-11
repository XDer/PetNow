using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;

namespace Pet_Now
{
    public class NewsController : INotifyPropertyChanged
    {
        private List<NewsItem> news;
        public List<NewsItem> News
        {
            get { return news; }
            set
            {
                if (news != value)
                {
                    news = value;
                    NotifyPropertyChanged("News");
                }
            }
        }

        public NewsController()
        {
            Loaded = false;
            news = new List<NewsItem>();
            for (int i = 0; i < 8; i++)
            {
                News.Add(new NewsItem());
            }
        }

        public bool Loaded { get; set; }

        public void LoadData()
        {
            if (!Loaded)
            {
                Reqest();
                Loaded = true;
            }
        }

        private string Reqest()
        {
            string resultString = string.Empty;
            HttpWebRequest request = HttpWebRequest.CreateHttp("http://www.ttpet.com");
            request.Method = "GET";
            request.BeginGetResponse((IAsyncResult result) =>
            {
                StreamReader reader = null;
                try
                {
                    HttpWebRequest webRequest = result.AsyncState as HttpWebRequest;
                    HttpWebResponse webResponse = (HttpWebResponse)webRequest.EndGetResponse(result);
                    Stream streamResult = webResponse.GetResponseStream();
                    reader = new StreamReader(streamResult);
                }
                catch (WebException e)
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        MessageBox.Show(e.Message);
                    });
                    Loaded = false;
                }
                //获取的返回值
                if (reader != null)
                {
                    resultString = reader.ReadToEnd();
                    if (resultString != null)
                    {
                        Deployment.Current.Dispatcher.BeginInvoke(() => { getelement(resultString); });
                    }
                }
            }, request);
            return resultString;
        }

        public void getelement(String s)
        {
            int index = s.IndexOf("<ul class=\"top_li\">");
            s = s.Substring(index);
            int index1 = s.IndexOf("</ul>");
            string s2 = s.Substring(0, index1);
            detailget(s2);
        }

        public void detailget(String s)
        {
            int i = 0;
            while (i < 8)
            {
                string s1 = "</a><a href=\"";
                string s2 = "\"";
                s = info(s1, s2, s, i);
                string s3 = "title=\"";
                string s4 = "\"";
                s = info(s3, s4, s, i);
                i++;
            }
        }

        public String info(string start, string end, string order, int i)
        {
            int index = order.IndexOf(start) + start.Length;
            order = order.Substring(index);
            int index1 = order.IndexOf(end);
            string s = order.Substring(0, index1);
            if (s.StartsWith("http"))
            {
                this.News[i].Link = s;
            }
            else
            {
                this.News[i].Title = s;
            }
            string rs = order.Substring(index1);
            return rs;
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
