using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

// ReSharper disable CommentTypo

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategorySevice _categorySevice;

        public CategoriesController(ICategorySevice categorySevice)
        {
            _categorySevice = categorySevice;
        }
        
        [HttpGet("getall")]

        public IActionResult GetList()
        {
            var result = _categorySevice.GetList();
            if (result.Success)
            {//Eğer doğru Çalıştıysa Data'yı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        [HttpGet("getbyid")]//küçük yazılır
        public IActionResult GetById(int categoryId)
        {
            var result = _categorySevice.GetById(categoryId);
            if (result.Success)
            {//Eğer doğru Çalıştıysa Data'yı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        [HttpPost("add")]
        public IActionResult Add(Category category)
        {
            var result = _categorySevice.Add(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }

        [HttpPost("delete")]//HttpPost Kullanılır
        public IActionResult Delete(Category category)
        {
            var result = _categorySevice.Delete(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }

        [HttpPost("update")]//HttpPost Kullanılır
        public IActionResult Update(Category category)
        {
            var result = _categorySevice.Update(category);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }
    }
}
