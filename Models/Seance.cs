using System;
using System.Collections.Generic;

namespace GestionCinema.Models;

public class Seance
{

    public int Id { get; set; }
    public Film Film { get; set; }
    public Salle Salle { get; set; }
    public DateTime Date { get; set; }
    public int NbPlaceAchete { get; set; }

}
