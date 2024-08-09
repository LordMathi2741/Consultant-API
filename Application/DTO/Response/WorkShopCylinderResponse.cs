namespace Application.DTO.Response;

public class WorkShopCylinderResponse
{
    public long Id { get; set; }
    public string SerialNumber { get; set; }
    public string Model { get; set; }
    public long Volume { get; set; }
    public DateTimeOffset? MadeDate { get; set; }
    public long WorkShopId { get; set; }
}