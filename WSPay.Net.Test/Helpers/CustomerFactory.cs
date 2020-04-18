namespace WSPay.Net.Test
{
    public class CustomerFactory
    {
        public static Customer Create()
        {
            return new Customer()
            {
                FirstName = "Test",
                LastName = "Testinovic",
                Email = "test@test.com",
                Address = "Test address",
                Phone = "123456"
            };
        }
    }
}