using Microsoft.AspNetCore.Identity;

namespace crudProdutos.Models
{
    public class AppUser : IdentityUser
    {
        public string Nome { get; set; }
    }
}
