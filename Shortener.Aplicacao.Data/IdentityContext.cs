using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shortener.Aplicacao.Models.Usuarios;

namespace Shortener.Aplicacao.Data
{
    public class IdentityContext : IdentityDbContext
    {
        public IdentityContext(DbContextOptions options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}