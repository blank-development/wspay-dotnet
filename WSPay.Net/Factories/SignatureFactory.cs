namespace WSPay.Net
{
    using System.Security.Cryptography;
    using System.Text;
    
    public class SignatureFactory : ISignatureFactory
    {
        public string GenerateFormRequestSignature(Shop shop, string shoppingCartId, double price)
        {
            var formattedPrice = WSPayHelpers.FormatPrice(price);
            using (var sha512Hash = SHA512.Create())
            {
                var data = $"{shop.ShopId}{shop.Secret}{shoppingCartId}{shop.Secret}{formattedPrice}{shop.Secret}";
                var hashBytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                var sBuilder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public string GenerateChangeTransactionStatusSignature(Shop shop, string wsPayOrderId, string stan, string approvalCode, double price)
        {
            var formattedPrice = WSPayHelpers.FormatPrice(price);

            using (var md5Hash = MD5.Create())
            {
                var data = $"{shop.ShopId}{wsPayOrderId}{shop.Secret}{stan}{shop.Secret}{approvalCode}{shop.Secret}{formattedPrice}{shop.Secret}{wsPayOrderId}";
                var hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                var sBuilder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
       
        public string GenerateTransactionStatusCheckSignature(Shop shop, string shoppingCartId)
        {
            using (var sha512Hash = SHA512.Create())
            {
                var data = $"{shop.ShopId}{shop.Secret}{shoppingCartId}{shop.Secret}{shop.ShopId}{shoppingCartId}";
                var hashBytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                var sBuilder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}
