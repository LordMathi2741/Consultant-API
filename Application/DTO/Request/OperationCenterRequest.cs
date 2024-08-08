namespace Application.DTO.Request;

public class OperationCenterRequest
{
    public string Name { get; set; }
    public string LegalRepresentative { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    
    public long OwnerId { get; set; }
}