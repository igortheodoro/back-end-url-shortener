using Microsoft.EntityFrameworkCore;
using Shortener.Aplicacao.Models.Link;
using Toolbelt.ComponentModel.DataAnnotations;

namespace Shortener.Aplicacao.Data
{
    public class AplicacaoContext:DbContext
    {
        public AplicacaoContext(DbContextOptions<AplicacaoContext> options) : base(options) { }

        public DbSet<Url> Urls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.BuildIndexesFromAnnotations();
        }
    }
}
