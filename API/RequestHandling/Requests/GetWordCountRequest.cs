using Domain;

namespace API.RequestHandling.Requests;

public class GetWordCountRequest : Request<Response>
{
    public GetWordCountRequest(IFormFile document)
    {
        Document = document;
    }
    public IFormFile Document { get; set; }
}