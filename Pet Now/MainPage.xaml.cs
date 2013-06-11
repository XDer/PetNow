using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;
using WeiboSdk;

namespace Pet_Now
{
    public partial class MainPage : PhoneApplicationPage
    {
        // 构造函数
        public MainPage()
        {
            InitializeComponent();

            ReadPhoto();
            // 将 listbox 控件的数据上下文设置为示例数据
            DataContext = App.MessageController;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            NewsList.DataContext = newsController;
        }

        NewsController newsController = new NewsController();

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            newsController.LoadData();
            ItemInfo.DataContext = App.UserPet;
            base.OnNavigatedTo(e);
        }

        // 为 ViewModel 项加载数据
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.MessageController.IsDataLoaded)
            {
                App.MessageController.LoadData();
            }
        }

        private void MenuTakePhoto_Click(object sender, EventArgs e)
        {
            TakePhoto();
        }

        private const string imageName = "head.jpg";
        private void TakePhoto()
        {
            CameraCaptureTask task = new CameraCaptureTask();
            task.Show();
            task.Completed += (sender, e) =>
            {
                if (e.ChosenPhoto == null)
                    return;
                SavePhoto(e.ChosenPhoto);
                BitmapImage image = new BitmapImage();
                image.SetSource(e.ChosenPhoto);
                ImageHead.Source = image;
            };
        }

        private void SavePhoto(Stream photoStream)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedStorageFileStream fileStream = storage.CreateFile(imageName);
                photoStream.CopyTo(fileStream);
                fileStream.Close();
            }
        }
        private void ReadPhoto()
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!storage.FileExists(imageName))
                {
                    BitmapImage i = new BitmapImage(new Uri(@"Image/test0.jpg", UriKind.Relative));
                    ImageHead.Source = i;
                }
                else
                {
                    using (IsolatedStorageFileStream stream = storage.OpenFile(imageName, FileMode.Open))
                    {
                        BitmapImage image = new BitmapImage();
                        image.SetSource(stream);
                        ImageHead.Source = image;
                    }
                }
            }
        }

        private void MenuChoosePhoto_Click(object sender, EventArgs e)
        {
            ChoosePhoto();
        }

        private void ChoosePhoto()
        {
            PhotoChooserTask task = new PhotoChooserTask();
            task.Show();
            task.Completed += (sender, e) =>
                {
                    if (e.ChosenPhoto == null)
                        return;
                    SavePhoto(e.ChosenPhoto);
                    BitmapImage image = new BitmapImage();
                    image.SetSource(e.ChosenPhoto);
                    ImageHead.Source = image;
                };
        }

        private void MenuResetPhoto_Click(object sender, EventArgs e)
        {
            using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (storage.FileExists(imageName))
                {
                    BitmapImage i = new BitmapImage(new Uri(@"Image/test0.jpg", UriKind.Relative));
                    ImageHead.Source = i;
                    storage.DeleteFile(imageName);
                }
            }
        }

        private void MenuSetting_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void MenuAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(App.AccessToken))
            {
                MessageBox.Show("未连接到新浪微博。");
            }
            else
            {
                SdkShare sdkShare = new SdkShare
                {
                    //设置OAuth2.0的access_token
                    AccessToken = App.AccessToken,
                    //AccessTokenSecret = App.AccessTokenSecret,
                    //PicturePath = "TempJPEG.jpg",
                    //Message = this.messageTextBlock.Text
                };
                sdkShare.Completed = new EventHandler<SendCompletedEventArgs>(ShareCompleted);
                //show it
                sdkShare.Show();
            }
        }
        void ShareCompleted(object sender, SendCompletedEventArgs e)
        {
            if (e.IsSendSuccess)
                MessageBox.Show("发送成功");
            else
                MessageBox.Show(e.Response, e.ErrorCode.ToString(), MessageBoxButton.OK);
        }

        private void NewsListBox_MouseLeftButtonUp(object sender, SelectionChangedEventArgs e)
        {
            if (NewsListBox.SelectedIndex > -1)
            {
                this.NavigationService.Navigate(new Uri("/NewsPage.xaml?index=" + NewsListBox.SelectedIndex, UriKind.Relative));
            }
        }
    }
}