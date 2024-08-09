namespace Support.Factory.Company;

public partial class InstallerCompany : ICompany
{
    public long Id { get; }
    public string Address { get; set; }
    public string SocialReason { get; set; }
    public string Ruc { get; set; }
    public long WorkShopId { get; set; }
}

public partial class InstallerCompany
{
    public InstallerCompany()
    {
        Address = string.Empty;
        SocialReason = string.Empty;
        Ruc = string.Empty;
        WorkShopId = 0;
    }
    
    public InstallerCompany(string address, string socialReason, string ruc, long workShopId)
    {
        Address = address;
        SocialReason = socialReason;
        Ruc = ruc;
        WorkShopId = workShopId;
    }
}

