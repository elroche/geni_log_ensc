using System;
using System.Collections.Generic;

namespace GestionCinema.Models;

public class Salle
{

    public int Id { get; set; }
    public Cinema Cinema { get; set; }
    public int NbPlace { get; set; }
    public int NumeroSalle { get; set; }
}
