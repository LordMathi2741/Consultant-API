namespace Support.Models;

public partial class Vehicle
{
    public long Id { get; }
    public string VehicleIdentifier { get; set; }
    
    public long OwnerId { get; set; }
    
    public long CylinderId { get; set; }
    
    
}

public partial class Vehicle
{
    public Vehicle()
    {
        VehicleIdentifier = string.Empty;
        OwnerId = 0;
        CylinderId = 0;
    }
    
    public Vehicle(string vehicleIdentifier, long ownerId, long cylinderId)
    {
        VehicleIdentifier = vehicleIdentifier;
        OwnerId = ownerId;
        CylinderId = cylinderId;
    }
  
}