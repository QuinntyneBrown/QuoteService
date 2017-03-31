using MediatR;
using QuoteService.Data;
using System.Threading.Tasks;
using QuoteService.Features.Core;

namespace QuoteService.Features.DigitalAssets
{
    public class GetDigitalAssetByIdQuery
    {
        public class GetDigitalAssetByIdRequest : IRequest<GetDigitalAssetByIdResponse> { 
			public int Id { get; set; }
		}

        public class GetDigitalAssetByIdResponse
        {
            public DigitalAssetApiModel DigitalAsset { get; set; } 
		}

        public class GetDigitalAssetByIdHandler : IAsyncRequestHandler<GetDigitalAssetByIdRequest, GetDigitalAssetByIdResponse>
        {
            public GetDigitalAssetByIdHandler(IQuoteServiceContext context, ICache cache)
            {
                _context = context;
                _cache = cache;
            }

            public async Task<GetDigitalAssetByIdResponse> Handle(GetDigitalAssetByIdRequest request)
            {                
                return new GetDigitalAssetByIdResponse()
                {
                    DigitalAsset = DigitalAssetApiModel.FromDigitalAsset(await _context.DigitalAssets.FindAsync(request.Id))
                };
            }

            private readonly IQuoteServiceContext _context;
            private readonly ICache _cache;
        }
    }
}
