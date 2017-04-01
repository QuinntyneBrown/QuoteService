using System.Collections.Generic;

namespace QuoteService.Features.Quotes
{
    public class QuoteRequestApiModel
    {        
        public ICollection<ServiceQuoteRequestApiModel> ServiceQuoteRequests { get; set; }
    }
}
