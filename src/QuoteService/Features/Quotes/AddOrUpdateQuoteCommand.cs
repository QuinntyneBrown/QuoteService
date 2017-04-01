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
    public class AddOrUpdateQuoteCommand
    {
        public class AddOrUpdateQuoteRequest : IRequest<AddOrUpdateQuoteResponse>
        {
            public QuoteApiModel Quote { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateQuoteResponse { }

        public class AddOrUpdateQuoteHandler : IAsyncRequestHandler<AddOrUpdateQuoteRequest, AddOrUpdateQuoteResponse>
        {
            public AddOrUpdateQuoteHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateQuoteResponse> Handle(AddOrUpdateQuoteRequest request)
            {
                var entity = await _context.Quotes
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Quote.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Quotes.Add(entity = new Quote() { TenantId = tenant.Id });
                }

                entity.Name = request.Quote.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateQuoteResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
