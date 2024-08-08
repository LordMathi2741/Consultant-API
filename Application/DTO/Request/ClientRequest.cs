namespace Application.DTO.Request;

public class ClientRequest
{
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
}