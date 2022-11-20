namespace Application.Services;

public interface IWordService
{
    Task<int> GetWordCount(CancellationToken cancellationToken);
}