using MediatR;

namespace Platform.Application.Common;

public interface IQuery : IRequest;

public interface IQuery<out TResponse> : IRequest<TResponse>;
