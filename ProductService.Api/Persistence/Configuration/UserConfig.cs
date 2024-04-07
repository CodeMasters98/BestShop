using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductService.Api.Entities;

namespace ProductService.Api.Persistence.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(x => x.FirstName)
            .HasMaxLength(200)
            .IsRequired();
    }
}
