using GerencialClube.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerencialClube.Infra.Configuracoes
{
    public class SocioConfig : IEntityTypeConfiguration<Socio>
    {
        public void Configure(EntityTypeBuilder<Socio> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.DataCadastro)
                .IsRequired();

            builder.HasOne(c => c.Endereco)
                .WithOne(e => e.Socio)
                .HasForeignKey<Endereco>(e => e.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Contatos)
                .WithOne(c => c.Socio)
                .HasForeignKey(c => c.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Plano)
                .WithMany()
                .HasForeignKey(c => c.PlanoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Socios");
        }
    }
}
