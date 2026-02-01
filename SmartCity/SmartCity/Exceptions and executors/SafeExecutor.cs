



public static class SafeExecutor { 

    public static void Execute(Action action)
    {
        try
        {
            action(); 
        }
        catch (CityExceptionSystem ex)
        {
            Console.WriteLine($"City Limit exception: {ex.Message}");
        }catch (Exception ex)
        {
            Console.WriteLine($"Nepoznata greska: {ex.Message}");
        }
    }

}




