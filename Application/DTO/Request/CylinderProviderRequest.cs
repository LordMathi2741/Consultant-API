namespace Application.DTO.Request;

public class CylinderProviderRequest
{
    public string SerieNumber { get; set; }
    public long Volume { get; set; }
    
    public string Brand { get; set; }
    public DateTimeOffset? EmitDate { get; set; }
    public long ProviderCompanyId { get; set; }
}