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

namespace slSecure
{
    public partial class DivHtmlPage : Page
    {
        public DivHtmlPage()
        {
            InitializeComponent();
        }

        // 使用者巡覽至這個頁面時執行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string url = this.NavigationContext.QueryString["url"].ToString();
            this.htmlhost.HostDiv = "htmlhost";
            this.htmlhost.Url = url;
             

        }

    }
}
