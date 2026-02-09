



public class CityExceptionSystem : Exception
{

    public static event Action<CityNode, string>? LimitProblem;


    public CityExceptionSystem(string message) : base(message) { }

    public static void CloseToTheParkingLimit(ParkingLot p, string message)
    {
        LimitProblem?.Invoke(p, message);
    }

    public static void HighTrafficJam(CrossRoad c, string message)
    {
        LimitProblem?.Invoke(c, message);
    }

}
