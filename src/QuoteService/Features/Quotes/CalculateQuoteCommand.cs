using MediatR;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Core;
using QuoteService.Features.Geolocation;
using System.Threading.Tasks;
using System.Data.Entity;
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
            public Guid TenantUniqueId { get; set; }
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
                var originFullAddress = "628 Fleet Street, Toronto, Ontario";

                var quote = new Quote();

                var quoteLineItem = new QuoteLineItem();

                var serviceQuoteRequest = request.QuoteRequest.ServiceQuoteRequest;

                var service = await _context.Services
                    .Include(x => x.Tenant)
                    .SingleAsync(x => x.Id == serviceQuoteRequest.ServiceId && x.Tenant.UniqueId == request.TenantUniqueId);

                var distance = await _mediator.Send(new GetDistanceQuery.GetDistanceRequest() { Address1 = originFullAddress, Address2 = $"{serviceQuoteRequest.Address},{serviceQuoteRequest.City},{serviceQuoteRequest.Province}" });

                float distanceKm = float.Parse(distance.Distance, CultureInfo.InvariantCulture.NumberFormat) / 1000;

                quoteLineItem.Amount = (service.Rate * serviceQuoteRequest.DurationInHours) + (distanceKm * (float)3.5);

                quoteLineItem.Description = $"{service.Name}: {serviceQuoteRequest.Address},{serviceQuoteRequest.City},{serviceQuoteRequest.Province}-{serviceQuoteRequest.DurationInHours} Hours";

                quote.QuoteLineItems.Add(quoteLineItem);

                originFullAddress = $"{serviceQuoteRequest.Address},{serviceQuoteRequest.City},{serviceQuoteRequest.Province}";

                return new CalculateQuoteResponse()
                {
                    Quote = QuoteApiModel.FromQuote(quote)
                };
            
            }

            private readonly QuoteServiceContext _context;
            private readonly ICache _cache;
            private readonly IMediator _mediator;
            private readonly IQuoteConfiguration _configuration;
        }

    }

}
