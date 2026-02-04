using System;
using System.Collections.Generic;
using System.Text;

namespace Senzori.Executors
{
    public static class SafeExecutor
    {
        public static void Execute(Action akcija)
        {
            try
            {
                akcija();
            }catch (DeviceLimitException ex)
            {
                Console.WriteLine(ex.Message);
            }catch(Exception ex)
            {
                Console.WriteLine($"NEPOZNATA GRESKA: {ex.Message}");
            }
        }


    }
}
