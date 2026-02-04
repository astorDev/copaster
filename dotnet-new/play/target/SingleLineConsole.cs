global using target;

namespace target;

public static class SingleLineConsole
{
    extension(ILoggingBuilder logging)
    {
        public void AddSingleLineConsole() => logging.AddSimpleConsole(c => c.SingleLine = true);
    }
}
