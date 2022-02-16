using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class ProductValidator : AbstractValidator<Product>
    {//Product Entity'sini doğrulayacak ... AbstractValidator için FluentValidation kütüphanesi indirmek gerekir.
        public ProductValidator()
        {// Aslında tek bir satırda kullanılabilir. Fakat  1 satırda 2 kural olmuş olur. ayırmak  sade ve daha güzel (yazım tekniği)..SOLİD e uyuyorum
            //Mesela tek satırda 2 kural yan yana olsaydı Mesajjı nasıl vericektim gibi ayırarak kullanmaya alışmalıyım
            RuleFor(p => p.ProductName).NotEmpty(); //Kural. ProductName boş olamaz ve  P yi yukardan gelen <Product> nesnelerinden alır
            RuleFor(p => p.ProductName).Length(2, 41);// min 2(dahil) karakter max 41(dahil) karakter olabilir.

            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(1);//1 den büyük veya eşit olmalı
            RuleFor(p => p.UnitPrice).GreaterThanOrEqualTo(10).When(p=>p.CategoryId==1);//1. kategorini fiyatı min 10 dur
            RuleFor(p => p.ProductName).Must(StartWithWithA);//Belli bir alan ile başlamalı kuralı. Mesela faturanın başına 00 koyun
        }

        private bool StartWithWithA(string arg)
        {
            return arg.StartsWith("A");//Büyük A ile başlamalı kuralı oluşturdum.
        }
    }
}
