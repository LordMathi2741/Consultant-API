using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Infrastructure.Models;

public partial class Client : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}