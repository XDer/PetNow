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
using System.Windows.Media.Imaging;

namespace Pet_Now
{
    public class Message
    {
        public Message()
        {
            Title = string.Empty;
            Content = string.Empty;
            source = null;
            image = null;
        }
        public Message(string title,string content,string source)
        {
            Title = title;
            Content = content;            
            if (source != null)
            {
                this.source = new Uri(source, UriKind.Relative);
                image = new BitmapImage(this.source);
            }
            else
            {
                image = null;
            }
        }

        public string Title { get; set; }
        public string Content { get; set; }
        private Uri source;
        public Uri ImageSource
        {
            get
            {
                return source;
            }
            set
            {
                if (source != value)
                {
                    source = value;
                    if (source != null)
                    {
                        image = new BitmapImage(source);
                    }
                }
            }
        }

        private BitmapImage image;
        public BitmapImage Image { get { return image; } }
    }
}
