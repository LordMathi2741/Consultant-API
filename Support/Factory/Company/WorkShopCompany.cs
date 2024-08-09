namespace Support.Factory.Company;

public partial class WorkShopCompany : ICompany
{
    public long Id { get; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Ruc { get; set; }
    public string Phone { get; set; }
    
    public long UserId { get; set; }
}

public partial class WorkShopCompany
{
    public WorkShopCompany()
    {
        Name = string.Empty;
        Address = string.Empty;
        Ruc = string.Empty;
        Phone = string.Empty;
        UserId = 0;
    }
    
    public WorkShopCompany(string name, string address, string ruc, string phone, long userId)
    {
        Name = name;
        Address = address;
        Ruc = ruc;
        Phone = phone;
        UserId = userId;
    }
}