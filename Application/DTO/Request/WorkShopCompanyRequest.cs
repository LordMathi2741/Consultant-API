namespace Application.DTO.Request;

public class WorkShopCompanyRequest
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string Ruc { get; set; }
    public string Phone { get; set; }
    
    public long UserId { get; set; }
}