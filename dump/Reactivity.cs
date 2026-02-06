namespace Copaster.Dump;

// This is a simple file one can use to test copaster's functionality

public interface IListenable
{
    public void AddListener(Action listener);
}

public class Notifier : IListenable
{
    private readonly List<Action> _listeners = [];

    public void AddListener(Action listener)
    {
        _listeners.Add(listener);
    }

    public void Notify()
    {
        foreach (var listener in _listeners)
        {
            listener();
        }
    }
}