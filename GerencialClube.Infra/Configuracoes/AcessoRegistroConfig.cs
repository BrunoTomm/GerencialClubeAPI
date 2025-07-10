using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GerencialClube.Infra.Configuracoes
{
    public class RegistroAcessoConfig : IEntityTypeConfiguration<RegistroAcesso>
    {
        public void Configure(EntityTypeBuilder<RegistroAcesso> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Area)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(r => r.DataHora)
                .IsRequired();

            builder.Property(r => r.Autorizado)
                .IsRequired();

            builder.HasOne(r => r.Socio)
                .WithMany()
                .HasForeignKey(r => r.SocioId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("RegistrosAcesso");
        }
    }

}
