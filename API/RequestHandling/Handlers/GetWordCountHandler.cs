using API.RequestHandling.Requests;
using Application.Services;
using Domain;
using MediatR;

namespace API.RequestHandling.Handlers;

public class GetWordCountHandler : IRequestHandler<GetWordCountRequest, Response>
{
    private readonly IWordService _wordService;
    public GetWordCountHandler(IWordService wordService)
    {
        _wordService = wordService;
    }

    public async Task<Response> Handle(GetWordCountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _wordService.GetWordCount(cancellationToken);
            return Response.Ok(request.Id, result);
        }
        catch (Exception e)
        {
            return Response.InternalServerError(request.Id, e);
        }
    }
}