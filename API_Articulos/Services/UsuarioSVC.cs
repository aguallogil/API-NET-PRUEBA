using BLL;
using DAL.Helpers;
using DAO;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaAPI.Services
{
    public interface IUsuarioSVC
    {
        Usuario Login(Usuario data);
    }
    public class UsuarioSVC : IUsuarioSVC
    {
        private readonly AppSettings _appSettings;
        public UsuarioSVC(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public Usuario Login(Usuario data)
        {
            if (string.IsNullOrEmpty(data.de_Usuario) || string.IsNullOrEmpty(data.de_Password))
                return null;
            //var user = UsuarioBO.GetAll().SingleOrDefault(x => x.Username.ToUpper() == username.ToUpper() && x.Password.ToUpper() == password.ToUpper());
            var user = UsuarioBO.Login(data);
            if (user == null)
            {
                Usuario jsonData = new Usuario
                {
                    Error_Description = "Usuario o contraseña inválidos"
                };
                return jsonData;
            }
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.id_Usuario.ToString()),
                    new Claim(ClaimTypes.Name, user.de_Usuario),
                    new Claim(ClaimTypes.GivenName, user.de_Password)
                }),
                //Expires = DateTime.UtcNow.AddDays(7),
                //Expires = DateTime.UtcNow.AddHours(1),
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Access_Token = tokenHandler.WriteToken(token);
            user.Expires_In = 240;//240 minutos
            user.Expire_Date = (DateTime)tokenDescriptor.Expires;//DateTime.UtcNow.AddDays(.5);
            // remove password before returning
            user.de_Password = null;

            return user;
        }
    }
}
