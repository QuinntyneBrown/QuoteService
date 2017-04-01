using MediatR;
using QuoteService.Data;
using QuoteService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Quotes
{
    public class GetQuoteByIdQuery
    {
        public class GetQuoteByIdRequest : IRequest<GetQuoteByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetQuoteByIdResponse
        {
            public QuoteApiModel Quote { get; set; } 
        }

        public class GetQuoteByIdHandler : IAsyncRequestHandler<GetQuoteByIdRequest, GetQuoteByIdResponse>
        {
            public GetQuoteByIdHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetQuoteByIdResponse> Handle(GetQuoteByIdRequest request)
            {                
                return new GetQuoteByIdResponse()
                {
                    Quote = QuoteApiModel.FromQuote(await _context.Quotes
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
