using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Concrete;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authService.Login(userForLoginDto);
            if (!userToLogin.Success)//işlem başarılı değilse
            {
                return BadRequest(userToLogin.Message);
            }
            //Eğer buraya geliyorsa zaten doğru dönmüş remek return dönüş yapıp çıktığı için 
            var result = _authService.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExits = _authService.UserExists(userForRegisterDto.Email); //Öncelikle kullanıcı var mı ona sonucuna işlemler zaten busniessta yapılıyor bakmak gerekiyor
            if (!userExits.Success)//Kullanıcı varsa sonucu
            {//API tarafın ne sonuç dönecek ona bakıyoruz Diğer işlemler zaten İş katmanında yapılıyor.
                return BadRequest(userExits.Message);//
            }

            var registerResult = _authService.Register(userForRegisterDto,userForRegisterDto.Password);//Kullanıcıyı kayıt ettik
            var result = _authService.CreateAccessToken(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
