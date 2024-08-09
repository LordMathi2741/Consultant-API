namespace Application.DTO.Response;

public class CylinderProviderResponse
{
    public long Id { get; set; }
    public string SerieNumber { get; set; }
    public long Volume { get; set; }
    
    public string Brand { get; set; }
    public DateTimeOffset? EmitDate { get; set; }
    public long ProviderCompanyId { get; set; }
}