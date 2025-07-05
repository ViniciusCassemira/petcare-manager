using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using system_petshop.Data;
using system_petshop.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace system_petshop.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Required][EmailAddress] string email, [Required] string password)
        {
            if (ModelState.IsValid)
            {
                // ✅ Buscar usuário pelo email na tabela User (que contém todos os tipos)
                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user != null)
                {
                    // Verificar senha
                    if (VerifyPassword(password, user.PassHash))
                    {
                        // ✅ O UserType já está automaticamente preenchido pelo EF Core discriminator
                        string userRole = DetermineUserRole(user);

                        // Criar claims para o usuário autenticado
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, user.Name ?? ""),
                            new Claim(ClaimTypes.Email, user.Email ?? ""),
                            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                            new Claim(ClaimTypes.Role, userRole),
                        };

                        // ✅ Adicionar claims específicas baseadas no tipo concreto
                        AddSpecificClaims(claims, user);

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = false
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        //return RedirectBasedOnUserType(userRole);
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Email ou senha inválidos.");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        // ✅ NOVO: Determinar role baseada no tipo da instância (TPH)
        private string DetermineUserRole(User user)
        {
            // Com TPH, o EF Core automaticamente retorna a instância do tipo correto
            return user switch
            {
                Admin => "admin",
                Veterinarian => "veterinarian",
                Client => "client",
                _ => "client" // fallback
            };
        }

        // ✅ NOVO: Adicionar claims específicas baseadas no tipo concreto
        private void AddSpecificClaims(List<Claim> claims, User user)
        {
            switch (user)
            {
                case Admin admin:
                    // Claims específicas do Admin
                    claims.Add(new Claim("AdminLevel", "SuperUser"));
                    break;

                case Veterinarian veterinarian:
                    // Claims específicas do Veterinário
                    claims.Add(new Claim("VeterinarianId", veterinarian.UserId.ToString()));
                    // Se tiver outras propriedades específicas do Veterinarian
                    if (!string.IsNullOrEmpty(veterinarian.Cfmv))
                    {
                        claims.Add(new Claim("Cfmv", veterinarian.Cfmv));
                    }
                    break;

                case Client client:
                    // Claims específicas do Cliente
                    claims.Add(new Claim("ClientId", client.UserId.ToString()));
                    break;
            }
        }

        // ✅ Método para redirecionar baseado no tipo de usuário
        //private IActionResult RedirectBasedOnUserType(string userRole)
        //{
        //    return userRole.ToLower() switch
        //    {
        //        "admin" => RedirectToAction("Dashboard", "Admin"),
        //        "veterinarian" => RedirectToAction("Dashboard", "Veterinarian"),
        //        "client" => RedirectToAction("Index", "Home"),
        //        _ => RedirectToAction("Index", "Home")
        //    };
        //}

        // Método para verificar senha
        private bool VerifyPassword(string password, string hash)
        {
            // EXEMPLO SIMPLES - SUBSTITUA por sua lógica de hash
            // Se você está usando BCrypt, seria algo como:
            // return BCrypt.Net.BCrypt.Verify(password, hash);

            // Por enquanto, comparação simples (NÃO use em produção!)
            return password == hash;
        }

        // Método para criar hash da senha
        private string HashPassword(string password)
        {
            // EXEMPLO SIMPLES - SUBSTITUA por sua lógica de hash
            // Se você está usando BCrypt, seria algo como:
            // return BCrypt.Net.BCrypt.HashPassword(password);

            // Por enquanto, retorna a senha sem hash (NÃO use em produção!)
            return password;
        }
    }
}