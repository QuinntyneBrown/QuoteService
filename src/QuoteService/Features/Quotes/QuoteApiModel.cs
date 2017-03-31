using QuoteService.Data.Model;
using System;

namespace QuoteService.Features.Quotes
{
    public class QuoteApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }
        public int? ServiceId { get; set; }
        public string Address { get; set; }
        public double Hours { get; set; }
        public DateTime? DateTime { get; set; }

        public static TModel FromQuote<TModel>(Quote quote) where
            TModel : QuoteApiModel, new()
        {
            var model = new TModel();
            model.Id = quote.Id;
            model.TenantId = quote.TenantId;
            model.Name = quote.Name;
            return model;
        }

        public static QuoteApiModel FromQuote(Quote quote)
            => FromQuote<QuoteApiModel>(quote);

    }
}
