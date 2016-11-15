using AvaloniaApplication1.Model;
using AvaloniaApplication1.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModel
{
    class MainVM : INotifyPropertyChanged
    {
        ToDoItemWindow toDoWindow;
        private ToDoItem _selectedToDoItem;
        public ToDoItem SelectedToDoItem
        {
            get
            {
                return _selectedToDoItem;
            }
            set
            {
                if (_selectedToDoItem != value)
                {
                    _selectedToDoItem = value;
                    OnPropertyChanged("SelectedToDoItem");
                }
            }
        }
        private ObservableCollection<ToDoItem> _toDoItems;
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get
            {
                return _toDoItems;
            }
            set
            {
                if (_toDoItems != value)
                {
                    _toDoItems = value;
                    OnPropertyChanged("ToDoItems");
                }
            }
        }

        public MainVM()
        {
            LoadToDoItems();
        }

        private void LoadToDoItems()
        {
            DataService.LoadToDoItems((toDoItems, error) =>
            {
                if (error != null)
                {
                    System.Diagnostics.Debug.WriteLine(error.StackTrace);
                    return;
                }
                ToDoItems = toDoItems;
            });
            toDoWindow?.Close();
            toDoWindow = null;
        }

        private Command _openToDoItemWindowCommand;
        public Command OpenToDoItemWindowCommand
        {
            get
            {
                return _openToDoItemWindowCommand ?? (_openToDoItemWindowCommand = new Command(OpenToDoItemWindow));
            }
        }

        private Command _addToDoItemCommand;
        public Command AddToDoItemCommand
        {
            get
            {
                return _addToDoItemCommand ?? (_addToDoItemCommand = new Command(OpenToDoItemWindow));
            }
        }

        private void OpenToDoItemWindow(object obj)
        {
            ToDoItem item = obj as ToDoItem ?? new ToDoItem();
            toDoWindow = new ToDoItemWindow();
            toDoWindow.DataContext = new ToDoItemVM(item, LoadToDoItems);
            toDoWindow.ShowDialog();
        }

        private Command _deleteToDoItemCommand;
        public Command DeleteToDoItemCommand
        {
            get
            {
                return _deleteToDoItemCommand ?? (_deleteToDoItemCommand = new Command(DeleteToDoItem));
            }
        }

        private void DeleteToDoItem(object obj)
        {
            DataService.DeleteToDoItem((error) =>
            {
                if (error != null)
                {
                    System.Diagnostics.Debug.WriteLine(error.StackTrace);
                    return;
                }
            }, obj as ToDoItem);

            SelectedToDoItem = null;
            LoadToDoItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
