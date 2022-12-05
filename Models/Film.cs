using System;
using System.Collections.Generic;

namespace GestionCinema.Models;

public enum Genre
{
    Drame, Biopic, Guerre, ScienceFiction, Comédie, Humour, Action, Aventure, Policier, Thriller, Documentaire, Horreur
}

public class Film
{

    public int Id { get; set; }
    public string Nom { get; set; }
    public string Realisateur { get; set; }
    public string Resume { get; set; }
    public Genre? Genre { get; set; }
    public DateTime Date { get; set; }
    public double Duree { get; set; }
    public int Statut { get; set; } = 1;

}