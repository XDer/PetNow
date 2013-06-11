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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Pet_Now
{
    public class MessageController : INotifyPropertyChanged
    {
        public MessageController()
        {
            messages = new ObservableCollection<Message>();
            IsDataLoaded = false;
        }
        private ObservableCollection<Message> messages;
        public ObservableCollection<Message> Messages
        {
            get { return messages; }
            set
            {
                if (messages != value)
                {
                    messages = value;
                    NotifyPropertyChanged("Messages");
                }
            }
        }

        public bool IsDataLoaded { get; private set; }

        public void LoadData()
        {
            messages.Add(new Message("美流浪狗成抽象派大画家，画作售价上千", "这位“抽象派大画家”以前是美国的一只流浪狗，生活在拉斯维加斯动物收容所，后来被一对美国夫妇领养，其名字叫做Arbor。当然在被领养后，Arbor的生活发生了变化，而且它的命运也就此改变了。\n在被领养后的一段时间里，主人夫妇惊奇的发现，Arbor居然天赋异禀，擅长作画而且是抽象画派。经过训练其画工与日俱进，摇身变成“抽象派画师”。如今Arbor的一副画作，售价可以买到3000元人民币了。\n主人夫妇回忆道，“当时，我们只是想找个伴。”然而，一次偶然的机会，他们发现Ardor极富灵性，会耍各种把戏。于是，他们试着教它用嘴叼笔作画。之后，Ardor逐渐掌握了要领，画工也与日俱进，可谓是“当代毕加索”。更难得的是，Ardor还将它的画作所得悉数捐给动物慈善机构，救助苦难的同胞。", @"Image/work1.jpg"));
            messages.Add(new Message("晒晒雪藏了一个月~终于长齐了鼻毛的何甜甜同学", "光板前拍照留念下~", @"Image/work2.jpg"));
            messages.Add(new Message("狗狗病了 路都走不了", "小狗 3岁 健康活泼 几乎从未生病，偏胖。突然之间今天站都站不起来后腿没有力气，只能拖者走路。不肯睡觉，一抱就叫。到宠物医院看病。医生诊断为肥胖引起脊柱压迫神经...一般6岁左右发病。主要原因喂人吃的太多。治疗方式是打针和吃药，兽医讲现在狗还年轻80%可以看好，要是3天站不起来，就难站起来了 。可能就瘫了，我可不想让他瘫痪啊，求求各位，他们打激素，地塞米松！人打多了这个也不好啊，狗狗也是一样的！", null));
            messages.Add(new Message("主人一声令下，小狗狗就去弹钢琴！弹吉他！太可爱了.", "主人一声令下，小狗狗就去弹钢琴！弹吉他！太可爱了.", null));
            messages.Add(new Message("发个幼犬狗粮总结，供大家参考", "幼犬狗粮总结以前发了一个，最近又整理了一些。价格参考了国内的一个正规宠物商城。给大家做个参考吧。) 我家芒果还在幼年期，所以整理的都是幼犬和全犬的。都是国外的天然粮，20块钱之内的。按照平安电话...", null));
            messages.Add(new Message("求救啊我家狗狗早上不肯睡觉", "四个月差点的贵宾！刚来家里一个星期！白天都很乖！即使我们出门也不乱叫！但是就是晚上不肯睡觉怎么办？刚来的时候把笼子放房间床边它还会睡觉只是半夜会醒两次！叫几下再哄哄它就好了！今天一个晚上不肯睡觉...", null));            
            IsDataLoaded = true;
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
