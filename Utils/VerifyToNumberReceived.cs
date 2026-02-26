namespace ConceptsDB.Utils;

public class VerifyToNumberReceived
{
    public static int? ParseInt(string numberInput)
    {
        int number;
        var isNumber = int.TryParse(numberInput, out number) && number > 0;

        if (!isNumber)
        {
            Console.WriteLine("O ID passado não é do tipo número.");
            return null;
        }
        
        return number;
    }
}