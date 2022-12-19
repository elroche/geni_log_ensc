using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;
public class Cinema
{
    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Adresse { get; set; }

    [Display(Name = "Code postal")]
    public int CodePostal { get; set; }
    public string? Ville { get; set; }
    public string? Responsable { get; set; }

    [Display(Name = "Prix d'une place")]
    public double PrixPlace { get; set; }
}
