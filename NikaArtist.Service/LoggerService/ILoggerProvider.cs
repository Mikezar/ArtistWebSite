namespace NikaArtist.Service.LoggerService
{
    public interface ILoggerProvider
    {
        ILogger<TLogger> GetLogger<TLogger>();
    }
}
