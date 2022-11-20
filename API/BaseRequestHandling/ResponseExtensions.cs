using API.Extensions;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.BaseRequestHandling;

public static class ResponseExtensions
{
    public static ActionResult AsAspNetCoreResult(this Response response)
    {
        if (response.HasError)
            return new ObjectResult(new ApiErrorResponse
            {
                Messages = (response as ErrorResponse)?.Meta,
                MainErrorMessage = response.Exception?.Message
            })
            {
                StatusCode = response.StatusCode
            };

        if (response?.Payload != null)
            return new OkObjectResult(response.Payload);
        return new OkResult();
    }

    public static ActionResult AsTransformedAspNetCoreResult<T>(this Response response)
    {
        if (response.HasError)
            return new ObjectResult(new ApiErrorResponse
            {
                Messages = (response as ErrorResponse)?.Meta,
                MainErrorMessage = (response as ErrorResponse).Exception.Message
            })
            {
                StatusCode = response.StatusCode
            };
        return new OkObjectResult(typeof(T).GetConstructor(new[] { response.Payload.GetType() })
            .Invoke(new[] { response.Payload }));
    }
}
