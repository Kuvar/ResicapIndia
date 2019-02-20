namespace ResicapIndia.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;
    
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using ResicapIndia.Validations;
    using ResicapIndia.ViewModels.Base;
    using ResicapIndia.Models;
    using ResicapIndia.Services;

    public class UserListViewModel : ViewModelBase
    {
        private ObservableCollection<Item> _addressList;
        public ObservableCollection<Item> AddressList
        {
            get { return _addressList; }
            set
            {
                _addressList = value;
                RaisePropertyChanged(() => AddressList);
            }
        }

        public UserListViewModel()
        {
            Task.Run(async () => {
                MockDataStore db = new MockDataStore();
                AddressList = new ObservableCollection<Item>(await db.GetItemsAsync(true));
            });
        }

        public async Task<bool> AddAddressCommandAsync(Item item)
        {
            MockDataStore db = new MockDataStore();
            return await db.AddItemAsync(item);
        }
    }
}
