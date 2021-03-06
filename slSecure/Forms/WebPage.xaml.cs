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
using System.Text;

namespace slSecure.Forms
{
    public partial class WebPage : Page
    {

       
        public WebPage()
        {
            InitializeComponent();
         
        }

        // 使用者巡覽至這個頁面時執行。
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                string url = this.NavigationContext.QueryString["url"].ToString();
//                var html = new StringBuilder(@"<html xmlns=""http://www.w3.org/1999/xhtml"" lang=""EN""> 
//                                            <head> 
//                                            <meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /> 
//                                            <title>{@pageTitle}</title> 
//                                            <style type=""text/css""> 
//                                            html {overflow: auto;} 
//                                            html, body, div, iframe {margin: 0px; padding: 0px; height: 100%; border: none;} 
//                                            iframe {display: block; width: 100%; border: none; overflow-y: auto; overflow-x: hidden;} 
//                                            </style> 
//                                            </head> 
//                                            <body> 
//                                            <iframe id=""tree"" name=""tree""   src=""{@PageLink}"" frameborder=""0"" marginheight=""0"" marginwidth=""0"" width=""100%"" height=""100%"" scrolling=""auto""></iframe> 
//                                            </body> 
//                                            </html>");
//                html.Replace("{@pageTitle}", Title);
//                html.Replace("{@PageLink}", url.ToString());
//                this.browser.NavigateToString(html.ToString());

                this.browser.Navigate(new Uri(url, UriKind.Absolute));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
              
             
           
        }

    }
}
