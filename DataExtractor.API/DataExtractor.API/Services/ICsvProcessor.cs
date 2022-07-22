public interface ICsvProcessor
{
    Task<bool> Process(string fileName);
    Task<bool> Save(string fileName);
}