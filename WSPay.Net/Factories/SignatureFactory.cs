namespace WSPay.Net
{
    using System.Security.Cryptography;
    using System.Text;
    
    public class SignatureFactory
    {
        public string GenerateFormRequestSignature(Shop shop, string shoppingCartId, string formattedPrice)
        {
            using (var md5Hash = MD5.Create())
            {
                var data = $"{shop.ShopId}{shop.Secret}{shoppingCartId}{shop.Secret}{formattedPrice}{shop.Secret}";
                var hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

                var sBuilder = new StringBuilder();

                foreach (var b in hashBytes)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public string GenerateTransactionRequestSignature(Shop shop, string wsPayOrderId, string stan, string approvalCode, string formattedPrice)
        {
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
            using (var md5Hash = MD5.Create())
            {
                var data = $"{shop.ShopId}{shop.Secret}{shoppingCartId}{shop.Secret}{shop.ShopId}{shoppingCartId}";
                var hashBytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(data));

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
