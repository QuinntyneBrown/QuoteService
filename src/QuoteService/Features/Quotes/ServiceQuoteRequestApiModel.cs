using QuoteService.Data.Model;
using QuoteService.Features.Geolocation;
using QuoteService.Features.Services;
using System;

namespace QuoteService.Features.Quotes
{
    public class ServiceQuoteRequestApiModel
    {
        public LocationApiModel Location { get; set; }
        public DateTime? DateTime { get; set; }
        public float DurationInHours { get; set; }
        public ServiceApiModel Service { get; set; }
    }
}
