

public static class SafeExecutor
{
    public static void Execute(Action akcija)
    {
        try
        {
            akcija(); 
        }catch(LibraryLimitException ex)
        {
            Console.WriteLine($"Automatski sam obradio gresku: {ex.Message}");
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Nepoznata greska: {ex.Message}");
        }
    }


}



