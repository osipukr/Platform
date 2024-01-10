using MediatR;

namespace Ecommerce.Application.Common;

public interface IQuery : IRequest;

public interface IQuery<out TResponse> : IRequest<TResponse>;
