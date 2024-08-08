namespace Infrastructure.Models;

public partial class Client
{
    public long Id { get; }
    public int Dni { get;  set; }
    public string Email { get;  set; }
    public string Phone { get;  set; }
    public string Address { get;  set; }
    public string Password { get;  set; }
    public string Username { get;  set; }
    public string Company { get;  set; }

    public string Type { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    
    public string Role { get; set; }
    
}

public partial class Client
{
    public Client()
    {
        Firstname = string.Empty;
        Lastname = string.Empty;
        Dni = 0;
        Email = string.Empty;
        Phone = string.Empty;
        Address = string.Empty;
        Password = string.Empty;
        Username = string.Empty;
        Company = string.Empty;
        Type = string.Empty;
    }
    
    public Client(string firstName, string lastName, int dni, string email, string phone, string address, string password, string username, string company,string type)
    {
        Firstname = firstName;
        Lastname = lastName;
        Dni = dni;
        Email = email;
        Phone = phone;
        Address = address;
        Password = password;
        Username = username;
        Company = company;
        Type = type;
        Role = EClientRoles.Default.ToString();
        CreatedDate = DateTimeOffset.Now;
        UpdatedDate = DateTimeOffset.Now;
    }
}