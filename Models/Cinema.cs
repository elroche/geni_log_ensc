using System;
using System.Collections.Generic;

namespace GestionCinema
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public int CodePostal { get; set; }
        public string Ville { get; set; }
        public string Responsable { get; set; }
        public double PrixPlace { get; set; }
    }
}
