using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Nastolka
{
	public class ViewModel : INotifyPropertyChanged
    {
        public BunchSets BunchSets { get; set; }

        private string setInfoName; 
        public string SetInfoName
        {
            get { return setInfoName; }
            set
            {
                setInfoName = value;
                OnPropertyChanged("SetInfoName");
            }
        }

        private string setInfo;
        public string SetInfo
        {
            get { return setInfo; }
            set
            {
                setInfo = value;
                OnPropertyChanged("SetInfo");
            }
        }

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
                      ViewOutput = BunchSets.RandomizeSettingSets();
                  }));
            }
        }

        // Команда вывода информации о наборе
        private ButtonCommand infoCommand;
        public ButtonCommand InfoCommand
        { 
            get
            {
                return infoCommand ??
                    (infoCommand = new ButtonCommand(obj =>
                    {
                        string setName = obj as string;
                        if (setName != null)
                        {
                            SetInfoName = setName;
                            SetInfo = BunchSets.GetSetInfo(setName);
                            var infoWindow = new SetInfoWindow(this);
                            infoWindow.ShowDialog();
                        }
                    }));
            }
        }

        //
        private ButtonCommand toSettingCommand;
        public ButtonCommand ToSettingCommand
        { 
            get
            {
                return toSettingCommand ??
                    (toSettingCommand = new ButtonCommand(obj =>
                    {
                        string setName = obj as string;
                        if (setName != null)
						{
							
						}
					}));
			}
        }

        public ViewModel()
        {
            BunchSets = new BunchSets();
            BunchSets.OpenAllSets();
		}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
