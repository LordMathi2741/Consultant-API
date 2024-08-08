namespace Support.Models;

public partial class Certifier
{
    public long Id { get;  }
    public string Name { get; set; }
    public string Brand { get; set; }
    
    public long OperationCenterId { get; set; }
}

public partial class Certifier
{
    public Certifier()
    {
        Name = string.Empty;
        Brand = string.Empty;
        OperationCenterId = 0;
    }
    public Certifier(string name, string brand, long operationCenterId)
    {
        Name = name;
        Brand = brand;
        OperationCenterId = operationCenterId;
    }
}