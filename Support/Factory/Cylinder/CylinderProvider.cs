namespace Support.Factory.Cylinder;

public partial class CylinderProvider : ICylinder
{
    public long Id { get; }
    public string SerieNumber { get; set; }
    public long Volume { get; set; }
    
    public string Brand { get; set; }
    public DateTimeOffset? EmitDate { get; set; }
    public long ProviderCompanyId { get; set; }
}

public partial class CylinderProvider
{
    public CylinderProvider()
    {
        SerieNumber = string.Empty;
        Volume = 0;
        Brand = string.Empty;
        EmitDate = DateTimeOffset.Now;
        ProviderCompanyId = 0;
    }

    public CylinderProvider(string serieNumber, long volume, string brand, DateTimeOffset emitDate,
        long providerCompanyId)
    {
        SerieNumber = serieNumber;
        Volume = volume;
        Brand = brand;
        EmitDate = emitDate;
        ProviderCompanyId = providerCompanyId;
    }
}
