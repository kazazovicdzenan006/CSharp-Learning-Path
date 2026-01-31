using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace TestProject1
{
 public class KasnjenjeIzracunaj
    {
        [Fact]

        public void KasnjenjeVracanjaKnjige()
        {
            var knjiga = new Knjiga();
            var datumPozajmice = DateOnly.FromDateTime(DateTime.Now).AddDays(-20);

            double rezultat = knjiga.IzracunajKasnjenje(datumPozajmice);

            Assert.Equal(5, rezultat); 
        }


        [Fact]
        public void NemaKasnjenjaKodKnjige()
        {
            var knjiga = new Knjiga();
            var datumPozajmice = DateOnly.FromDateTime(DateTime.Now).AddDays(-5);
            double rezultat = knjiga.IzracunajKasnjenje(datumPozajmice);

            Assert.Equal(0, rezultat); 


        }


        [Fact]
        public void KasnjenjeKodFilma()
        {
            var film = new Film();
            var datumPozajmice = DateOnly.FromDateTime(DateTime.Now).AddDays(-6);
            double rezultat = film.IzracunajKasnjenje(datumPozajmice);

            Assert.Equal(1, rezultat);

        }


        [Fact]
        public void BezKasnjenjeKodFilma()
        {
            var film = new Film();
            var datumPozajmice = DateOnly.FromDateTime(DateTime.Now).AddDays(-3);
            double rezultat = film.IzracunajKasnjenje(datumPozajmice);

            Assert.Equal(0, rezultat);

        }

    }
}
