using System.Threading.Tasks;

namespace CqsDecorators
{
    public class MainHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
        where TQuery : DataQuery
    {
        public Task<DataResult<TResult>> HanldeAsync(TQuery query)
        {
            // Just an example
            return Task.FromResult(new DataResult<TResult> { Data = (TResult)(object) query.Page.ToString() });
        }
    }
}
