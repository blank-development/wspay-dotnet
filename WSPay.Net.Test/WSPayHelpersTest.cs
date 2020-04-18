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

        [Fact]
        public void IsActionSuccessful()
        {
            var response = @"
                <input type=""hidden"" name=""ActionSuccess"" value=""1"">
                <input type=""hidden"" name=""ActionError"" value=""0"">
            ";
            
            var actual = WSPayHelpers.IsActionSuccessful(response);
            actual.Should().BeTrue();
        }
        
        [Fact]
        public void IsActionSuccessful_Error()
        {
            var response = @"
                <input type=""hidden"" name=""ActionSuccess"" value=""0"">
                <input type=""hidden"" name=""ActionError"" value=""0"">
            ";
            
            var actual = WSPayHelpers.IsActionSuccessful(response);
            actual.Should().BeFalse();
        }

        [Fact]
        public void GetErrorMessageFromResponseString()
        {
            var response = @"
                <input type=""hidden"" name=""ActionSuccess"" value=""0"">
                <input type=""hidden"" name=""ErrorMessage"" value=""Došlo je do pogreške"">
            ";
            
            var actual = WSPayHelpers.GetErrorMessageFromResponseString(response);
            actual.Should().Be("Došlo je do pogreške");
        }
    }
}