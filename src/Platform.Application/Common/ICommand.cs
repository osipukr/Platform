using MediatR;

namespace Platform.Application.Common;

public interface ICommand : IRequest;

public interface ICommand<out TResult> : IRequest<TResult>;
