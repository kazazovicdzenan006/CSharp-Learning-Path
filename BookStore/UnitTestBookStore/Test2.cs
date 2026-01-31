using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    public class FilmTest
    {
        [Fact]
        public void TrajanjeFilmaMin_Exception()
        {
            var film = new Film();

            double trajanje = 15;

            

            Assert.Throws<LibraryLimitException>(
                () =>
                {
                    film.TrajanjeUMinutama = trajanje;
                }
                
                );

        }

        [Fact] 
        public void TrajanjeFilmaMax_Exception()
        {
            var film = new Film();
            double trajanje = 401;

            Assert.Throws<LibraryLimitException>(
                () =>
                {
                    film.TrajanjeUMinutama = trajanje;
                }
                );
        }


        [Theory]
        [InlineData(21)]
        [InlineData(120)]
        [InlineData(400)]
        public void IspravnoTrajanjeFilma(double ispravnoTrajanje)
        {
            var film = new Film();

            film.TrajanjeUMinutama = ispravnoTrajanje;


            Assert.Equal(ispravnoTrajanje, film.TrajanjeUMinutama);


        }
    }
}
