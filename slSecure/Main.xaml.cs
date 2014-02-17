﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Common;
using Common.Info;
using slSecure.Web;
using System.ServiceModel.DomainServices.Client;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace slSecure
{
    public partial class Main : Page
    {
        ObservableCollection< CCTVLockInfo> cctvLocks = new ObservableCollection< CCTVLockInfo>();
        DateTime LastOperationDatetime = System.DateTime.Now;
        System.Windows.Threading.DispatcherTimer tmr = new System.Windows.Threading.DispatcherTimer();
        MessageInfo[] msgInfos = new MessageInfo[] { 
           new MessageInfo(){ DateTime=DateTime.Now, Type="A", AlarmStatus=2, Message="Message1Message1"},
           new MessageInfo(){ DateTime=DateTime.Now, Type="D", AlarmStatus=1, Message="Message1Message1"},
           new MessageInfo(){ DateTime=DateTime.Now, Type="A", AlarmStatus=0, Message="Message1Message1"}
        };
        public Main()
        {
            InitializeComponent();
            Util.GetICommon().SetMain(this);
            this.lstMessage.ItemsSource = msgInfos;

        }

        // 使用者巡覽至這個頁面時執行。
        protected  async  override void OnNavigatedTo(NavigationEventArgs e)
        {
         
            tmr.Interval = TimeSpan.FromSeconds(30);
            tmr.Tick += tmr_Tick;
            tmr.Start();
        
           
          //  Microsoft.Expression.Interactivity.Layout.FluidMoveBehavior bev = new Microsoft.Expression.Interactivity.Layout.FluidMoveBehavior();

        SecureDBContext db= DB.GetDB();

        EntityQuery<vwUserMenuAllow> q=   db.GetVwUserMenuAllowQuery().Where(n => n.UserID == Util.GetICommon().GetUserID());
            var UserMenus =  await  db.LoadAsync(q);

            var res = from n in UserMenus  group n by n.GroupName into g   select  new UserGroupMenu() { GroupMenu = g.Key, Menus = g.OrderBy(k=>k.MenuOrder).ToList()  }  ;
            this.acdMenu.ItemsSource = res;
           this.frameMain.Navigate(new Uri("/Forms/Monitor.xaml", UriKind.Relative));
           txtTitle.DataContext = new tblMenu() { MenuName = "門禁監控" };
            this.acdMenu.SelectedIndex =1;
        }

        void tmr_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Subtract(LastOperationDatetime) > TimeSpan.FromMinutes(1 ))
            {
                tmr.Stop();
                this.NavigationService.Navigate(new Uri("/Login.xaml", UriKind.Relative));
            }
            //throw new NotImplementedException();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.acdMenu.Visibility == Visibility.Visible)
                this.acdMenu.Visibility = System.Windows.Visibility.Collapsed;
            else
                this.acdMenu.Visibility = System.Windows.Visibility.Visible;
        }

        private void acdMenu_SelectedItemsChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.txtTitle.DataContext = (sender as Control).DataContext;
            vwUserMenuAllow menu = (sender as Control).DataContext as vwUserMenuAllow;
            if (menu.XAML.Trim().ToUpper().StartsWith("HTTP:"))
                this.frameMain.Navigate(new Uri("/Forms/WebPage.xaml?url=" + menu.XAML, UriKind.Relative));
            else
                this.frameMain.Navigate(new Uri(menu.XAML, UriKind.Relative));
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            tmr.Stop();
            this.NavigationService.Navigate(new  Uri("/Login.xaml",UriKind.Relative));
          
        }

        private void Page_MouseMove(object sender, MouseEventArgs e)
        {
            LastOperationDatetime = DateTime.Now;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private async void BtbCCTVLock_Click(object sender, RoutedEventArgs e)
        {
           
         //   cctvLocks.Add(new Source.CCTVLockInfo() { DateTime = DateTime.Now });
         //   lstCCTVLock.ItemsSource = from n in cctvLocks orderby n.DateTime descending select n; 
          //  this.lstCCTVLock.Children.Add(new slSecure.Controls.CCTVLock() { Width = 200, Height = 150 });
            await AddCCTVAsync();
          //  MessageBox.Show("ok");
        }

        public Task<object> AddCCTVAsync()
        {
            slSecure.Controls.CCTVLock cctv=new slSecure.Controls.CCTVLock(new Random().Next(1,40)) { Width = 250, Height =200, Margin = new Thickness(10) };
            System.Threading.Tasks.TaskCompletionSource<object> source = new TaskCompletionSource<object>();
            cctv.Click += cctv_Click;
            this.lstCCTVLock.Children.Insert(0,cctv );
            source.SetResult(new object());
            return source.Task;
        }

        void cctv_Click(object sender, MouseButtonEventArgs e)
        {
            this.frameMain.Navigate(new Uri("/Forms/ControlRoom.xaml", UriKind.Relative));
        }

        


        public void Navigate(Uri uri)
        {
            this.frameMain.Navigate(uri);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.lstCCTVLock.Children.Clear();
        }

        private void lstMessage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.frameMain.Navigate(new Uri("/Forms/ControlRoom.xaml", UriKind.Relative));
        }

      

    }

    public class UserGroupMenu
    {
        public string GroupMenu { get; set; }
        public System.Collections.Generic.List<vwUserMenuAllow> Menus { get; set; }

    }
}