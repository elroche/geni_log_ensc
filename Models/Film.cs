using System;
using System.Collections.Generic;

namespace GestionCinema
{

    public class Film
    {

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Realisateur { get; set; }
        public string Resume { get; set; }
        public DateTime Date { get; set; }
        public double Duree { get; set; }
        public int Statut { get; set; }

    }
}