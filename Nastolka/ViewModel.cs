using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nastolka
{
	public class ViewModel : INotifyPropertyChanged
    {
        public string TestText { get; set; }


        public ViewModel()
        {
            Load();
		}

        private void Load()
        {
            TestText = Parser.ParseAllFromDirectory();
		}


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
