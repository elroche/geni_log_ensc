namespace GestionCinema.Models;

// Data Transfer Object class, used to bypass navigation properties validation during API calls
public class SalleDTO
{
    public int Id { get; set; }
    public int CinemaId { get; set; }
    public int NbPlace { get; set; }
    public int NumeroSalle { get; set; }

}
