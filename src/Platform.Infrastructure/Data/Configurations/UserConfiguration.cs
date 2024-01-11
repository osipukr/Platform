using Platform.Domain.Users;
using Platform.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Platform.Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id).ValueGeneratedOnAdd();

        builder
            .OwnsOne(user => user.FirstName)
            .Property(firstName => firstName.Value)
            .HasColumnName(nameof(User.FirstName));

        builder
            .OwnsOne(user => user.LastName)
            .Property(lastName => lastName.Value)
            .HasColumnName(nameof(User.LastName));

        builder
            .OwnsOne(user => user.Email)
            .Property(email => email.Value)
            .HasColumnName(nameof(User.Email));

        builder
            .Property(user => user.PasswordHash)
            .IsRequired();
    }
}
