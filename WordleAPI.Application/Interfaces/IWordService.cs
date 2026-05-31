namespace WordleAPI.Application.Interfaces;

public interface IWordService
{
    string GetRandomWord();
    bool IsValidWord(string word);
}
