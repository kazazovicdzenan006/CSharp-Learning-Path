using System.Security.Cryptography.X509Certificates;

namespace SmartCityTest
{
    public class ParkingLotTest
    {
        [Theory]
        [InlineData(14)]
        [InlineData(3001)]
        public void WrongTotalSpotSet(int p)
        {
            ParkingLot parking = new ParkingLot();
            Assert.Throws<CityExceptionSystem>(
                () =>
                {
                    parking.TotalParkingSpots = p;
                }
                );
                
            }
        

        [Fact]
        public void CorrectTotalSpotSet()
        {
            int number = 150;
            ParkingLot parking = new ParkingLot();
            parking.TotalParkingSpots = number;
            Assert.Equal(number, parking.TotalParkingSpots); 
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1001)]
        
        public void AvailableSpotExceptionSet(int p)
        {
            int totalSpots = 1000; 
            ParkingLot parking = new ParkingLot();

            Assert.Throws<CityExceptionSystem>(
                () =>
                {
                    parking.AvailableParkingSpots = p;
                }
                
                );
                
            }

        [Fact]
        public void AvailableSpotsCorrectSet()
        {
            int available = 150; 
            int totalSpots = 1000;
            ParkingLot parking = new ParkingLot();
            parking.TotalParkingSpots = totalSpots; 
            parking.AvailableParkingSpots = available;

            Assert.Equal(available, parking.AvailableParkingSpots); 

        }

        [Fact]
        public void CloseToLimitTest()
        {
            int total = 100;
            int available = 92; 
            string? receivedMessage = null; 

            ParkingLot p = new ParkingLot();


            p.TotalParkingSpots = total; 
            p.AvailableParkingSpots = available;
            Assert.Equal(available, p.AvailableParkingSpots); 
        }


    }
}
