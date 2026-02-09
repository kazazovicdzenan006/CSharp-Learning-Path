
public class LibraryLimitException : Exception
{

    public static event Action<DateTime, string>? OnLimitReached;

    public LibraryLimitException(string Message) : base(Message)
    {
        OnLimitReached?.Invoke(DateTime.Now, Message);
    }
}
