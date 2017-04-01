using System;

namespace QuoteService.Features.Quotes
{
    public class ServiceQuoteRequestApiModel
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; } = "Ontario";
        public DateTime? DateTime { get; set; }
        public float DurationInHours { get; set; }
        public int? ServiceId { get; set; }        
    }
}
