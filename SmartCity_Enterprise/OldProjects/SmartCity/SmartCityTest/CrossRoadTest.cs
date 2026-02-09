



public class CrossRoadTest
{
    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void WrongTrafficJamSet(double v)
    {
        CrossRoad cross = new CrossRoad();

        Assert.Throws<CityExceptionSystem>(
            ()=>
            {
                cross.TrafficJamPercantage = v; 
            }
            );
    }



    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(22.5)]
    [InlineData(99)]
    [InlineData(100)]
    public void CorrectTrafficJamSet(double v)
    {
        CrossRoad cross = new CrossRoad(); 
        cross.TrafficJamPercantage = v;

        Assert.Equal(v, cross.TrafficJamPercantage); 


    }
    
}