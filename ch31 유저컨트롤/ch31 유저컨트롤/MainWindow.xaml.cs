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

namespace ch31_유저컨트롤
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //userControl 에서 testbox  이름을 txt로 지었기 때문에 .txt  들어간다.
            MessageBox.Show(uc1.txt.Text);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //userControl 에서 testbox  이름을 txt로 지었기 때문에 .txt  들어간다.
            MessageBox.Show(uc2.Pb.Value.ToString());
        }
    }
}
