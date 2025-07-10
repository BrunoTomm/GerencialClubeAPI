using GerencialClube.Dominio.Enumeradores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace GerencialClube.Infra.Configuracoes
{
    public class PlanoConfig : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.AreasPermitidas)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<List<AreaClube>>(v, new JsonSerializerOptions()))
                .HasColumnName("AreasPermitidasJson");

            builder.ToTable("Planos");
        }
    }
}
