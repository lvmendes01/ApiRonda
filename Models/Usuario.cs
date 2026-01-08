using Microsoft.AspNetCore.Identity;

namespace RondaSegurancaBack.Models
{
    public class Usuario : IdentityUser
    {
        public string NomeCompleto { get; set; } = "";
    }
}
