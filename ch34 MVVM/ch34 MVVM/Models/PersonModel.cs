using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ch34_MVVM.Models
{
    class PersonModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        //name에 해당하는 필드에 변화가 생길때마다 알려주는 메서드 구현
        protected void OnPropertyChanged(String name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(name));
        }

        private String name;
        private int age;

        //각 필드에 대한 property 구현
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name"); 

            }

        }

        public int Age
        {
            get { return age; }
            set
            {
                age = value;
                OnPropertyChanged("Age");

            }

        }
    }
}


