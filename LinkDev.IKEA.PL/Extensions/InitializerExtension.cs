using LinkDev.IKEA.DAL.Contracts;
using System.Runtime.CompilerServices;

namespace LinkDev.IKEA.PL.Extensions
{
    public static  class InitializerExtension
    {
        public static void InitializeDataBase(this IApplicationBuilder app  ) {
            using var Scope = app.ApplicationServices.CreateScope();
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            dbInitializer.Initialize();
            dbInitializer.Seed();
        }
    }
}
