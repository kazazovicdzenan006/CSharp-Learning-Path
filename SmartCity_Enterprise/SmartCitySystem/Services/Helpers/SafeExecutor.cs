public static class SafeExecutor
{



    public static string Execute(Action akcija)
    {
        try
        {
             akcija();
            return null;
        }
        catch (DeviceLimitException ex) { return $"DEVICE ERROR: {ex.Message}"; }
        catch (CityExceptionSystem ex) { return $"CITY ERROR: {ex.Message}"; }
        catch (LibraryLimitException ex) { return $"LIBRARY ERROR: {ex.Message}"; }
        catch (Exception ex) { return $"FATAL ERROR: {ex.Message}"; }
    }

    public static async Task<string> ExecuteAsync(Func<Task> akcija)
    {
        try
        {
            await akcija(); 
            return null;
        }
        catch (DeviceLimitException ex) { return $"DEVICE ERROR: {ex.Message}"; }
        catch (CityExceptionSystem ex) { return $"CITY ERROR: {ex.Message}"; }
        catch (LibraryLimitException ex) { return $"LIBRARY ERROR: {ex.Message}"; }
        catch (Exception ex) { return $"FATAL ERROR: {ex.Message}"; }
    }
}