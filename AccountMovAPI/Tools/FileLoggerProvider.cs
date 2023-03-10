using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
//using Microsoft.Extensions.Logging.File;

namespace AccountMovAPI.Helpers
{

    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        public FileLoggerProvider(string filePath)
        {
            _filePath = filePath;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(categoryName, _filePath);
        }

        public void Dispose()
        {
        }
    }

    public class FileLogger : ILogger
    {
        private readonly string _categoryName;
        private readonly string _filePath;

        public FileLogger(string categoryName, string filePath)
        {
            _categoryName = categoryName;
            _filePath = filePath;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var message = formatter(state, exception);
            var logEntry = $"{DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")} [{logLevel}] {_categoryName}: {message}{Environment.NewLine}";
            File.AppendAllText(_filePath, logEntry);
        }

        private class NullScope : IDisposable
        {
            public static NullScope Instance { get; } = new NullScope();

            public void Dispose()
            {
            }
        }
    }

}
