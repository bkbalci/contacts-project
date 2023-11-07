using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContactService.Infrastructure.EntityConfigurations;

public class UserContactInfoConfiguration : IEntityTypeConfiguration<UserContactInfo>
{
    public void Configure(EntityTypeBuilder<UserContactInfo> builder)
    {
        builder.ToTable("UserContactInfos");
        builder.HasKey(ci => ci.Id);
        
        builder.Property(ci => ci.ContactType)
            .IsRequired();

        builder.Property(ci => ci.ContactTypeValue)
            .HasMaxLength(100);

        builder.HasOne(ci => ci.User)
            .WithMany(u => u.ContactInfos)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}