using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using info;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Modelos;
using Services;

namespace syncos_Blazor.Services
{
    public class AuthService
    {
        private readonly IDbContextFactory<APPDbContext> _dbFactory;

        public event Action OnAuthStateChanged;
        public Usuario CurrentUser { get; private set; }
        public bool IsAuthenticated => CurrentUser != null;

        public AuthService(IDbContextFactory<APPDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<(bool Sucesso, string Mensagem)> LoginAsync(string loginOuEmail, string senha)
        {
            if (string.IsNullOrWhiteSpace(loginOuEmail) || string.IsNullOrWhiteSpace(senha))
            {
                return (false, "Por favor, informe seu login/e-mail e a senha.");
            }

            try
            {
                using var ctx = await _dbFactory.CreateDbContextAsync();
                var loginTermo = loginOuEmail.Trim();

                // Busca o usuário por E-mail ou Nome de Login
                var user = await ctx.Usuarios
                    .FirstOrDefaultAsync(u => u.Email == loginTermo || u.LoginName == loginTermo);

                if (user == null)
                {
                    return (false, "E-mail ou usuário não encontrado.");
                }

                if (!user.Ativo)
                {
                    return (false, "Este usuário está desativado no sistema.");
                }

                // Criptografa a senha informada usando o algoritmo MD5 com a chave secreta do sistema
                var tempUser = new Usuario
                {
                    Email = user.Email,
                    LoginPass = senha
                };

                var senhaCriptografada = ToPassWord.Get(tempUser);

                if (user.LoginPass != senhaCriptografada)
                {
                    return (false, "Senha inválida. Verifique os dados informados.");
                }

                // Carrega o Perfil do usuário para definir o Role
                var perfil = await ctx.Perfis.FindAsync(user.PerfilId);
                user.Role = perfil?.Descricao ?? "Usuário";

                CurrentUser = user;
                OnAuthStateChanged?.Invoke();

                return (true, "Autenticado com sucesso!");
            }
            catch (Exception ex)
            {
                return (false, $"Erro na autenticação: {ex.Message}");
            }
        }

        public void Logout()
        {
            CurrentUser = null;
            OnAuthStateChanged?.Invoke();
        }
    }

    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly AuthService _authService;

        public CustomAuthStateProvider(AuthService authService)
        {
            _authService = authService;
            _authService.OnAuthStateChanged += NotifyAuthenticationStateChanged;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = _authService.CurrentUser;
            if (user == null)
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.LoginName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Role, user.Role ?? "Usuário"),
                new Claim("PerfilId", user.PerfilId.ToString()),
                new Claim("Id", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, "SyncOSAuth");
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        private void NotifyAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
