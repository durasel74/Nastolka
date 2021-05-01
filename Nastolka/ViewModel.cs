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

        private string currentSettingName;
        public string CurrentSettingName
        {
            get { return currentSettingName; }
            set
            {
                currentSettingName = value;
                OnPropertyChanged("CurrentSettingName");
            }
        }

        private string setsCount;
        public string SetsCount
        {
            get { return setsCount; }
            set
            {
                setsCount = value;
                OnPropertyChanged("SetsCount");
            }
        }

        private string settingSetsCount;
        public string SettingSetsCount
        {
            get { return settingSetsCount; }
            set
            {
                settingSetsCount = value;
                OnPropertyChanged("SettingSetsCount");
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

        // Команда перемещения набора из общей области в сеттинг
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
                            BunchSets.SetToSetting(setName);
                            UpdateSettingSetsCount();
                        }
					}));
			}
        }

        // Команда перемещения набора из сеттинга в общую область
        private ButtonCommand toCommonCommand;
        public ButtonCommand ToCommonCommand
        {
            get
            {
                return toCommonCommand ??
                    (toCommonCommand = new ButtonCommand(obj =>
                    {
                        string setName = obj as string;
                        if (setName != null)
                        {
                            BunchSets.SetToCommon(setName);
                            UpdateSettingSetsCount();
                        }
                    }));
            }
        }

        // Команда перемещения набора вверх по сеттингу
        private ButtonCommand upOnSetting;
        public ButtonCommand UpOnSetting
        {
            get
            {
                return upOnSetting ??
                    (upOnSetting = new ButtonCommand(obj =>
                    {
                        string setName = obj as string;
                        if (setName != null)
                        {
                            BunchSets.MoveSetUp(setName);
                        }
                    }));
            }
        }

        // Команда перемещения набора вниз по сеттингу
        private ButtonCommand downOnSetting;
        public ButtonCommand DownOnSetting
        {
            get
            {
                return downOnSetting ??
                    (downOnSetting = new ButtonCommand(obj =>
                    {
                        string setName = obj as string;
                        if (setName != null)
                        {
                            BunchSets.MoveSetDown(setName);
                        }
                    }));
            }
        }

        public ViewModel()
        {
            BunchSets = new BunchSets();
            BunchSets.OpenAllSets();
            CurrentSettingName = "Новый";
            SetsCount = $"Всего: {BunchSets.GetCountSets()}";
            SettingSetsCount = $"Включено: 0";
		}

        // Обновляет индикатор количества включенных наборов
        private void UpdateSettingSetsCount()
        {
            SettingSetsCount = $"Включено: {BunchSets.SettingSets.Count}";
		}

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
