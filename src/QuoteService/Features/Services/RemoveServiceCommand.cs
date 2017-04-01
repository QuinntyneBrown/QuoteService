using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;
using System;

namespace QuoteService.Features.Services
{
    public class RemoveServiceCommand
    {
        public class RemoveServiceRequest : IRequest<RemoveServiceResponse>
        {
            public int Id { get; set; }
            public Guid TenantUniqueId { get; set; }
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
                var service = await _context.Services
                    .Include(x=>x.Tenant)
                    .SingleAsync(x=>x.Id == request.Id && x.Tenant.UniqueId == request.TenantUniqueId);
                service.IsDeleted = true;
                await _context.SaveChangesAsync();
                return new RemoveServiceResponse();
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
