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

        // Команда переноса всех наборов в общую область
        private ButtonCommand allToCommon;
        public ButtonCommand AllToCommon
        { 
            get
            {
                return allToCommon ??
                    (allToCommon = new ButtonCommand(obj =>
                    {
						BunchSets.AllSetsToCommon();
                        UpdateSettingSetsCount();
					}));
			}
        }

        // Команда переноса всех наборов в сеттинг
        private ButtonCommand allToSetting;
        public ButtonCommand AllToSetting
        {
            get
            {
				return allToSetting ??
					(allToSetting = new ButtonCommand(obj =>
					{
						BunchSets.AllSetsToSetting();
                        UpdateSettingSetsCount();
                    }));
			}
        }

        // Команда пункта меню
        private ButtonCommand menuCommand;
        public ButtonCommand MenuCommand
        { 
            get
            {
                return menuCommand ??
                    (menuCommand = new ButtonCommand(obj =>
                    {
                        string command = obj as string;
                        if (command != null)
                        {
                            MenuCommander(command);
						}
                    }));
            }
        }

        public ViewModel()
        {
            BunchSets = new BunchSets();
            BunchSets.OpenAllSets();
            BunchSets.CurrentSettingName = "Новый";
            SetsCount = $"Всего: {BunchSets.GetCountSets()}";
            SettingSetsCount = $"Включено: 0";
		}

        // Обновляет индикатор количества включенных наборов
        private void UpdateSettingSetsCount()
        {
            SettingSetsCount = $"Включено: {BunchSets.SettingSets.Count}";
		}

        // Принимает команду меню и выбирает действие
        private void MenuCommander(string command)
        {
            switch (command)
            {
                case "new":
                    NewSetting();
                    break;
                case "open":
                    OpenSetting();
                    break;
                case "save":
                    SaveSetting();
                    break;
                case "exit":
                    System.Windows.Application.Current.Shutdown();
                    break;
            }
		}

        // Создает пустой сеттинг
        private void NewSetting()
        {
            AllToCommon.Execute(null);
            BunchSets.CurrentSettingName = "Новый";
		}

        // Диалог открытия сеттинга
        private void OpenSetting()
        {
            var openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.DefaultExt = ".json";
            openDialog.Filter = "Text files (*.json)|*.json";

            Nullable<bool> result = openDialog.ShowDialog();
            if (result == true)
            {
                AllToCommon.Execute(null);
                string fileName = openDialog.FileName;
                BunchSets.DeserializeSetting(fileName);
            }
        }

        // Диалог сохранения сеттинга
        private void SaveSetting()
        {
            var saveDialog = new Microsoft.Win32.SaveFileDialog();
            if (BunchSets.CurrentSettingName != "Новый")
                saveDialog.FileName = BunchSets.CurrentSettingName;
            saveDialog.DefaultExt = ".json";
            saveDialog.Filter = "Text files (*.json)|*.json";

            Nullable<bool> result = saveDialog.ShowDialog();
            if (result == true)
            {
                string fileName = saveDialog.FileName;
                BunchSets.SerializeCurrentSetting(fileName);
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
