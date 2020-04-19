namespace WSPay.Net
{
    using System.Threading.Tasks;

    public interface IWSPayClient
    {
        TRes Request<TReq, TRes>(TReq request, string service)
            where TReq : class
            where TRes : class;

        Task<TRes> RequestAsync<TReq, TRes>(TReq request, string service)
            where TReq : class
            where TRes : class;
    }
}