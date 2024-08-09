namespace Application.DTO.Request;

public class VehicleRequest
{
    public string VehicleIdentifier { get; set; }
    
    public long OwnerId { get; set; }
    
    public long CylinderId { get; set; }
}