using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SisGerenciador.src.Models;

namespace SisGerenciador.Identity
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<Usuario, IdentityRole>
    {
        public AdditionalUserClaimsPrincipalFactory(
            UserManager<Usuario> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        public async override Task<ClaimsPrincipal> CreateAsync(Usuario user)
        {
            var principal = await base.CreateAsync(user);

            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();
            if (user.isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "AdminBlaster"));
            }
            else
            {
                claims.Add(new Claim(ClaimTypes.Role, "UserBlaster"));
            }

            identity.AddClaims(claims);
            return principal;
        }
    }
}
