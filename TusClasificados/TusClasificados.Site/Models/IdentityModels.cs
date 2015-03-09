using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TusClasificados.Site.Migrations;
using System.Web;

using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;

namespace TusClasificados.Site.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            //Database.SetInitializer<ApplicationDbContext>(new DbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public static ApplicationDbContext GetDBContext()
        {
            return HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
        }

        public DbSet<Anuncio> Anuncios { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
    }

    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            Array values = Enum.GetValues(typeof(TipoCuenta));
            var random = new Random();

            var users = new List<ApplicationUser>();

            for(int index = 1; index <= 10; index++)
            {
                string password = "prueba";
                string email = String.Format("test{0}@example.com", index);
                TipoCuenta tipo = (TipoCuenta)values.GetValue(random.Next(values.Length));

                IdentityResult result = userManager.Create(new ApplicationUser { UserName = email, Email = email, Nombre = "Test", Apellido = "Subject", TipoCuenta = tipo }, password);
                var user = userManager.FindByName(email);
                users.Add(user);
            }

            for (int index = 1; index <= 10; index++)
            {
                var anuncio = new Anuncio()
                {
                    Titulo = string.Format("Anuncio {0}", index),
                    NumeroTelefono = (index * 100000).ToString(),
                    Anunciante = users[random.Next(users.Count)],
                    Detalles = "In ac felis quis tortor malesuada pretium. Sed augue ipsum, egestas nec, vestibulum et, malesuada adipiscing, dui. Etiam feugiat lorem non metus. Vivamus laoreet. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Cura.",
                    ExtraDetalles = "Twitter: @ImCarlosEME"
                };

                context.Anuncios.Add(anuncio);
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}