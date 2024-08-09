namespace Support.Factory.Cylinder;

public partial class WorkShopCylinder : ICylinder
{
    public long Id { get; }
    public string SerialNumber { get; set; }
    public string Model { get; set; }
    public long Volume { get; set; }
    public DateTimeOffset? MadeDate { get; set; }
    public long WorkShopId { get; set; }
}

public partial class WorkShopCylinder
{
    public WorkShopCylinder()
    {
        SerialNumber = string.Empty;
        Model = string.Empty;
        Volume = 0;
        MadeDate = DateTimeOffset.Now;
        WorkShopId = 0;
    }
    
    public WorkShopCylinder(string serialNumber, string model, long volume, DateTimeOffset madeDate, long workShopId)
    {
        SerialNumber = serialNumber;
        Model = model;
        Volume = volume;
        MadeDate = madeDate;
        WorkShopId = workShopId;
    }
}
