using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountDomain;
using MVVMAccount.Annotations;

namespace MVVMAccount
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _shortName;
        private readonly IAccount _account;
        public AccountPropertiesViewModel AccountPropertiesViewModel { get; set; }
        public AccountAllocationsViewModel AccountAllocationsViewModel { get; set; }

        public MainWindowViewModel(IAccount account)
        {
            _account = account;
            var accountManagementService = new AccountManagementService(account);
            SyncToAccount();
            _account.PropertyChanged += (o, e) => SyncToAccount();
            AccountPropertiesViewModel = new AccountPropertiesViewModel(accountManagementService);
            AccountAllocationsViewModel = new AccountAllocationsViewModel(accountManagementService);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void SyncToAccount()
        {
            ShortName = _account.ShortName;
        }

        public string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value == _shortName) return;
                _shortName = value;
                OnPropertyChanged();
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
