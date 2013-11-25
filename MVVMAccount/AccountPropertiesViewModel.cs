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
    public class ComboBoxItem<T>
    {
        public string DisplayValue { get; set; }
        public T ItemValue { get; set; }
    }

    public class AccountPropertiesViewModel:INotifyPropertyChanged
    {
        private string _shortName;
        private ProductType _productType;
        private readonly AccountManagementService _accountManagement; 
        public event PropertyChangedEventHandler PropertyChanged;

        public AccountPropertiesViewModel(AccountManagementService accountManagementService)
        {
            _accountManagement = accountManagementService;
            _accountManagement.Account.PropertyChanged += (o, e) => SyncToAccount();
        }
        private void SyncToAccount()
        {
            ShortName = _accountManagement.Account.ShortName;
            ProductType = _accountManagement.Account.ProductType;
        }

        public IEnumerable<ComboBoxItem<ProductType>>  ProductTypes
        {
            //TODO: This really should be provided by a provider or something.
            get
            {
                return
                    Enum.GetValues(typeof (ProductType))
                        .Cast<ProductType>()
                        .Select(pt => new ComboBoxItem<ProductType> {DisplayValue = pt.ToString(), ItemValue = pt});
            }
        }



        public string ShortName
        {
            get { return _shortName; }
            set
            {
                if (value == _shortName) return;
                _shortName = value;
                _accountManagement.UpdateShortName(value);
                OnPropertyChanged();
            }
        }

        public ProductType ProductType
        {
            get { return _productType; }
            set
            {
                if (value == _productType) return;
                _productType = value;
                _accountManagement.UpdateProductType(value);
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
