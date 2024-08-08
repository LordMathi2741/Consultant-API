namespace Application.DTO.Request;

public class CertifierRequest
{
    public string Name { get; set; }
    public string Brand { get; set; }
    
    public long OperationCenterId { get; set; }
}