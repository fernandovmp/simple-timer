using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimerXamarin.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(params string[] properties)
        {
            foreach (var property in properties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
