using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public class Seance
{

    public int Id { get; set; }
    public Film Film { get; set; } = null!;
    public Salle Salle { get; set; } = null!;

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    [Required(ErrorMessage = "Veuillez entrer une date valide.")]
    public DateTime Date { get; set; }

    [Display(Name = "Nombre de place achetées")]
    public int NbPlaceAchete { get; set; }

}
