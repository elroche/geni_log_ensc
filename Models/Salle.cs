using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public class Salle
{
    public int Id { get; set; }
    public Cinema Cinema { get; set; } = null!;

    [Display(Name = "Nombre de place")]
    public int NbPlace { get; set; }

    [Display(Name = "Numero de salle")]
    public int NumeroSalle { get; set; }
}
