using AvaloniaApplication1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaApplication1.ViewModel
{
    class ToDoItemVM : INotifyPropertyChanged
    {
        private Action updateToDoItemListBox;
        private ToDoItem _toDoItem;
        public ToDoItem ToDoItem
        {
            get
            {
                return _toDoItem;
            }
            set
            {
                if (_toDoItem != value)
                {
                    _toDoItem = value;
                    OnPropertyChanged("SelectedToDoItem");
                }
            }
        }

        public ToDoItemVM(ToDoItem toDoItem, Action loadToDoItems)
        {
            updateToDoItemListBox = loadToDoItems;
            ToDoItem = toDoItem;
        }

        private Command _saveToDoItemCommand;
        public Command SaveToDoItemCommand
        {
            get
            {
                return _saveToDoItemCommand ?? (_saveToDoItemCommand = new Command(AddOrUpdateToDoItem));
            }
        }

        private void AddOrUpdateToDoItem(object obj)
        {
            DataService.AddOrUpdateToDoItem((error) =>
            {
                if (error != null)
                {
                    System.Diagnostics.Debug.WriteLine(error.StackTrace);
                    return;
                }
            }, ToDoItem as ToDoItem);
            updateToDoItemListBox();
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
