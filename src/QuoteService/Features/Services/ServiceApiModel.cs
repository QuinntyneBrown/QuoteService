using QuoteService.Data.Model;

namespace QuoteService.Features.Services
{
    public class ServiceApiModel
    {        
        public int Id { get; set; }
        public int? TenantId { get; set; }
        public string Name { get; set; }

        public static TModel FromService<TModel>(Service service) where
            TModel : ServiceApiModel, new()
        {
            var model = new TModel();
            model.Id = service.Id;
            model.TenantId = service.TenantId;
            model.Name = service.Name;
            return model;
        }

        public static ServiceApiModel FromService(Service service)
            => FromService<ServiceApiModel>(service);

    }
}
