namespace Application.DTO.Response;

public class VehicleResponse
{
    public long Id { get; set; }
    public string VehicleIdentifier { get; set; }
    
    public long OwnerId { get; set; }
    
    public long CylinderId { get; set; }
}