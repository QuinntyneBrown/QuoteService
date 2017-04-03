using QuoteService.Data.Model;

namespace QuoteService.Features.Locations
{
    public class LocationApiModel
    {        
        public int Id { get; set; }

        public int? TenantId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string Province { get; set; }

        public string PostalCode { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        public bool IsMasterOrigin { get; set; }

        public static TModel FromLocation<TModel>(Location location) where
            TModel : LocationApiModel, new()
        {
            var model = new TModel();

            model.Id = location.Id;

            model.TenantId = location.TenantId;

            model.Name = location.Name;

            model.Address = location.Address;

            model.City = location.City;

            model.Province = location.Province;

            model.PostalCode = location.PostalCode;

            model.Longitude = location.Longitude;

            model.Latitude = location.Latitude;

            model.IsMasterOrigin = location.IsMasterOrigin;

            return model;
        }

        public static LocationApiModel FromLocation(Location location)
            => FromLocation<LocationApiModel>(location);

    }
}
