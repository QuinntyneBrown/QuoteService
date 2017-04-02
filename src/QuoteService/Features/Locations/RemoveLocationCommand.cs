using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Locations
{
    public class RemoveLocationCommand
    {
        public class RemoveLocationRequest : IRequest<RemoveLocationResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; } 
        }

        public class RemoveLocationResponse { }

        public class RemoveLocationHandler : IAsyncRequestHandler<RemoveLocationRequest, RemoveLocationResponse>
        {
            public RemoveLocationHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveLocationResponse> Handle(RemoveLocationRequest request)
            {
                var location = await _context.Locations.SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                location.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveLocationResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
