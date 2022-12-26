using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestionCinema.Models;

public enum Genre
{
    Drame, Biopic, Guerre, ScienceFiction, Comédie, Humour, Action, Aventure, Policier, Thriller, Documentaire, Horreur
}

public class Film
{

    public int Id { get; set; }
    public string? Nom { get; set; }
    public string? Realisateur { get; set; }

    [Display(Name = "Resumé")]
    public string? Resume { get; set; }
    public Genre? Genre { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    public DateTime Date { get; set; }

    [Display(Name = "Durée")]
    public double Duree { get; set; }
    public int Statut { get; set; } = 1;

}