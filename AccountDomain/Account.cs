using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AccountDomain.Annotations;

namespace AccountDomain
{
    public interface IAccount : INotifyPropertyChanged
    {
        string ShortName { get; set; }
        Benchmark Benchmark { get; set; }
        ProductType ProductType { get; set; }
    }

    public class Account: IAccount
    {
        private string _shortName;
        private ProductType _productType;
        private Benchmark _benchmark;

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

        public Benchmark Benchmark
        {
            get { return _benchmark; }
            set
            {
                if (Equals(value, _benchmark)) return;
                _benchmark = value;
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
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public enum ProductType
    {
        Tmc = 1,
        Cpm = 2
    }
}
