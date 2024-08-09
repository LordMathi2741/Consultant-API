namespace Support.Models;

public partial class OperationCenter
{
   
    public long Id { get; }
    public string Name { get; set; }
    public string LegalRepresentative { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    
    public long ClientId { get; set; }
}

public partial class OperationCenter
{
    public OperationCenter()
    {
        Name = string.Empty;
        LegalRepresentative = string.Empty;
        Address = string.Empty;
        Phone = string.Empty;
        ClientId = 0;
    }
    
    public OperationCenter( string name, string legalRepresentative, string address, string phone, long clientId)
    {
        Name = name;
        LegalRepresentative = legalRepresentative;
        Address = address;
        Phone = phone;
        ClientId = clientId;
    }
 
}