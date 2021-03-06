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
using System.Windows.Media.Imaging;
using System.ServiceModel.DomainServices.Client;
using slSecure.Web;
using slSecure.Controls;
using slWCFModule;
using slWCFModule.RemoteService;
using System.Threading.Tasks;
using System.Windows.Interactivity;
using slSecureLib.Forms;

namespace slSecure.Forms
{
    public partial class ControlRoom : Page
    {

        slSecure.Web.SecureDBContext db;
        DoorBindingData[] DoorBindingDatas;
        CCTVBindingData[] CCTVBindingDatas;
        ItemBindingData[] ItemBindingDatas;
        ItemGroupBindingData[] ItemGroupBindingDatas;
        MyClient client;
        int PlaneID;
        tblERPlane tblPlane;
        public ControlRoom()
        {
            InitializeComponent();
        }

        // 使用者巡覽至這個頁面時執行。

            Task<bool> GetALLDoorBindingData(int planeid)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
              
              client.SecureService.GetALLDoorBindingDataCompleted += (s, a) =>
                {
                    if (a.Error != null)
                    {
                        taskCompletionSource.TrySetResult(false);
                        return;
                    }
                    DoorBindingDatas = a.Result.ToArray();
                    taskCompletionSource.TrySetResult(true);
                };
              if (!IsExit)
              client.SecureService.GetALLDoorBindingDataAsync(planeid);
              return taskCompletionSource.Task;
        }
            Task<bool> GetALLCCTVBindingData(int planeid)
            {
                TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

                client.SecureService.GetAllCCTVBindingDataCompleted += (s, a) =>
                {
                    if (a.Error != null)
                    {
                        taskCompletionSource.TrySetResult(false);
                        return;
                    }
                    CCTVBindingDatas = a.Result.ToArray();
                    taskCompletionSource.TrySetResult(true);
                };
                if (!IsExit)
                client.SecureService.GetAllCCTVBindingDataAsync(planeid);
                return taskCompletionSource.Task;
            }

            Task<bool> GetAllItemBindingData(int planeid)
            {
                TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

                client.SecureService.GetAllItemBindingDataCompleted += (s, a) =>
                {
                    if (a.Error != null)
                    {
                        taskCompletionSource.TrySetResult(false);
                        return;
                    }
                    ItemBindingDatas = a.Result.ToArray();
                    taskCompletionSource.TrySetResult(true);
                };

                if (!IsExit)
                client.SecureService.GetAllItemBindingDataAsync(planeid);
                return taskCompletionSource.Task;
            }


            Task<bool> GetAllItemGroupBindingData(int planeid)
            {
                TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();

             //   System.Threading.Thread.Sleep(1000);
                client.SecureService.GetAllItemGroupBindingDataCompleted += (s, a) =>
                {
                    if (a.Error != null)
                    {
                        taskCompletionSource.TrySetResult(false);
                        return;
                    }
                    ItemGroupBindingDatas = a.Result.ToArray();
                    taskCompletionSource.TrySetResult(true);
                };
                if (!IsExit)
                client.SecureService.GetAllItemGroupBindingDataAsync(planeid);
                return taskCompletionSource.Task;
            }


            Task HookDoorEvent(int planeid)
          {
              TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
              client.SecureService.HookCardReaderEventCompleted += (s, a) =>
              {
                  if (a.Error != null)
                  {
                      taskCompletionSource.TrySetResult(false);
                     
                  }
                  taskCompletionSource.TrySetResult(true);
                 
               
              };
              client.SecureService.HookCardReaderEventAsync(client.Key,planeid);

              return taskCompletionSource.Task;
          }

        Task HookItemValueChangeEvent(int planeid)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            client.SecureService.HookItemValueChangedEventCompleted += (s, a) =>
            {
                if (a.Error != null)
                {
                    taskCompletionSource.TrySetResult(false);

                }
                taskCompletionSource.TrySetResult(true);


            };

            if (!IsExit) 
                    client.SecureService.HookItemValueChangedEventAsync(client.Key, planeid);

