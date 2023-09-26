using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Pipes;
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

namespace ch28_영어단어맞추기
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// INotifyPropertyChanged 인터페이스 추가
    /// window.INotifyPropertyChanged 에서 오류가 나서 window 빼버렸더니 오류 해결 됨 
    /// 


    public partial class MainWindow : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        //name 에 binding이 되서 name이 바뀔 때마다 바뀌어짐
        protected void OnPropertyChanged(String name)
        {

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private String wrongStatus;
        private String selEng;
        private String selKor;
        private String message;

        //담을 리스트 만들어준다.
        private List<Char> btns = new List<Char>();
        public string WrongStatus
        {
            get => wrongStatus;
            set
            {

                wrongStatus = value;
                OnPropertyChanged("WrongStatus");
            }
        }

        public String SelEng
        {
            get => selEng;
            set
            {
                selEng = value;
                OnPropertyChanged("SelEng");
            }
        }
        public String SelKor
        {
            get => selKor;
            set
            {
                selKor = value;
                OnPropertyChanged("SelKor");
            }
        }
        public String Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }


        List<char> SelWord = new List<Char>();
        List<String> words = new List<String>()
        {
            "boy.소년",
            "school.학교",
            "fish.물고기",
            "car.자동차",
            "book.책"
        };

        //틀린횟수 저장할 변수
        int wrong = 0;
        //틀린 횟수 최대 변수
        int maxWrong = 3;
        string compareWord = string.Empty;



        public MainWindow()
        {
            InitializeComponent();


            //Mainwindows.xaml에서  Binding을 했지만 제대로 적용되지 않음
            // -> 오류로 DataContext를 찾을 수 없다고 나왔음. 다음 코드 안 써줘서 그러했음
            DataContext = this;
            //알파벳 글자 하나씩 btns 에 char로써 들어감
            btns.AddRange("abcdefghijklmnopqrstuvwxyz");
            ic.ItemsSource = btns;
            RandomWord();
            ChangeWord(SelEng, SelWord);
            WrongStatus = $"틀린횟수: {wrong} of {maxWrong}";
        }

        //영어단어를 * 표시로 치환해주는 메서드
        private void ChangeWord(String selEng, List<Char> selWord)
        {
            char[] result = selEng.Select(x => (selWord.IndexOf(x) >= 0 ? x : '*')).ToArray();
            SelEng = String.Join(" ", result);

        }




        private void RandomWord()
        {
            String[] selchar = words[new Random().Next(words.Count)].Split(".");
            SelEng = selchar[0];
            SelKor = selchar[1];
            //사용자가 입력한 부분이랑 비교하는 부분이므로 default 영어로 설정한다.
            compareWord = SelEng;
        }


        //알파벳 버튼 처리
        //다시하기
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //그동안 만든 변수 초기화해주고 , 메서드 실행하면 된다.
            wrong = 0;
            SelWord = new List<Char>();
            RandomWord();
            ChangeWord(SelEng, SelWord);
            Message = "알파벳을 선택하여 주세요";
            Status();
            // SelWord 에 a~z까지 넣었으므로, a 부터 비활성화 된 것을 활성화로 풀어준다.
            // c부터 해도 어차피 foreach라서 그 안에 item 에 있는 거 싹 돌아서 전부 활성화로 바뀌나보다.
            foreach (var a in ic.Items)
            {
                var btn = (UIElement)ic.ItemContainerGenerator.ContainerFromItem(a);
                if (btn != null)
                {
                    btn.IsEnabled = true; //비활성화된 버튼이 활성화되도록
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //sender를 button으로 형변환
            var btn = sender as Button;
            if (btn != null)
            {
                //버튼을 눌렀을 때 표시된 글자가 별표시에 들어감
                var result = btn.Content.ToString();
                CheckWord(result[0]);
            }
        }

        private void CheckWord(char v)
        {
            //아예 아무것도 입력이 안 되어있을 때
            if (SelWord.IndexOf(v) == -1)
            {
                SelWord.Add(v);
            }
            // compareword 와 비교해서 선택한 알파벳이 있으면, 별 표시를 치환하기 위해 다시 chagneword로 보내줌


            if (compareWord.IndexOf(v) >= 0)
            {
                ChangeWord(compareWord, SelWord);
                CheckWin();
            }
            // 누른 알파벳이 compareword에 없는 경우
            else if (compareWord.IndexOf(v) == -1)
            {
                wrong++;
                Status();
                CheckLost();
            }
        }

        // $'' 말고 $""으로 해야 적용된다.
        private void Status()
        {
            WrongStatus = $"틀린횟수: {wrong} of {maxWrong}";
        }

        private void CheckWin()
        {   //changeWord 에서 공백을 지우고 봤을 때 같으면 you win 메시지 나오게끔
            if (compareWord == SelEng.Replace(" ", ""))
            {
                Message = "You Win!";
                //정답을 맞추고나서 비활성화
                foreach (var a in ic.Items)
                {
                    var btn = (UIElement)ic.ItemContainerGenerator.ContainerFromItem(a);
                    if (btn != null)
                    {
                        btn.IsEnabled = false;
                    }
                }

            }
        }

        // 3번 틀리면 비활성화 하도록
        // 화면에서 컨트롤러 이름을 ic로 지어놓음
        private void CheckLost()
        {
            if (wrong == maxWrong)
            {
                Message = "You Lost!";
                // 3번 틀린거를 어디서 체크하지
                foreach (var a in ic.Items)
                {
                    var btn = (UIElement)ic.ItemContainerGenerator.ContainerFromItem(a);
                    if (btn != null)
                    {
                        btn.IsEnabled = false;
                    }
                }

            }
        }
    }
}
