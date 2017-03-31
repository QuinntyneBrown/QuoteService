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
    public class RemoveServiceCommand
    {
        public class RemoveServiceRequest : IRequest<RemoveServiceResponse>
        {
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class RemoveServiceResponse { }

        public class RemoveServiceHandler : IAsyncRequestHandler<RemoveServiceRequest, RemoveServiceResponse>
        {
            public RemoveServiceHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<RemoveServiceResponse> Handle(RemoveServiceRequest request)
            {
                var service = await _context.Services.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId);
                service.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveServiceResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
