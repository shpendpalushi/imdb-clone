using IMDBClone.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IMDBClone.Data.Configuration
{
    public class MovieActorConfiguration : IEntityTypeConfiguration<MovieActor>
    {
        public void Configure(EntityTypeBuilder<MovieActor> builder)
        {
            builder.HasKey(ma => ma.MovieActorId);
            builder.HasOne(ma => ma.Actor)
                .WithMany(a => a.Movies)
                .HasForeignKey(ma => ma.ActorId);
            
            builder.HasOne(ma => ma.Movie)
                .WithMany(a => a.Cast)
                .HasForeignKey(ma => ma.MovieId);
        }
    }
}