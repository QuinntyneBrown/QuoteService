using System.Data.Entity.Migrations;
using QuoteService.Data;
using QuoteService.Data.Model;
using System;

namespace QuoteService.Migrations
{
    public class TenantConfiguration
    {
        public static void Seed(QuoteServiceContext context) {

            context.Tenants.AddOrUpdate(x => x.Name, new Tenant()
            {
                Name = "Default",
                UniqueId = new Guid("50848e1d-f3ec-486a-b25c-7f6cf1ef7c93")
            });

            context.SaveChanges();
        }
    }
}
