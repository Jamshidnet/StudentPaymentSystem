using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Token;

public class UserRefreshToken
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string RefreshToken { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
   public DateTimeOffset ExpirationDate { get; set; }
}
