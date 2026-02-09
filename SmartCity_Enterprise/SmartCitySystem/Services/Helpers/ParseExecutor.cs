


public class ParseExecutor
{

    public int SafeParse(string input)
    {

        bool success = int.TryParse(input, out int result);
        if (success)
        {

            return result;


        }
        throw new LibraryLimitException("We couldn't parse your input!");

    }
}








