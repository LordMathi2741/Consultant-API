namespace Application.DTO.Response;

public class ValveResponse
{
    public long Id { get;  }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerieNumber { get; set; }
    
    public long CylinderId { get; set; }
}