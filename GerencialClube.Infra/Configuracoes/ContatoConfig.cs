using GerencialClube.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerencialClube.Infra.Configuracoes
{
    public class ContatoConfig : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Texto)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Tipo)
                .IsRequired()
                .HasConversion<int>();

            builder.HasOne(c => c.Socio)
                .WithMany(c => c.Contatos)
                .HasForeignKey(c => c.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Contato");
        }
    }
}
