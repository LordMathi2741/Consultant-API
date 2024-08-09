namespace Application.DTO.Response;

public class ProviderCompanyResponse
{
    public long Id { get; set; }
    public string ContactPerson { get; set; }
    public string Ruc { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    
    public long UserId { get; set; }
}