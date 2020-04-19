using System.Threading.Tasks;

namespace WSPay.Net.Test
{
    public static class TaskExtensions
    {
        public static T WaitTask<T>(this Task<T> task)
        {
            return task.GetAwaiter().GetResult();
        }
    }
}