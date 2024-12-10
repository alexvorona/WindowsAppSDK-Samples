using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System;
using CommunityToolkit.Mvvm.Input;
using PhotoEditor.Models;

namespace PhotoEditor.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ImageFileInfo> Images { get; } = new ObservableCollection<ImageFileInfo>();

        private ImageFileInfo _persistedItem;
        public ImageFileInfo PersistedItem
        {
            get => _persistedItem;
            set => SetProperty(ref _persistedItem, value);
        }

        private double _itemSize;
        public double ItemSize
        {
            get => _itemSize;
            set => SetProperty(ref _itemSize, value);
        }

        public RelayCommand<ItemClickEventArgs> NavigateToDetailPageCommand { get; }

        public MainPageViewModel()
        {
           // NavigateToDetailPageCommand = new RelayCommand<ItemClickEventArgs>(NavigateToDetailPage);
        }


        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
