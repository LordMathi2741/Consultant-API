namespace Support.Factory.Company;

public partial class ProviderCompany : ICompany
{
    public long Id { get; }
    public string ContactPerson { get; set; }
    public string Ruc { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    
    public long UserId { get; set; }
}

public partial class ProviderCompany
{
    public ProviderCompany()
    {
        ContactPerson = string.Empty;
        Ruc = string.Empty;
        Name = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        UserId = 0;
    }
    
    public ProviderCompany(string contactPerson, string ruc, string name, string address, string phone, long userId)
    {
        ContactPerson = contactPerson;
        Ruc = ruc;
        Name = name;
        Address = address;
        Phone = phone;
        UserId = userId;
    }
}