using Northwind.Domain.Exceptions;
using Northwind.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace Northwind.Domain.Tests.ValueObjects
{
    public class AdAccountTests
    {
        [Fact]
        public void ShouldHaveCorrectDomain()
        {
            var account = new AdAccount("SSW\\Jason");

            account.Domain.ShouldBe("SSW");
        }

        [Fact]
        public void ShouldHaveCorrectName()
        {
            var account = new AdAccount("SSW\\Jason");

            account.Name.ShouldBe("Jason");
        }

        [Fact]
        public void ImplicitConversionToStringReturnsDomainAndName()
        {
            const string value = "SSW\\Jason";

            var account = new AdAccount(value);

            string result = account;

            result.ShouldBe(value);
        }

        [Fact]
        public void ExplicitConversionFromStringSetsDomainAndName()
        {
            var account = (AdAccount) "SSW\\Jason";

            account.Domain.ShouldBe("SSW");
            account.Name.ShouldBe("Jason");
        }

        [Fact]
        public void ShouldThrowAdAccountInvalidExceptionForInvalidAdAccount()
        {
            Should.Throw<AdAccountInvalidException>(() => (AdAccount)"SSWJason");
        }
    }
}
