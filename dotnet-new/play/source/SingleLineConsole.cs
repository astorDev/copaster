namespace Copaster.DotnetNew.Playground.Source;

public static class SingleLineConsole
{
    extension(ILoggingBuilder logging)
    {
        public void AddSingleLineConsole() => logging.AddSimpleConsole(c => c.SingleLine = true);
    }
}
