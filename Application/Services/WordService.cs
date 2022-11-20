namespace Application.Services;

public class WordService : IWordService
{
    public Task<int> GetWordCount(CancellationToken cancellationToken)
    {
        var result = 5;
        return Task.FromResult(result);
    }
}