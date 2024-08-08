namespace Support.Models;

public partial class Observation
{
    public long Id { get;  }
    public string Content { get; set; }
    public long ClientId { get; set; }
}

public partial class Observation
{
    public Observation()
    {
        Content = string.Empty;
        ClientId = 0;
    }
    public Observation( string content, long clientId)
    {
        Content = content;
        ClientId = clientId;
    }
}