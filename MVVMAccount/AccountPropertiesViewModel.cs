using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountDomain;
using MVVMAccount.Annotations;

namespace MVVMAccount
{
    public class AccountPropertiesViewModel:INotifyPropertyChanged
    {
        private string _shortName;
        private readonly IAccount _account;
        public event PropertyChangedEventHandler PropertyChanged;

        public AccountPropertiesViewModel(IAccount account)
        {
            _account = account;
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
