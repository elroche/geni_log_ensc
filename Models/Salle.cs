using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public class Salle
{

    public int Id { get; set; }

    public int CinemaId { get; set; }
    public Cinema Cinema { get; set; } = null!;

    [Display(Name = "Nombre de place")]
    [Required(ErrorMessage = "Veuillez entrer un nombre de place valide.")]
    public int NbPlace { get; set; }

    [Display(Name = "Numero de salle")]
    [Required(ErrorMessage = "Veuillez entrer un numéro de salle valide.")]
    public int NumeroSalle { get; set; }

    // Default (empty) constructor
    public Salle() { }

    // Copy constructor
    public Salle(SalleDTO dto)
    {
        // Copy DTO field values
        Id = dto.Id;
        CinemaId = dto.CinemaId;
    }
}
