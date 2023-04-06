//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;


//using CoffeCRMBeck.Common;
//using CoffeCRMBeck.DAL.Context;
//using CoffeCRMBeck.Service;
//using CoffeCRMBeck.Model;


//namespace TestWebApiOnlineMove.Controllers
//{

//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : Controller
//    {
//        private readonly IOptions<AuthOptions> _authOptions;
//        private readonly AppDbContext _context;
//        private MailService _mailService;
//        private readonly UserService _userService;



//        public AuthController(AppDbContext context, IOptions<AuthOptions> options, MailService mailService, UserService authService)
//        {
//            _authOptions = options;
//            _context = context;
//            _mailService = mailService;
//            _userService = authService;

//        }
//        [HttpPost("registr")]
//        public async Task<IActionResult> Registr(Login request)
//        {
//            try
//            {
//               var user = new User
//                {
//                    id = 0,
//                    Name = request.Email,
//                    UrlIcon = "https://abs.twimg.com/sticky/default_profile_images/default_profile_400x400.png",
//                    FavoritesId = new() { },
//                };
//                await _userService.Create(user);

//                var account = new Worker
//                {
//                    id = 0,
//                    Email = request.Email,
//                    Password = request.Password,
//                    isAcivate = false,
//                    acivateLink = Guid.NewGuid().ToString(),
//                    UserId = user.id,
//                    Roles = new Role[] { Role.User }
//                };
//                if (account != null && !await _context.Accounts.AnyAsync(u => u.Email == request.Email && u.Password == request.Password))
//                {
//                    await _userService.Create(account);

//                    _mailService.SenEmail(account.Email!, "anisite@gmail.com", "Anisite", "The confirmation", $"<div><a href = \"{Configure.URL_BECEND_SITE}/api/Auth/active/{account.acivateLink}\">I confirm</div>");

//                    return Ok(account);
//                }
//            }
//            catch(Exception ex)
//            {
//                return Forbid(ex.Message);
//            }
//            return Unauthorized();
//        }

//        [HttpGet("active/{acivateLink}")]
//        public async Task<IActionResult> Active(string acivateLink)
//        {
//            try
//            {
//                var user = await _context.Accounts.FirstOrDefaultAsync(a => a.acivateLink == acivateLink);
//                if(user != null)
//                {
//                    user.isAcivate = true;
//                    _context.Accounts.Update(user);
//                    await _context.SaveChangesAsync();
//                    return Redirect(Configure.URL_FRONTEND_SITE);
//                }
//                return Forbid();
//            }
//            catch(Exception ex)
//            {
//                return Forbid(ex.Message);
//            }          
//        }
//        [HttpPost("login")]
//        public IActionResult Login(Login request)
//        {
//            if(request == null)
//            {
//                return Forbid();
//            }
//            Account user = AuthenticateUser(request.Email, request.Password);
//            if(user != null)
//            {
//                var token = GenerJWTToken(user);
//                if (token == null) { return Forbid(); }

//                return Ok(new { access_token = token, id = user.id});
//            }
//            return Unauthorized();
//        }

//        private Worker AuthenticateUser(string email, string password)
//        {
//            var worker = _context.Workers.FirstOrDefault(u => u.Email == email && u.Password == password);
//            return worker;
//        }
//        private string GenerJWTToken(Worker worker)
//        {
//            if(worker == null && (worker!.Email == null || worker.Email == "")) { return null!; }

//            var authParams = _authOptions.Value;
//            var securityKey = authParams.GetSymmetricSecurityKey();
//            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>()
//            {
//                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email, worker.Email),
//                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, worker.Id.ToString())
//            };

//           claims.Add(new Claim("role", worker.Roles.ToString()));

//            var token = new JwtSecurityToken(authParams.Issuer,
//                authParams.Audience,
//                claims,
//                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
//                signingCredentials: credentials);
//            return new JwtSecurityTokenHandler().WriteToken(token);

//        }
//    }
//}
