using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public class Seance
{

    public int Id { get; set; }
    public Film Film { get; set; } = null!;
    public Salle Salle { get; set; } = null!;
    public DateTime Date { get; set; }

    [Display(Name = "Nombre de place acheté")]
    public int NbPlaceAchete { get; set; }

}
