using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountDomain;
using NSubstitute;
using NUnit.Framework;

namespace MVVMAccount.Test
{
    [TestFixture]
    public class MainvWindowViewModelTest
    {
        [Test]
        public void WhenShortNameIsChangedPropertyNotifyEventIsRaised()
        {
            var account = Substitute.For<IAccount>();
            var vm = new MVVMAccount.MainWindowViewModel(account);
            string propertyName = "notset";
            vm.PropertyChanged += (o, e) => { propertyName = e.PropertyName; };
            vm.ShortName = "X";
            Assert.That(propertyName,Is.EqualTo("ShortName"));
        }

        [Test]
        public void WhenAccountChangesPropertiesAreSynced()
        {
            var account = Substitute.For<IAccount>();
            account.ShortName = "X";

            var vm = new MainWindowViewModel(account);
            Assert.That(vm.ShortName,Is.EqualTo("X"));
            account.ShortName = "Y";
            account.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(this,
                                                                                new PropertyChangedEventArgs("antyhing"));
            Assert.That(vm.ShortName,Is.EqualTo("Y"));

        }


    }
}
