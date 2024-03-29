﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using AgentAssignment5;

namespace AgentAssignment5
{
    //
    public class Agents : ObservableCollection<Agent>, INotifyPropertyChanged
    {
        private string filename = "";

        public Agents()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                Add(new Agent("001", "Nina", "Assassination", "UpperVolta"));
                Add(new Agent("007", "James Bond", "Martinis", "North Korea"));
            }
        }

        #region Commands

        ICommand _addCommand;
        public ICommand AddCommand
        {
            get { return _addCommand ?? (_addCommand = new RelayCommand(AddAgent)); }
        }

        private void AddAgent()
        {
            Add(new Agent());
            CurrentIndex = Count - 1;
        }


        ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(DeleteAgent, DeleteAgent_CanExecute)); }
        }

        private void DeleteAgent()
        {
            RemoveAt(CurrentIndex);
        }

        private bool DeleteAgent_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }

        ICommand _nextCommand;
        public ICommand NextCommand
        {
            get
            {
                return _nextCommand ?? (_nextCommand = new RelayCommand(
                    () => ++CurrentIndex,
                    () => CurrentIndex < (Count - 1)));
            }
        }

        ICommand _PreviusCommand;
        public ICommand PreviusCommand
        {
            get { return _PreviusCommand ?? (_PreviusCommand = new RelayCommand(PreviusCommandExecute, PreviusCommandCanExecute)); }
        }

        private void PreviusCommandExecute()
        {
            if (CurrentIndex > 0)
                --CurrentIndex;
        }

        private bool PreviusCommandCanExecute()
        {
            if (CurrentIndex > 0)
                return true;
            else
                return false;
        }

        private ICommand _SaveAsCommand;

        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argsFileName)
        {
            if (argsFileName == "")
            {
                MessageBox.Show("You must enter a file name in the File Name text box!", "Unable to save file");
            }
            else
            {
                filename = argsFileName;
                SaveFileCommand_Execute();
            }
        }

        private ICommand _SaveCommand;

        public ICommand SaveCommand
        {
            get { return _SaveCommand ?? (_SaveCommand = new RelayCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)); }
        }

        private void SaveFileCommand_Execute()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Agents));
            TextWriter writer = new StreamWriter(filename);

            serializer.Serialize(writer, this);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return ((filename != "") && (Count > 0));
        }

        private ICommand _NewFileCommand;

        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new RelayCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult result =
                MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?",
                    "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);

            if (result == MessageBoxResult.Yes)
            {
                Clear();
                filename = "";
            }
        }

        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new RelayCommand<string>(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                filename = argFilename;
                Agents tempAgents = new Agents();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(Agents));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all the agents.
                    tempAgents = (Agents)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // We have to insert the agents in the existing collection.
                Clear();
                foreach (var agent in tempAgents)
                    Add(agent);
            }
        }

        ICommand _CloseAppCommand;
        public ICommand CloseAppCommand
        {
            get { return _CloseAppCommand ?? (_CloseAppCommand = new RelayCommand(CloseCommand_Execute)); }
        }

        private void CloseCommand_Execute()
        {
            Application.Current.MainWindow.Close();

            // Could use the line below instead
            // Application.Current.Shutdown();
        }

        #endregion // Commands

        #region Properties

        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
