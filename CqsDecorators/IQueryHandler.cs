using System.Threading.Tasks;

namespace CqsDecorators
{
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<DataResult<TResult>> HanldeAsync(TQuery query);
    }
}
