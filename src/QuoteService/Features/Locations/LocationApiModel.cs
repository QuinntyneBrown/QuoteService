using QuoteService.Data.Model;

namespace QuoteService.Features.Locations
{
    public class LocationApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromLocation<TModel>(Location location) where
            TModel : LocationApiModel, new()
        {
            var model = new TModel();
            model.Id = location.Id;
            model.TenantId = location.TenantId;
            model.Name = location.Name;
            return model;
        }

        public static LocationApiModel FromLocation(Location location)
            => FromLocation<LocationApiModel>(location);

    }
}
