using Architectures.CleanArch.Domain.Excecoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Architectures.CleanArch.WebApi.Filters;

public class FiltroExcecao : ExceptionFilterAttribute
{
    private readonly IDictionary<Type, Action<ExceptionContext>> _handlers;

    public FiltroExcecao()
    {
        _handlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ImprocessavelExcecao), HandleValidationException },
            { typeof(ObjetoNaoEncontradoExcecao), HandleNotFoundException },
            { typeof(AutorizacaoExcecao), HandlePermissionException }
        };
    }

    public override void OnException(ExceptionContext context)
    {
        var type = context.Exception.GetType();
        if (_handlers.ContainsKey(type))
        {
            _handlers[type].Invoke(context);
            return;
        }

        base.OnException(context);
    }

    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ImprocessavelExcecao)context.Exception;

        context.Result = new BadRequestObjectResult(exception.Message);
        context.ExceptionHandled = true;
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (ObjetoNaoEncontradoExcecao)context.Exception;

        context.Result = new NotFoundObjectResult(exception.Message);
        context.ExceptionHandled = true;
    }

    private void HandlePermissionException(ExceptionContext context)
    {
        var exception = (AutorizacaoExcecao)context.Exception;

        context.Result = new UnauthorizedObjectResult(exception.Message);
        context.ExceptionHandled = true;
    }
}