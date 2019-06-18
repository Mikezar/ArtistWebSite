using System;

namespace NikaArtist.Service.LoggerService
{
    public interface ILogger<TLogger>
    {
        TLogger InternalLogger { get; }

        void LogError(string message);
        void LogError(string message, Exception exception);
        void LogError(Exception exception);
    }
}