            return taskCompletionSource.Task;
        }
        protected async  override void OnNavigatedTo(NavigationEventArgs e)
        {
            client = new MyClient("CustomBinding_ISecureService", false);
         
            db = new SecureDBContext();
            this.PlaneID = int.Parse(this.NavigationContext.QueryString["PlaneID"]);

           


            this.image.Source = new BitmapImage(new Uri("/Diagrams/" + PlaneID + ".png", UriKind.Relative));
#if !R23
          
#endif
            if (!IsExit)
            await GetALLDoorBindingData(PlaneID);
            if (!IsExit)
            await GetALLCCTVBindingData(PlaneID);
            if (!IsExit)
            await GetAllItemBindingData(PlaneID);
            if (!IsExit)
            await GetAllItemGroupBindingData(PlaneID);
#if !R23

#endif
            PlaceDoor();
            PlaceCCTV();
            PlaceItem();
            PlaceItemGroup();
            var erplanes= await  db.LoadAsync<tblERPlane>(db.GetTblERPlaneQuery().Where(n=>n.PlaneID==this.PlaneID));
           this.tblPlane= erplanes.FirstOrDefault();
           this.DataContext = tblPlane;
           // tblPlane.PlaneName


      

           client.OnRegistEvent += async (s) =>
           {

               if (!IsExit)
                   await HookDoorEvent(PlaneID);
               if (!IsExit)
                   await HookItemValueChangeEvent(PlaneID);

           };
           client.OnDoorEvent += client_OnDoorEvent;
           client.OnItemValueChangedEvent += client_OnItemValueChangedEvent;
           if (!IsExit)
               await client.RegistAndGetKey();
        }

        void client_OnItemValueChangedEvent(ItemBindingData itemdata)
        {
            if (IsExit)
                return;
            if (this.ItemBindingDatas == null)
                return;

            ItemBindingData data = this.ItemBindingDatas.Where(n => n.ItemID == itemdata.ItemID).FirstOrDefault();
            bool IsDegreeChanged = false;
         if (data == null)
             return;

         data.Content = itemdata.Content;
         data.ColorString = itemdata.ColorString;
         data.IsAlarm = itemdata.IsAlarm;

         if (data.Degree != itemdata.Degree)
             IsDegreeChanged = true;
         data.Value = itemdata.Value;
         data.Degree = itemdata.Degree;

         if (itemdata.GroupID != null && IsDegreeChanged)
                   UpdateItemGroup((int)itemdata.GroupID);
        }
        void UpdateItemGroup(int GroupID)
        {
        
            if (IsExit)
                return;
            if (ItemGroupBindingDatas == null)
                return;
            MyClient myclient = new MyClient("CustomBinding_ISecureService", false);
            myclient.SecureService.GetAllItemGroupBindingDataCompleted += (s, a) =>
                {
                    if (a.Error != null)
                    {
                        myclient.Dispose();
                        return;
                    }
                    ItemGroupBindingData[] datas = a.Result.ToArray();

                    ItemGroupBindingData source = datas.Where(n => n.GroupID == GroupID).FirstOrDefault();
                   
                   ItemGroupBindingData  dest = ItemGroupBindingDatas.Where(n=>n.GroupID==GroupID).FirstOrDefault();
                    if(dest!=null && source!=null)
                        dest.ColorString=source.ColorString;
                    myclient.Dispose();
                };
            if (!IsExit)
            myclient.SecureService.GetAllItemGroupBindingDataAsync(this.PlaneID);
        }
        void client_OnDoorEvent(DoorEventType evttype, DoorBindingData bindingdata)
        {
            if (IsExit)
                return;
            DOOR door = Canvas.FindName("Door" + bindingdata.ControlID) as DOOR;
            door.DataContext = bindingdata;

            //throw new NotImplementedException();
        }

        async void PlaceCCTV()
        {
            //CCTV temp = new CCTV();
            //temp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            //temp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //temp.SetValue(Grid.MarginProperty, new Thickness(625, 500, 0, 0));
            //this.Canvas.Children.Add(temp);
            //return;

            if (IsExit)
                return;
            var q = from n in db.GetTblCCTVConfigQuery() where  n.PlaneID == this.PlaneID select n;
            var res = await db.LoadAsync<tblCCTVConfig>(q);

            foreach (tblCCTVConfig tbl in res)
            {

               
                CCTV item = new CCTV();
                item.Name = "CCTV" + tbl.CCTVID;
                

                item.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                item.VerticalAlignment = System.Windows.VerticalAlignment.Top;




                // this.Canvas.DataContext = item.DataContext = tbl;



                
                item.SetValue(Grid.MarginProperty, new Thickness(tbl.X, tbl.Y, 0, 0));
               // CompositeTransform transform = new CompositeTransform() { Rotation = tbl.Rotation, ScaleX = tbl.ScaleX, ScaleY = tbl.ScaleY };
               // item.RenderTransform = transform;
                CCTVBindingData bindingdata=null;
                
                if (CCTVBindingDatas == null)
                    return;
             
                  bindingdata=CCTVBindingDatas.FirstOrDefault(n => n.CCTVID==tbl.CCTVID );
                item.UserName = bindingdata.UserName;
                item.Password = bindingdata.Password;
                item.Url = bindingdata.MjpegCgiString;
                 
                item.DataContext =   bindingdata;
                item.MouseLeftButtonDown += CCTVLock_MouseLeftButtonDown;


                this.Canvas.Children.Add(item);
               
             //   CCTVLock_MouseLeftButtonDown(item, null);
               
                


            }

            CCTVBindingData cctvdata = CCTVBindingDatas.FirstOrDefault();
            if (cctvdata == null)
                return;
            if (IsExit)
                return;
            CCTVControl cctv = new CCTVControl(cctvdata.MjpegCgiString, cctvdata.UserName, cctvdata.Password);
            cctv.Name = "cctvctl"+cctvdata.CCTVName;
         //   cctv.Width = 400;
          //  cctv.Width = 300;
            cctv.Height = 200;
          //  Interaction.GetBehaviors(cctv).Add(new Microsoft.Expression.Interactivity.Layout.MouseDragElementBehavior());
            cctv.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            cctv.VerticalAlignment = System.Windows.VerticalAlignment.Top;
          //  this.Canvas.Children.Add(cctv);
            cctv.Margin = new Thickness(5,35,0,0);
            cctv.IsEnableCloseButton = false;
            Grid.SetRow(cctv, 0);
            Grid.SetColumn(cctv, 1);
            LayoutRoot.Children.Add(cctv);
        }

        void CCTVLock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.Canvas.FindName("cctvctl")!=null)
                return;
             Control ctl = sender as Control;
            CCTVBindingData data=ctl.DataContext as CCTVBindingData;
            CCTVControl cctv = new CCTVControl(data.MjpegCgiString, data.UserName, data.Password);
            cctv.Name = data.CCTVName;
            cctv.Width=400;
            cctv.Height=300;
           Interaction.GetBehaviors(cctv).Add(new Microsoft.Expression.Interactivity.Layout.MouseDragElementBehavior());
           cctv.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
          cctv.VerticalAlignment= System.Windows.VerticalAlignment.Top;
          this.Canvas.Children.Add(cctv);
            //throw new NotImplementedException();
        }

    
        async void PlaceDoor()
        {
            var q = from n in db.GetTblControllerConfigQuery() where (n.ControlType == 1 || n.ControlType == 2) && n.PlaneID == this.PlaneID select n;
            var res = await db.LoadAsync<tblControllerConfig>(q);

            foreach (tblControllerConfig tbl in res)
            {
                DOOR item = new DOOR();
                item.Name = "Door" + tbl.ControlID;


                item.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                item.VerticalAlignment = System.Windows.VerticalAlignment.Top;

             

             
                // this.Canvas.DataContext = item.DataContext = tbl;



               
                item.SetValue(Grid.MarginProperty, new Thickness(tbl.X ?? 0, tbl.Y ?? 0, 0, 0));
                CompositeTransform transform = new CompositeTransform() { Rotation = tbl.Rotation ?? 0, ScaleX = tbl.ScaleX ?? 0, ScaleY = tbl.ScaleY ?? 0 };
                item.RenderTransform = transform;
                if(DoorBindingDatas!=null)
                item.DataContext =  DoorBindingDatas.FirstOrDefault(n => n.ControlID == tbl.ControlID);
                this.Canvas.Children.Add(item);
                item.OnMenuEvent += item_OnMenuEvent;
                //item.MouseLeftButtonDown += selectedDevice_MouseLeftButtonDown;
                //item.MouseLeftButtonUp += selectedDevice_MouseLeftButtonUp;
                //item.MouseMove += selectedDevice_MouseMove;


            }
        }

        void item_OnMenuEvent(object sender, MenuItem item)
        {
            if (item.Header.ToString() == "中心開門")
            {
                client.SecureService.ForceOpenDoorAsync((item.DataContext as DoorBindingData).ControlID);
                client.SecureService.ForceOpenDoorCompleted += (s, a) =>
                    {
                        if (a.Error != null)
                            MessageBox.Show(a.Error.Message);
                        else
                            MessageBox.Show("ok!");
                    };
            }
            //throw new NotImplementedException();
        }


        async void PlaceItemGroup()
        {
            var q = from n in db.GetTblItemGroupQuery() where n.PlaneID == this.PlaneID && n.IsShow select n ;
            var res = await db.LoadAsync<tblItemGroup>(q);
            foreach (tblItemGroup tbl in res)
            {
                ItemGroup item = new ItemGroup() { Name = "Grp" + tbl.GroupID };

                (item as Control).HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                (item as Control).VerticalAlignment = System.Windows.VerticalAlignment.Top;
                (item as Control).SetValue(Grid.MarginProperty, new Thickness(tbl.X , tbl.Y , 0, 0));
                CompositeTransform transform = new CompositeTransform() { Rotation = tbl.Rotation , ScaleX = tbl.ScaleX  , ScaleY = tbl.ScaleY   };
                (item as Control).RenderTransform = transform;
                if (ItemGroupBindingDatas != null)
                {
                    ItemGroupBindingData data =ItemGroupBindingDatas.FirstOrDefault(n => n.PlaneID == this.PlaneID && n.GroupID == tbl.GroupID);
                  //  data.Content = "hello";
                    (item as Control).DataContext = data;// ItemGroupBindingDatas[0];
                 
                }
                item.MouseLeftButtonDown += itemGroup_MouseLeftButtonDown;
                this.Canvas.Children.Add(item);
            }
        }
        void itemGroup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int gid = ((sender as ItemGroup).DataContext as ItemGroupBindingData).GroupID;

            this.lstIOMenu.ItemsSource = ItemBindingDatas.Where(n => n.GroupID == gid && (n.Type == "AI" || n.Type == "DI"));
        }
        async void  PlaceItem()
        {
             var q = from n in db.GetTblItemConfigQuery()  where   n.tblItemGroup.PlaneID == this.PlaneID  && n.IsShow select n;
             var res = await db.LoadAsync<tblItemConfig>(q);

             foreach (tblItemConfig tbl in res)
             {

                 I_IO item;
                 if (tbl.Type == "AI")
                 {
                     item = new AI() { Name = "AI" + tbl.ItemID };

                 }
                 else if (tbl.Type == "DI")
                     item = new DI() { Name = "DI" + tbl.ItemID };
                 else if (tbl.Type == "DO")
                     item = new DO() { Name = "DO" + tbl.ItemID };
                 else
                     continue;
                // item.Name = "Door" + tbl.ControlID;


                 (item as Control).HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                 (item as Control).VerticalAlignment = System.Windows.VerticalAlignment.Top;
                 (item as Control).SetValue(Grid.MarginProperty, new Thickness(tbl.X ?? 0, tbl.Y ?? 0, 0, 0));
                 CompositeTransform transform = new CompositeTransform() { Rotation = tbl.Rotation ?? 0, ScaleX = tbl.ScaleX ?? 0, ScaleY = tbl.ScaleY ?? 0 };
                 (item as Control).RenderTransform = transform;
                if(ItemBindingDatas!=null)
                (item as Control).DataContext = ItemBindingDatas.FirstOrDefault(n=>n.PlaneID==this.PlaneID && n.ItemID==tbl.ItemID  );

                if (tbl.Type == "DO") 
                (item as Control).MouseLeftButtonDown += DO_MouseLeftButtonDown;
                 this.Canvas.Children.Add(item as Control);
             }
        }

        void DO_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DO Do=sender as DO;
            if((int)(Do.DataContext as ItemBindingData).Value==0)
                client.SecureService.SetItemDOValueAsync((Do.DataContext as ItemBindingData).ItemID,true);
            else
                client.SecureService.SetItemDOValueAsync((Do.DataContext as ItemBindingData).ItemID, false);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void DOOR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CCTV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            new Dialog.CCTVDialog().Show();
        }

        private void ScrollViewer_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewBox.Width = scrollViewer.ViewportWidth;
            viewBox.Height = scrollViewer.ViewportHeight;
           
            //Canvas.Width = image.ActualWidth;
            //Canvas.Height = image.ActualHeight;
        }

        private void Viewbox_SizeChanged(object sender, SizeChangedEventArgs e)
        {
          
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            //Point p = e.GetPosition(Canvas);
            //txtCoor.Text = string.Format("{0},{1}", p.X, p.Y);
        }


        bool IsExit = false;
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            IsExit = true;
            if(client!=null)
            client.Dispose();
        }

        private void IOMenu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemBindingData data = ((sender as IOMenu).DataContext as ItemBindingData);
            if (data.Type != "AI")
                return;

           Dialog.TRDialog dialog=   new Dialog.TRDialog(data.ItemID);
           //dialog.Width = 800;
           //dialog.Height = 600;
           dialog.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new ItemGuide().Show();
        }

      

    }
}
