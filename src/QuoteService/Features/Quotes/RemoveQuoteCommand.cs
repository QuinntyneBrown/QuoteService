using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Quotes
{
    public class RemoveQuoteCommand
    {
        public class RemoveQuoteRequest : IRequest<RemoveQuoteResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveQuoteResponse { }

        public class RemoveQuoteHandler : IAsyncRequestHandler<RemoveQuoteRequest, RemoveQuoteResponse>
        {
            public RemoveQuoteHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveQuoteResponse> Handle(RemoveQuoteRequest request)
            {
                var quote = await _context.Quotes.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                quote.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveQuoteResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
