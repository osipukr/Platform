using AutoMapper;
using AutoMapper.Internal;
using System.Linq.Expressions;

namespace Platform.Application.Common.Extensions;

public static class AutoMapperExtensions
{
    public static IMappingExpression<TSource, TDestination> MapRecordMember<TSource, TDestination, TMember>(
        this IMappingExpression<TSource, TDestination> mappingExpression,
        Expression<Func<TDestination, TMember>> destinationMember,
        Expression<Func<TSource, TMember>> sourceMember)
    {
        ArgumentNullException.ThrowIfNull(mappingExpression);
        ArgumentNullException.ThrowIfNull(destinationMember);
        ArgumentNullException.ThrowIfNull(sourceMember);

        var memberInfo = ReflectionHelper.FindProperty(destinationMember);
        var memberName = memberInfo.Name;

        return mappingExpression
            .ForMember(destinationMember, opt => opt.MapFrom(sourceMember))
            .ForCtorParam(memberName, opt => opt.MapFrom(sourceMember));
    }
}
