namespace Application.DTO.Response;

public class OperationCenterResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string LegalRepresentative { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    
    public long OwnerId { get; set; }
}