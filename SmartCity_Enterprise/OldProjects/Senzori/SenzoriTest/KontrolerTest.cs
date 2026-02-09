using System;
using System.Collections.Generic;
using System.Text;

namespace SenzoriTest
{
    public class KontrolerTest
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(9)]
        public void KontrolerNeispravanSet(int v)
        {
            Kontroler kontroler = new Kontroler();

            Assert.Throws<DeviceLimitException>(()=>
            {
                kontroler.BrojKanala = v; 
            });


        }
        [Theory]
        [InlineData(0)]
        [InlineData(8)]
        public void KontrolerIspravanSet(int v)
        {
            Kontroler kontroler = new Kontroler();
            kontroler.BrojKanala = v;
            Assert.Equal(v, kontroler.BrojKanala); 
        }




    }
}
