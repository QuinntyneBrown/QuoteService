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
    public class GetLocationByIdQuery
    {
        public class GetLocationByIdRequest : IRequest<GetLocationByIdResponse> { 
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class GetLocationByIdResponse
        {
            public LocationApiModel Location { get; set; } 
        }

        public class GetLocationByIdHandler : IAsyncRequestHandler<GetLocationByIdRequest, GetLocationByIdResponse>
        {
            public GetLocationByIdHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetLocationByIdResponse> Handle(GetLocationByIdRequest request)
            {                
                return new GetLocationByIdResponse()
                {
                    Location = LocationApiModel.FromLocation(await _context.Locations
                    .Include(x => x.Tenant)				
					.SingleAsync(x=>x.Id == request.Id &&  x.Tenant.UniqueId == request.TenantUniqueId))
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
