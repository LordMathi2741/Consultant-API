namespace Application.DTO.Response;

public class OwnerResponse
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Dni { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string District { get; set; }
    public string Province { get; set; }
    public string Department { get; set; }
    
    public long OperationCenterId { get; set; }
}