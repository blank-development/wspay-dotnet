namespace WSPay.Net
{
    public interface ISignatureFactory
    {
        string GenerateFormRequestSignature(Shop shop, string shoppingCartId, double price);

        string GenerateChangeTransactionStatusSignature(Shop shop, string wsPayOrderId, string stan,
            string approvalCode, double price);

        string GenerateTransactionStatusCheckSignature(Shop shop, string shoppingCartId);
    }
}