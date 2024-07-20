using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Api.Models;

public class User : IdentityUser<long>
{
    public List<IdentityRole<long>>? Roles { get; set; }
}
