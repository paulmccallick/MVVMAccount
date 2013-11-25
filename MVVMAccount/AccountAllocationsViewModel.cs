using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountDomain;
using AccountDomain.Annotations;

namespace MVVMAccount
{
    public class AccountAllocationsViewModel : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly AccountManagementService _accountManagement;
        private Benchmark _benchmark;
        public event PropertyChangedEventHandler PropertyChanged;

        public AccountAllocationsViewModel(AccountManagementService accountManagementService)
        {
            _accountManagement = accountManagementService;
            _accountManagement.Account.PropertyChanged += (o, e) => SyncToAccount();
            _accountManagement.ValidationChanged += (o, e) => HandleValidation(e.PropertyName);
        }

        private void HandleValidation(string propertyName)
        {
            if (propertyName == "Benchmark")
            {
                if (PropertyChanged != null) PropertyChanged(this,new PropertyChangedEventArgs("Benchmark"));
            }
        }

        public Benchmark Benchmark
        {
            get { return _benchmark; }
            set
            {
                if (Equals(value, _benchmark)) return;
                _benchmark = value;
                _accountManagement.UpdateBenchmark(value);
                OnPropertyChanged();
            }
        }

        //TODO:this should be handled by a provider
        public IEnumerable<ComboBoxItem<Benchmark>> Benchmarks
        {
            get { 
                var benchmarks = new[] {new Benchmark {ShortName = "ru1000"}, new Benchmark {ShortName = "ru2000"}};
                return benchmarks.Select(b => new ComboBoxItem<Benchmark> {DisplayValue = b.ShortName, ItemValue = b});
            }
        } 

        private void SyncToAccount()
        {
            Benchmark = _accountManagement.Account.Benchmark;
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public string this[string columnName]
        {
            get { return _accountManagement.GetErrorText(columnName); }
        }

        public string Error { get; private set; }
    }
}
