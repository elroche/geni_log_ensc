using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;
public class Cinema
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Veuillez entrer un nom de cinéma valide.")]
    public string? Nom { get; set; }

    [Required(ErrorMessage = "Veuillez entrer une adresse valide.")]
    public string? Adresse { get; set; }

    [Display(Name = "Code postal")]
    [Required(ErrorMessage = "Veuillez entrer un code postal valide.")]
    public int CodePostal { get; set; }

    [Required(ErrorMessage = "Veuillez entrer une ville valide.")]
    public string? Ville { get; set; }

    [Required(ErrorMessage = "Veuillez entrer un nom de responsable valide.")]
    public string? Responsable { get; set; }

    [Display(Name = "Prix d'une place")]
    [Required(ErrorMessage = "Veuillez entrer un prix valide.")]
    public double PrixPlace { get; set; }
}
