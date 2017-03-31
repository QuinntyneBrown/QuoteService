using QuoteService.Data.Model;

namespace QuoteService.Features.Quotes
{
    public class QuoteApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

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
