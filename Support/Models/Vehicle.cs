namespace Support.Models;

public partial class Vehicle
{
    public long Id { get; }
    public string VehicleIdentifier { get; set; }
    
    public long OwnerId { get; set; }
}

public partial class Vehicle
{
    public Vehicle()
    {
        VehicleIdentifier = string.Empty;
        OwnerId = 0;
    }
    
    public Vehicle(string vehicleIdentifier, long ownerId)
    {
        VehicleIdentifier = vehicleIdentifier;
        OwnerId = ownerId;
    }
  
}