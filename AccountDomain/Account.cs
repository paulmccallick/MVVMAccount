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

        public Benchmark Benchmark { get; set; }
        public ProductType ProductType { get; set; }
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
