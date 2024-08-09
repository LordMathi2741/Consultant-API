namespace Support.Models;

public partial class WorkShop
{
    public long Id { get; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public long WorkShopCompanyId { get; set; }
}

public partial class WorkShop
{
    public WorkShop()
    {
        Name = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        WorkShopCompanyId = 0;
    }
    
    public WorkShop(string name, string address, string phone, long workShopCompanyId)
    {
        Name = name;
        Address = address;
        Phone = phone;
        WorkShopCompanyId = workShopCompanyId;
    }
}

