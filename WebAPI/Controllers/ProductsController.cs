using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;

// ReSharper disable StringLiteralTypo
// ReSharper disable CommentTypo

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]//küçük yazılır
        //[Authorize(Roles="Product.List")]//Admin yetkisi gibi düşün Product.List yetkisi olan açabilir.
        //Herhangi bir yetki vermiyorum.bu operasyonu çalıştırabilmek için elinde bir tokenın olması gerek ynai sisteme giriş yapmış gibi düşünün
        public IActionResult GetList()
        {
            var result = _productService.GetList();
            if (result.Success)
            {//Eğer doğru Çalıştıysa Data'yı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        [HttpGet("getlistbycategory")]//küçük yazılır
        public IActionResult GetListByCategory(int categoryId)//isimlendirme standartlarına uy
        {
            var result = _productService.GetListByCategory(categoryId);
            if (result.Success)
            {//Eğer doğru Çalıştıysa Data'yı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        [HttpGet("getbyid")]//küçük yazılır
        public IActionResult GetById(int productId)
        {
            var result = _productService.GetById(productId);
            if (result.Success)
            {//Eğer doğru Çalıştıysa Data'yı getir
                return Ok(result.Data);
            }

            return BadRequest(result.Message);//Eğer Hatalı ise Mesaj dönder.
        }

        [HttpPost("add")]//HttpPost Kullanılır
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }

        [HttpPost("delete")]//HttpPost Kullanılır
        public IActionResult Delete(Product product)
        {
            var result = _productService.Delete(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }

        [HttpPost("update")]//HttpPost Kullanılır
        public IActionResult Update(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }

        [HttpPost("transaction")]//HttpPost Kullanılır
        public IActionResult TransactionTest(Product product)
        {
            var result = _productService.TransactionalOperation(product);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);//Yanlışsa çalışır. Yani if'in içi çalışmazsa çalışır zaten if'in içinde return çalışırsa devamına bakmaz
        }
    }
}
