using System.Threading.Tasks;

namespace CqsDecorators
{
    public class TelemetryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> handler;

        public TelemetryHandler(IQueryHandler<TQuery, TResult> handler)
        {
            this.handler = handler;
        }

        public Task<DataResult<TResult>> HanldeAsync(TQuery query)
        {
            // Some action here
            return handler.HanldeAsync(query);
        }
    }
}
