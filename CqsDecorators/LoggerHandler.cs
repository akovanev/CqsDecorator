using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

namespace CqsDecorators
{
    public class LoggerHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> handler;
        private readonly ILogger logger;

        public LoggerHandler(IQueryHandler<TQuery, TResult> handler, ILogger<TQuery> logger)
        {
            this.handler = handler;
            this.logger = logger;
        }

        public async Task<DataResult<TResult>> HanldeAsync(TQuery query)
        {
            var result = await handler.HanldeAsync(query);
            logger.LogInformation(result.Data.ToString());
            return result;
        }
    }
}
