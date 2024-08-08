namespace Application.DTO.Request;

public class ValveRequest
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerieNumber { get; set; }
    
    public long CylinderId { get; set; }
}