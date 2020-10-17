using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PontoColeta.Models;

namespace PontoColeta.Data.Maps
{
    public class CoordinateMap : IEntityTypeConfiguration<Coordinate>
    {
        public void Configure(EntityTypeBuilder<Coordinate> builder)
        {
            builder.ToTable("Coordinate");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Latitude).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");
            builder.Property(x => x.Longitude).IsRequired().HasMaxLength(11).HasColumnType("varchar(11)");
            builder.Property(x => x.NameOfPlace).HasMaxLength(200).HasColumnType("varchar(200)");
            builder.HasOne(x => x.Category).WithMany(x => x.Coordinates);
        }
    }
}