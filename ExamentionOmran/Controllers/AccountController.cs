using AutoMapper;
using Examention.Api.DTO;
using Examention.Data.Models;
using Examention.EF.Repository.GenricRepository;
using Examention.EF.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Examention.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(IMapper mapper,UserManager<User> userManager
            ,SignInManager<User>signInManager, IUnitOfWork unitofwork,
            IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitofwork;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        [HttpPost("RegisterForStudent")]
        public async Task<IActionResult> RegisterForStudent(RegisterStudentDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _mapper.Map<User>(registerDto);
            var result =await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var roleExist =await _roleManager.RoleExistsAsync("Student");
            if(!roleExist)
            {
                var role = new IdentityRole("Student");
                 await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Student");
            var userAfterSave =await _userManager.FindByEmailAsync(user.Email);
            var student = new Student
            {
                LevelId=registerDto.LevelId,
                UserId= userAfterSave.Id,
            };

            await _unitOfWork.Students.Create(student);
            return Ok(registerDto);
        } 
        [HttpPost("RegisterDocotor")]
        public async Task<IActionResult> RegisterDocotor(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _mapper.Map<User>(registerDto);
            var result =await _userManager.CreateAsync(user,registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            var roleExist = await _roleManager.RoleExistsAsync("Doctor");
            if (!roleExist)
            {
                var role = new IdentityRole("Doctor");
                await _roleManager.CreateAsync(role);
            }
            await _userManager.AddToRoleAsync(user, "Doctor");
            var userAfterSave =await _userManager.FindByEmailAsync(user.Email);
            var doctor = new Doctor
            {
                UserId= userAfterSave.Id,
            };
            await _unitOfWork.Doctors.Create(doctor);
            return Ok(registerDto);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Please Enter Email and Password");
            var result = await _signInManager.PasswordSignInAsync(loginDto.Email,loginDto.Password,false,false);
            if(!result.Succeeded)
            {
                return Unauthorized("Email or Password NoCorrect");
            }
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var claime = new List<Claim>();
            claime.Add(new Claim(ClaimTypes.Name, user.UserName));
            claime.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claime.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var Roles = await _userManager.GetRolesAsync(user);
            foreach (var role in Roles)
            {
                claime.Add(new Claim(ClaimTypes.Role, role));
            }
            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken Token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: claime,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: signingCredentials
            );
            return Ok(new
            {
                message="success",
                token = new JwtSecurityTokenHandler().WriteToken(Token),
                expiration = Token.ValidTo
            });
    

        }
             


    }
}
