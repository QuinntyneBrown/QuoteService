using MediatR;
using QuoteService.Data;
using QuoteService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System;

namespace QuoteService.Features.Services
{
    public class GetServicesQuery
    {
        public class GetServicesRequest : IRequest<GetServicesResponse> { 
            public Guid TenantUniqueId { get; set; }        
        }

        public class GetServicesResponse
        {
            public ICollection<ServiceApiModel> Services { get; set; } = new HashSet<ServiceApiModel>();
        }

        public class GetServicesHandler : IAsyncRequestHandler<GetServicesRequest, GetServicesResponse>
        {
            public GetServicesHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetServicesResponse> Handle(GetServicesRequest request)
            {
                var services = await _context.Services
                    .Include(x => x.Tenant)
                    .Where(x => x.Tenant.UniqueId == request.TenantUniqueId )
                    .ToListAsync();

                return new GetServicesResponse()
                {
                    Services = services.Select(x => ServiceApiModel.FromService(x)).ToList()
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
