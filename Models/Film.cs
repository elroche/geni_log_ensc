using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GestionCinema.Models;

public enum Genre
{
    Drame, Biopic, Guerre, ScienceFiction, Comédie, Humour, Action, Aventure, Policier, Thriller, Documentaire, Horreur
}

public class Film
{

    public int Id { get; set; }

    [Required(ErrorMessage = "Veuillez entrer un nom valide.")]
    public string? Nom { get; set; }

    [Display(Name = "Réalisateur")]
    public string? Realisateur { get; set; }

    [Display(Name = "Résumé")]
    [Required(ErrorMessage = "Veuillez entrer un résumé valide.")]
    public string? Resume { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Genre? Genre { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
    [Required(ErrorMessage = "Veuillez entrer une date valide.")]
    public DateTime Date { get; set; }

    [Display(Name = "Durée")]
    [Required(ErrorMessage = "Veuillez entrer une durée valide.")]
    public double Duree { get; set; }

    public static string[] getNamesGenres()
    {
        return (Enum.GetNames(typeof(Genre)));
    }
}