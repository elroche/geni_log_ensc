using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public class Seance
{

    public int Id { get; set; }

    public int FilmId { get; set; }
    public Film Film { get; set; } = null!;

    public int SalleId { get; set; }
    public Salle Salle { get; set; } = null!;

    public int CinemaId { get; set; }

    [Display(Name = "Cinéma")]
    public Cinema Cinema { get; set; } = null!;

    [Required(ErrorMessage = "Veuillez entrer une date et un horaire valide.")]
    public DateTime Date { get; set; }

    [Display(Name = "Nombre de places achetées")]
    public int NbPlaceAchete { get; set; } = 0;


    // Default (empty) constructor
    public Seance() { }

    // Copy constructor
    public Seance(SeanceDTO dto)
    {
        // Copy DTO field values
        Id = dto.Id;
        SalleId = dto.SalleId;
        CinemaId = dto.CinemaId;
        FilmId = dto.FilmId;
    }
}
