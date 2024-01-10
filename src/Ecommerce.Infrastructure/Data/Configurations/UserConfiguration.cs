using Ecommerce.Domain.Users;
using Ecommerce.Infrastructure.Data.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Infrastructure.Data.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder
            .Property(x => x.FirstName)
            .HasConversion(firstName => firstName.Value, value => FirstName.Create(value).Value)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasConversion(lastName => lastName.Value, value => LastName.Create(value).Value)
            .IsRequired();

        builder
            .HasIndex(user => user.Email)
            .IsUnique();

        builder
            .Property(x => x.Email)
            .HasConversion(email => email.Value, value => Email.Create(value).Value)
            .IsRequired();

        builder
            .Property(user => user.PasswordHash)
            .IsRequired();
    }
}
