using NLog;
using System;

namespace NikaArtist.Service.LoggerService
{
    public class NLogLogger : ILogger<Logger>
    {
        private readonly Logger _logger;

        public NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        public Logger InternalLogger => _logger;

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public void LogError(string message, Exception exception)
        {
            _logger.Error(exception, message);
        }

        public void LogError(Exception exception)
        {
            _logger.Error(exception);
        }
    }
}
