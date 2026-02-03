public class DeviceLimitException : Exception
{

    public static event Action<Senzor, double>? OnLimitReached; 
    public DeviceLimitException(string message) : base(message) { }
    

    public static void Trigger(Senzor s, double v)
    {
        OnLimitReached?.Invoke(s, v);
    }
} 