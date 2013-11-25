using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountDomain
{
    public class AccountManagementService
    {
        private readonly IAccount _account;
        private readonly Dictionary<string, string> _errors; 

        public event ValidationChangedEventHandler ValidationChanged;
        public delegate void ValidationChangedEventHandler(object sender, ValidationChangedEventArgs e);

        public AccountManagementService(IAccount account)
        {
            _account = account;
            _errors = new Dictionary<string, string>();
        }

        public IAccount Account { get { return _account; } }

        public void UpdateShortName(string shortName)
        {
            _account.ShortName = shortName;
        }

        public string GetErrorText(string propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }

        public void UpdateProductType(ProductType productType)
        {
            _account.ProductType = productType;
            if (productType == ProductType.Cpm)
            {
                _account.Benchmark = null;
                _errors.Remove("Benchmark");
                if (ValidationChanged != null)
                    ValidationChanged(this, new ValidationChangedEventArgs { PropertyName = "Benchmark" });
            }
            if (productType == ProductType.Tmc)
            {
                if(!_errors.ContainsKey("Benchmark"))
                    _errors.Add("Benchmark","If you are TMC then you gotta have a benchmark");
                if (ValidationChanged != null)
                    ValidationChanged(this, new ValidationChangedEventArgs {PropertyName = "Benchmark"});
            }
        }

        public void UpdateBenchmark(Benchmark benchmark)
        {
            _account.Benchmark = benchmark;
        }
    }
}
