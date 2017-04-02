using MediatR;
using QuoteService.Data;
using QuoteService.Features.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Locations
{
    public class GetLocationsQuery
    {
        public class GetLocationsRequest : IRequest<GetLocationsResponse> { 
            public Guid TenantUniqueId { get; set; }       
        }

        public class GetLocationsResponse
        {
            public ICollection<LocationApiModel> Locations { get; set; } = new HashSet<LocationApiModel>();
        }

        public class GetLocationsHandler : IAsyncRequestHandler<GetLocationsRequest, GetLocationsResponse>
        {
            public GetLocationsHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetLocationsResponse> Handle(GetLocationsRequest request)
            {
                var locations = await _context.Locations
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetLocationsResponse()
                {
                    Locations = locations.Select(x => LocationApiModel.FromLocation(x)).ToList()
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
