namespace TestProject1;
    using Xunit;

    public class KnjigaTests
    {
        [Fact]
        public void BrojStranica_SetIspodMinimuma_Exception()
    {
        // Arrange (1. korak - postavljanje varijabli i vrijednosti) 
        var knjiga = new Knjiga();
        int nevazeciBrojStranica = 2;

        Assert.Throws<LibraryLimitException>( // provjerava da li baca ocekivani exception 
            () => // lambda izraz - recept za akciju koju xUnit treba pokrenut u svom try catch bloku 
            { // pocetak uputstva
                knjiga.BrojStranica = nevazeciBrojStranica; // izraz koji bi trebao baciti exception 
            }   // kraj uputstva
            ); 
    }

    [Theory]
    [InlineData(10)]
    [InlineData(500)]
    [InlineData(5000)]
    public void BrojStranica_IspravneVrijednosti(int ispravneVrijednosti) {
        var knjiga = new Knjiga();

        knjiga.BrojStranica = ispravneVrijednosti;

        Assert.Equal(ispravneVrijednosti, knjiga.BrojStranica); 
        
    }

}

