using System;
using System.Collections.Generic;
using System.Text;

namespace SenzoriTest
{
    public class GetStatusTest
    {

        [Fact]
        public void GetStatusTestVeceOd100()
        {
            double vrijednost = 101; 
            Senzor senzor = new Senzor();

            senzor.Vrijednost = vrijednost; 


            Assert.Equal($"{senzor.Name}: KRITICNO", senzor.GetStatus());

        }


        [Fact]
        public void GetStatusTestManjeOd100()
        {
            double vrijednost = 99;
            Senzor senzor = new Senzor();

            senzor.Vrijednost = vrijednost;


            Assert.Equal($"{senzor.Name}: Normalno", senzor.GetStatus());

        }

    }
}
