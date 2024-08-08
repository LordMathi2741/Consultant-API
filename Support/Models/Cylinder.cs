namespace Support.Models;

public partial class Cylinder
{
    public long Id { get; }
    public string Brand { get; set; }
    public string SerieNumber { get; set; }
    public int Capacity { get; set; }
    public long VehicleId { get; set; }
}

public partial class Cylinder
{
    public Cylinder()
    {
        Brand = string.Empty;
        SerieNumber = string.Empty;
        Capacity = 0;
        VehicleId = 0;
    }
    
    public Cylinder(string brand, string serieNumber, int capacity, long vehicleId)
    {
        Brand = brand;
        SerieNumber = serieNumber;
        Capacity = capacity;
        VehicleId = vehicleId;
    }
}