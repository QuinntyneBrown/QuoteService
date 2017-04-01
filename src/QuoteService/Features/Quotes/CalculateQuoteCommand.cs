using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using QuoteService.Features.Geolocation;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Linq;
using System;
using System.Globalization;

namespace QuoteService.Features.Quotes
{
    public class CalculateQuoteCommand
    {
        public class CalculateQuoteRequest : IRequest<CalculateQuoteResponse>
        {
            public int? TenantId { get; set; }
            public QuoteRequestApiModel QuoteRequest { get; set; }
        }

        public class CalculateQuoteResponse
        {
            public QuoteApiModel Quote { get; set; }
        }

        public class CalculateQuoteHandler : IAsyncRequestHandler<CalculateQuoteRequest, CalculateQuoteResponse>
        {
            public CalculateQuoteHandler(
                QuoteServiceContext context, 
                ICache cache, 
                IMediator mediator,
                IQuoteConfiguration configuration)
            {
                _context = context;
                _cache = cache;
                _mediator = mediator;
                _configuration = configuration;
            }

            public async Task<CalculateQuoteResponse> Handle(CalculateQuoteRequest request)
            {
                var origin = _configuration.Origin;
                var quote = new Quote();
                
                foreach (var serviceQuoteRequest in request.QuoteRequest.ServiceQuoteRequests)
                {
                    var quoteLineItem = new QuoteLineItem();

                    var service = await _context.Services.SingleAsync(x => x.Id == serviceQuoteRequest.Service.Id);

                    var distance = await _mediator.Send(new GetDistanceQuery.GetDistanceRequest() { Address1 = origin, Address2 = serviceQuoteRequest.Location.StreetAddress });
                    
                    quoteLineItem.Amount = (service.Rate * serviceQuoteRequest.DurationInHours) + (float.Parse(distance.Distance, CultureInfo.InvariantCulture.NumberFormat) * (float)3.5);

                    quoteLineItem.Description = $"{service.Name}: {serviceQuoteRequest.Location.StreetAddress}-{serviceQuoteRequest.DurationInHours}";

                    quote.QuoteLineItems.Add(quoteLineItem);
                    
                    origin = serviceQuoteRequest.Location.StreetAddress;
                }

                return new CalculateQuoteResponse()
                {
                    Quote = null
                };
            
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
            private readonly IMediator _mediator;
            private readonly IQuoteConfiguration _configuration;
        }

    }

}
