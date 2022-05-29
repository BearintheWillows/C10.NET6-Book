namespace WorkingWithEFCore.Logger;

using Microsoft.Extensions.Logging;

public class ConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        /**
		 * We could have different logger implementations for
		 * different categoryName values but for simplicity
		 * we will just return the same logger for all
		 */

        return new ConsoleLogger();
    }

    //If the logger provider needs to do any cleanup, it should do it here

    public void Dispose() { }

}
public class ConsoleLogger : ILogger
{
    //If the logger needs to do any cleanup, it should do it here

    public IDisposable BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        switch (logLevel)
        {
            case LogLevel.Trace:
            case LogLevel.Information:
            case LogLevel.None:
                return false;
            case LogLevel.Debug:
            case LogLevel.Warning:
            case LogLevel.Error:
            case LogLevel.Critical:
            default:
                return true;
        };
    }

    public void Log<TState>(LogLevel loglevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
    {
        if (eventId.Id == 20100)
        {
            //Log the level and event identifier
            Console.Write("Level: {0}, Event Id: {1}, Event: {2}",
                loglevel,
                eventId.Id,
                eventId.Name);

            //Only output state or exception if it exists

            if (state != null)
            {
                Console.Write($", State: {state}");
            }
            if (exception != null)
            {
                Console.Write($", Exception: {exception.Message}");
            }
            Console.WriteLine();
        }
    }
}