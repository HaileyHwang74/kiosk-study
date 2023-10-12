using System;
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

namespace ch31_유저컨트롤.uc
{
    /// <summary>
    /// UserControl1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public string 타이틀 { get; set; }
        public int 최대글자수   { get; set; }
        public int 피비높이  { get; set; }


        public UserControl1()
        {
            InitializeComponent();

            DataContext = this;
        }

       
    }
}
