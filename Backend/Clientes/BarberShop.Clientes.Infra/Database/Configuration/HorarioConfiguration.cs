using BarberShop.Clientes.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BarberShop.Clientes.Infra.Database.Configuration
{
    public class HorarioConfiguration : IEntityTypeConfiguration<Horario>
    {
        public void Configure(EntityTypeBuilder<Horario> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Horarios)
                .HasForeignKey(x => x.ClienteId);
            builder.HasOne(x => x.Servico);
        }
    }
}
