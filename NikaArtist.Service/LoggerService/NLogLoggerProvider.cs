using NLog;

namespace NikaArtist.Service.LoggerService
{
    public class NLogLoggerProvider : ILoggerProvider
    {
        public ILogger<TLogger> GetLogger<TLogger>()
        {
            return (ILogger<TLogger>) new NLogLogger(LogManager.GetCurrentClassLogger());
        }
    }
}
