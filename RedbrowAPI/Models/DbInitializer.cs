using System;

namespace RedbrowAPI.Models
{
    public class DbInitializer
    {
        public static void Initialize(RedbrowDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Usuario.Any())
            {
                return;
            }

            var usuario = new Usuario
            {
                Nombre = "Percy Cornelio",
                Correo = "pcornelio@redbrow.io",
                Password = "123"
            };

            context.Usuario.Add(usuario);
            context.SaveChanges();
        }
    }
}
