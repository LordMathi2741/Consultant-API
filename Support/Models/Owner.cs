namespace Support.Models;

public partial class Owner
{
    public long Id { get; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Dni { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string District { get; set; }
    public string Province { get; set; }
    public string Department { get; set; }
    
    
}

public partial class Owner
{
    public Owner()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Dni = 0;
        Address = string.Empty;
        Phone = string.Empty;
        District = string.Empty;
        Province = string.Empty;
        Department = string.Empty;
    }
    public Owner(string firstName, string lastName, int dni, string address, string phone, string district, string province, string department)
    {
        FirstName = firstName;
        LastName = lastName;
        Dni = dni;
        Address = address;
        Phone = phone;
        District = district;
        Province = province;
        Department = department;
    }
    
}