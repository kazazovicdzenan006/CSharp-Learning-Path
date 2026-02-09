namespace SenzoriTest
{
    public class SenzorTest
    {
        [Theory]
        [InlineData(-5)]
        [InlineData(1001)]
        public void NeispravanSetSenzora (double v)
        {
            Senzor senzor = new Senzor ();
            
            Assert.Throws<DeviceLimitException>(()=>
            {
                senzor.Vrijednost = v;

            });       
        
        
        }


        [Theory]
        [InlineData(1)]
        [InlineData(999)]
        public void IspravanSetSenzora(double v)
        { 
            Senzor senzor = new Senzor();
            
            senzor.Vrijednost = v;
            Assert.Equal(v, senzor.Vrijednost); 
        }
    }
}
