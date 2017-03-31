using System.Data.Entity.Migrations;
using QuoteService.Data;
using QuoteService.Data.Model;

namespace QuoteService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(QuoteServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default"
            });

            context.SaveChanges();
        }
    }
}
