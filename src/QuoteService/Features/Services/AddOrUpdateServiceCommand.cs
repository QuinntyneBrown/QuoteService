using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using System.Threading.Tasks;
using System.Data.Entity;
using System;

namespace QuoteService.Features.Services
{
    public class AddOrUpdateServiceCommand
    {
        public class AddOrUpdateServiceRequest : IRequest<AddOrUpdateServiceResponse>
        {
            public ServiceApiModel Service { get; set; }
            public Guid TenantUniqueId { get; set; }
        }

        public class AddOrUpdateServiceResponse { }

        public class AddOrUpdateServiceHandler : IAsyncRequestHandler<AddOrUpdateServiceRequest, AddOrUpdateServiceResponse>
        {
            public AddOrUpdateServiceHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<AddOrUpdateServiceResponse> Handle(AddOrUpdateServiceRequest request)
            {
                var entity = await _context.Services
                    .Include(x=>x.Tenant)
                    .SingleOrDefaultAsync(x => x.Id == request.Service.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                
                if (entity == null) {
                    var tenant = await _context.Tenants.SingleAsync(x => x.UniqueId == request.TenantUniqueId);
                    _context.Services.Add(entity = new Service() { TenantId = tenant.Id });
                }

                entity.Name = request.Service.Name;                
                entity.Rate = request.Service.Rate;
                entity.Description = request.Service.Description;
                entity.ImageUrl = request.Service.ImageUrl;

                await _context.SaveChangesAsync();

                return new AddOrUpdateServiceResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
