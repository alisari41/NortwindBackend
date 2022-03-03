using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Exception;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.CrossCuttingConcerns.Validation;
using Core.Extensions;
using Core.Utilities.Business;
using Core.Utilities.Results;
using FluentValidation;
using Microsoft.AspNetCore.Http;


namespace Business.Concrete
{
    public class ProductManager : IProductService
    {//Bizim iş katmanında veri katmanını  çağırmamız gerekecek
        private IProductDal _productDal;
        private ICategorySevice _categorySevice;

        public ProductManager(IProductDal productDal)
        {
            //Yarın öbür gün başka bir ORM aracı implement ediyorsa onu kullanabilirim
            _productDal = productDal;
        }
        public IDataResult<Product> GetById(int productId)
        {
            //Burada sürekli bağımlılık var. Yani proje sürekli EF olarak yazılması gerekir diyor. Bunu ortadan kaldırmak için Dependency İnjection işlemi yaptım.
            //EfProductDal productDal = new EfProductDal();
            //return productDal.Get(p => p.ProductId == productId);

            //Eğer bu şekilde kullanırsam "Dependency İnjection" sayesinde bağımlılığı ortadan kaldırıyor.Bakacak olursak ortada EFcore kodu gözümüyor.EFcore'a bağımlılık ortadan kalktı. Artık ona bağılı değilim.
            //Yani diğer ORM araçlarıda işlem yapabilmelidir.


            //Bu işlem döndürülüyorsa başarılı olmuştur onun için bana sadece data'sı yeter
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }


        [PerformanceAspect(5)]//Eğer verdiğim saniyeyi(5) geçerse output'a yazıcak
        public IDataResult<List<Product>> GetList()
        {
            Thread.Sleep(5000);
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }

        //[SecuredOperation("Product.List,Admin")]//Yetki , Rol
        [CacheAspect(100)]//Duration Cache'te ne kadar dakika kalıcak değeri veriyorum vermezsem 60 sabit ayarladım.
        [LogAspect(typeof(DatabaseLogger))]//Loglama yapıldı ...... FileLogger da kullanabilirim
        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }


        //Cros Cutting Concerns - Validation, Cache, Log, Performance, Auth, Transaction
        //AOP (Aspect Oriented Programing) Yazılım Geliştirme Yaklaşımı



        //Doğrulama işlemini FluentValidation olarak yaptım
        [ValidationAspect(typeof(ProductValidator), Priority = 1)]//Priority sıralama
        [CacheRemoveAspect("IProductService.Get")]// Yeni Ürün Eklendiğin Ön Belleği temizle. İçerisinde IProductService.Get olanları Yani Başı Get İle başlayanları temizler
        public IResult Add(Product product)
        {

            //ValidationTool.Validate(new ProductValidator(),product); Bu FluentValidation işlemini metot başındaki işlem ile kurtuldum


            //Kuralları kontrol etmek için
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),CheckIfCategoryIsEnabled());
            if (result != null)
            {
                return result;
            }


            _productDal.Add(product);


            //"Ürün başarıyla eklendi."  parantez içinde bunu kullanmak Magic Stringlere giriyor yani bu mesajı bir çok yerde kullandığımı varsayarsak
            //Bunu değiştirmek istediğimizde çok zorlanacağız. O yüzden Magic Stringlerden kurtulmak için bir sınıf oluşturdum mesajları oradan çekiyorum
            return new SuccessResult(Messages.ProductAdded);
        }

        private IResult CheckIfProductNameExists(string productName)
        {//Metodlar çoğunlukla IResult kullanılıyor dikkat et

            //Any metodu, bir koleksiyonda belirtilen koşula uygun kayıt varsa geriye true, yoksa false değerini döndürmektetedir.
            var result = _productDal.GetList(p => p.ProductName == productName).Any();

            if (result)
            {//Eğer girilen ürün adı sistemde varsa
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();//Boş bir successResult dönerse sorun yok Diğer metodda okumak için 
        }

        private IResult CheckIfCategoryIsEnabled()
        {//kategoriler ile iş yapmayı göstermek için uyduruyorum Farklı servisleri kullanmayı göstermek için Mesajlar ve kurulan sistem değişebilir
            var result = _categorySevice.GetList();
            if (result.Data.Count <= 10)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {
            //yaptığım işlemler uydurma anlamak için deniyorum
            _productDal.Update(product);//başarılı olsun 
            _productDal.Add(product); // başarısız olsun sonrasında Update de geri alınsın
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
