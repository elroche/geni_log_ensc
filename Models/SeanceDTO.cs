namespace GestionCinema.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class SeanceDTO
{
    public int Id { get; set; }
    public int CinemaId { get; set; }
    public int SalleId { get; set; }
    public int FilmId { get; set; }

}
