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
    public class AddOrUpdateLocationCommand
    {
        public class AddOrUpdateLocationRequest : IRequest<AddOrUpdateLocationResponse>
        {
            public LocationApiModel Location { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateLocationResponse { }

        public class AddOrUpdateLocationHandler : IAsyncRequestHandler<AddOrUpdateLocationRequest, AddOrUpdateLocationResponse>
        {
            public AddOrUpdateLocationHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateLocationResponse> Handle(AddOrUpdateLocationRequest request)
            {
                var entity = await _context.Locations
                    .Include(x => x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Location.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Locations.Add(entity = new Location() { TenantId = tenant.Id });
                }

                entity.Name = request.Location.Name;
                
                await _context.SaveChangesAsync();

                return new AddOrUpdateLocationResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
