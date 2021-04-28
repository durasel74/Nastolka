using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nastolka
{
	public class ViewModel : INotifyPropertyChanged
    {
        BunchSets bunchSets = new BunchSets();

        private string viewOutput;
        public string ViewOutput
        {
            get { return viewOutput; }
            set
            {
                viewOutput = value;
                OnPropertyChanged("ViewOutput");
            }
        }

        // Команда генерации характеристик.
        private ButtonCommand genCommand;
        public ButtonCommand GenCommand
        {
            get
            {
                return genCommand ??
                  (genCommand = new ButtonCommand(obj =>
                  {
                      ViewOutput = bunchSets.RandomAll();
                  }));
            }
        }

        public ViewModel()
        {
            bunchSets.OpenAllSets();
		}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
