using MediatR;

namespace Ecommerce.Application.Common;

public interface ICommand : IRequest;

public interface ICommand<out TResult> : IRequest<TResult>;
