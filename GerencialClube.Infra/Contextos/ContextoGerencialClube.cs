using GerencialClube.Dominio.Entidades;
using GerencialClube.Infra.Configuracoes;
using Microsoft.EntityFrameworkCore;

namespace GerencialClube.Infra.Contextos
{
    public class ContextoGerencialClube : DbContext
    {
        public ContextoGerencialClube(DbContextOptions<ContextoGerencialClube> options)
            : base(options) { }

        public DbSet<Socio> Socios { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Contato> Contatos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<RegistroAcesso> RegistrosAcesso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SocioConfig());
            modelBuilder.ApplyConfiguration(new EnderecoConfig());
            modelBuilder.ApplyConfiguration(new ContatoConfig());
            modelBuilder.ApplyConfiguration(new PlanoConfig());
            modelBuilder.ApplyConfiguration(new RegistroAcessoConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
