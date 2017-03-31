using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
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
            public int? TenantId { get; set; }
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
                    .SingleOrDefaultAsync(x => x.Id == request.Quote.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Quotes.Add(entity = new Quote());
                entity.Name = request.Quote.Name;
                entity.TenantId = request.TenantId;

                await _context.SaveChangesAsync();

                return new AddOrUpdateQuoteResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
