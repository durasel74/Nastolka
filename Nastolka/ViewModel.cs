using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nastolka
{
	public class ViewModel : INotifyPropertyChanged
    {
        BunchSets bunchSets = new BunchSets();

        public string TestText { get; set; }

        public ViewModel()
        {
            Load();
		}

        private void Load()
        {
            bunchSets.OpenAllSets();
            TestText = bunchSets.GetAllSetsInfo();
		}


        private ButtonCommand genCommand;
        public ButtonCommand GenCommand
        {
            get
            {
                return genCommand ??
                  (genCommand = new ButtonCommand(obj =>
                  {
                      
                  }));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
