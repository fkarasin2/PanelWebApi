using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Panel.DTOs;
using Panel.Services;

namespace Panel.API.Filters;

public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
{
    private readonly IService<T> _service;

    public NotFoundFilter(IService<T> service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {

        var idValue = context.ActionArguments.Values.FirstOrDefault();
        if (idValue == null)
        {
            await next.Invoke();
            return;
        }
        var id = Guid.Parse(idValue.ToString());
        var anyEntity = await _service.AnyAsync(x=>x.id == id);

        if (anyEntity)
        {
            await next.Invoke();
            return;
        }

        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404,$"{typeof(T).Name}({id}) not found"));

    }
}