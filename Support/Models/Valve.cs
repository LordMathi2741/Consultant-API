namespace Support.Models;

public partial class Valve
{
    public long Id { get;  }
    public string Brand { get; set; }
    public string Model { get; set; }
    public string SerieNumber { get; set; }
    
    public long CylinderId { get; set; }
}

public partial class Valve
{
    public Valve()
    {
        Brand = string.Empty;
        Model = string.Empty;
        SerieNumber = string.Empty;
        CylinderId = 0;
        
    }
    public Valve( string brand, string model, string serieNumber, long cylinderId)
    {
        Brand = brand;
        Model = model;
        SerieNumber = serieNumber;
        CylinderId = cylinderId;
    }
}