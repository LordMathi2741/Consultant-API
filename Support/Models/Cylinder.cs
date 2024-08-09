namespace Support.Models;

public partial class Cylinder
{
    public long Id { get; }
    public string Brand { get; set; }
    public string SerieNumber { get; set; }
    public int Capacity { get; set; }
    public long ClientId{ get; set; }
}

public partial class Cylinder
{
    public Cylinder()
    {
        Brand = string.Empty;
        SerieNumber = string.Empty;
        Capacity = 0;
        ClientId = 0;
    }
    
    public Cylinder(string brand, string serieNumber, int capacity, long clientId)
    {
        Brand = brand;
        SerieNumber = serieNumber;
        Capacity = capacity;
        ClientId = clientId;
    }
}