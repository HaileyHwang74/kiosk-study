﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ch29_화면이동_프레임.Views
{
    /// <summary>
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }


        //다른 페이지로 이동시킬 때 , 그 페이지를 먼저 인스턴스화 시켜주자.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Page2 page2 = new Page2();
            NavigationService .Navigate(page2);
        }
    }
}
