using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Services
{
    public class AddOrUpdateServiceCommand
    {
        public class AddOrUpdateServiceRequest : IRequest<AddOrUpdateServiceResponse>
        {
            public ServiceApiModel Service { get; set; }
            public int? TenantId { get; set; }
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
                    .SingleOrDefaultAsync(x => x.Id == request.Service.Id && x.TenantId == request.TenantId);
                if (entity == null) _context.Services.Add(entity = new Service());
                entity.Name = request.Service.Name;
                entity.TenantId = request.TenantId;
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
