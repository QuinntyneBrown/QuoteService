using MediatR;
using QuoteService.Data;
using QuoteService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Quotes
{
    public class GetQuotesQuery
    {
        public class GetQuotesRequest : IRequest<GetQuotesResponse> { 
            public int? TenantId { get; set; }        
        }

        public class GetQuotesResponse
        {
            public ICollection<QuoteApiModel> Quotes { get; set; } = new HashSet<QuoteApiModel>();
        }

        public class GetQuotesHandler : IAsyncRequestHandler<GetQuotesRequest, GetQuotesResponse>
        {
            public GetQuotesHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuotesResponse> Handle(GetQuotesRequest request)
            {
                var quotes = await _context.Quotes
                    .Where( x => x.TenantId == request.TenantId )
                    .ToListAsync();

                return new GetQuotesResponse()
                {
                    Quotes = quotes.Select(x => QuoteApiModel.FromQuote(x)).ToList()
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
