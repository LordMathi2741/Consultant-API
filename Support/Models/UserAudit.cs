

using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace Support.Models;

public partial class User : IEntityWithCreatedUpdatedDate
{
    public DateTimeOffset? CreatedDate { get; set; }
    public DateTimeOffset? UpdatedDate { get; set; }
}