using System.Data.Entity.Migrations;
using QuoteService.Data;
using QuoteService.Data.Model;
using QuoteService.Features.Users;

namespace QuoteService.Migrations
{
    public class RoleConfiguration
    {
        public static void Seed(QuoteServiceContext context) {

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.SYSTEM
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.PRODUCT
            });

            context.Roles.AddOrUpdate(x => x.Name, new Role()
            {
                Name = Roles.DEVELOPMENT
            });

            context.SaveChanges();
        }
    }
}
