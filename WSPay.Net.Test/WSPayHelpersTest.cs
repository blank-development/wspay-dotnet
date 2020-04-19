namespace WSPay.Net.Test
{
    using FluentAssertions;
    using Xunit;
    
    public class WSPayHelpersTest
    {
        [Fact]
        public void FormatPrice()
        {
            var actual = WSPayHelpers.FormatPrice(15.25);
            actual.Should().Be("1525");
        }
        
        [Fact]
        public void FormatPrice_WithRounding()
        {
            var actual = WSPayHelpers.FormatPrice(15.257);
            actual.Should().Be("1526");
        }
        
        [Fact]
        public void FormatAmountForRegularShopForm()
        {
            var actual = WSPayHelpers.FormatAmountForRegularShopForm(15.25);
            actual.Should().Be("15,25");
        }
        
        [Fact]
        public void FormatAmountForRegularShopForm_WithRounding()
        {
            var actual = WSPayHelpers.FormatAmountForRegularShopForm(15.257);
            actual.Should().Be("15,26");
        }
        
        [Fact]
        public void FormatAmountForRegularShopForm_Rounded()
        {
            var actual = WSPayHelpers.FormatAmountForRegularShopForm(15);
            actual.Should().Be("15,00");
        }
    }
}