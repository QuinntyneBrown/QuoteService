using QuoteService.Data.Model;

namespace QuoteService.Features.Geolocation
{
    public class LocationApiModel
    {        
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostalCode { get; set; }
    }
}
