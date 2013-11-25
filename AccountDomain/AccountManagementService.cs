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

        public AccountManagementService(IAccount account)
        {
            _account = account;
        }

        public IAccount Account { get { return _account; } }

        public void UpdateShortName(string shortName)
        {
            _account.ShortName = shortName;
        }

        public void UpdateProductType(ProductType productType)
        {
            _account.ProductType = productType;
            if (productType == ProductType.Cpm)
            {
                _account.Benchmark = null;
            }
        }

        public void UpdateBenchmark(Benchmark benchmark)
        {
            _account.Benchmark = benchmark;
        }
    }
}
