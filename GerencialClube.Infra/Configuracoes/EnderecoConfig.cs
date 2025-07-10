using GerencialClube.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerencialClube.Infra.Configuracoes
{
    public class EnderecoConfig : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cep)
                .HasMaxLength(10);

            builder.Property(e => e.Logradouro)
                .HasMaxLength(200);

            builder.Property(e => e.Cidade)
                .HasMaxLength(100);

            builder.Property(e => e.Numero)
                .HasMaxLength(20);

            builder.Property(e => e.Complemento)
                .HasMaxLength(100);

            builder.HasOne(e => e.Socio)
                .WithOne(s => s.Endereco)
                .HasForeignKey<Endereco>(e => e.SocioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Enderecos");
        }
    }
}
