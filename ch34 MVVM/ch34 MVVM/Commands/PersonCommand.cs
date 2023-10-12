using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ch34_MVVM.Commands
{
    //기존의  internal class 를 public  으로 바꾸어줌
    //라디오버튼 등과 컨트롤에 이벤트 처리하기 위한 인터페이스 
    public class PersonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        //return  값이 없을 때 사용 가능
        Action<String> exe;

        //boolean 값으로 return 하는 delegate
        Predicate<string> canExe;

        //생성자 축약어 ctor
        public PersonCommand(Action<String> msg, Predicate<string> checkMsg)
        {
            exe = msg;
            canExe = checkMsg;

        }


        //Predicate는 boolean 값 return 하는 delegate

        public bool CanExecute(object? parameter)
        {
            //true일 때만 아래의 exectue 실행가능
            // return true; 대신에 아래의 코드로 대체
            return canExe.Invoke(parameter as string);
        }
         
        public void Execute(object? parameter)
        {
            exe.Invoke(parameter as string);
        } 
    }
}
