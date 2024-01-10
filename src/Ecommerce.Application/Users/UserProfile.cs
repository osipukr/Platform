using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Domain.Users;

namespace Ecommerce.Application.Users;

internal sealed class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .MapRecordMember(dest => dest.FirstName, src => src.FirstName.Value)
            .MapRecordMember(dest => dest.LastName, src => src.LastName.Value)
            .MapRecordMember(dest => dest.Email, src => src.Email.Value);
    }
}
