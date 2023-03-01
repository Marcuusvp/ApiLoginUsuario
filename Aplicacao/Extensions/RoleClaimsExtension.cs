using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this Usuario user)
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName)
            };
            result.AddRange(
                user.Permissoes.Select(role => new Claim(ClaimTypes.Role, role.Nome)));
            return result;
        }
    }
}
