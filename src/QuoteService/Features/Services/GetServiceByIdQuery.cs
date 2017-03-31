using MediatR;
using QuoteService.Data;
using QuoteService.Features.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

namespace QuoteService.Features.Services
{
    public class GetServiceByIdQuery
    {
        public class GetServiceByIdRequest : IRequest<GetServiceByIdResponse> { 
            public int Id { get; set; }
            public int? TenantId { get; set; }
        }

        public class GetServiceByIdResponse
        {
            public ServiceApiModel Service { get; set; } 
        }

        public class GetServiceByIdHandler : IAsyncRequestHandler<GetServiceByIdRequest, GetServiceByIdResponse>
        {
            public GetServiceByIdHandler(QuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetServiceByIdResponse> Handle(GetServiceByIdRequest request)
            {                
                return new GetServiceByIdResponse()
                {
                    Service = ServiceApiModel.FromService(await _context.Services.SingleAsync(x=>x.Id == request.Id && x.TenantId == request.TenantId))
                };
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
        }

    }

}
